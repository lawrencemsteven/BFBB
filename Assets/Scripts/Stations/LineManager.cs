using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
[RequireComponent(typeof(LineRenderer))]
public class LineManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject pourTool;
    [SerializeField] private float maxDistance = 0.3F;
    [SerializeField] private bool drawLine;
    [SerializeField] private bool lerpToPoint;
    [SerializeField] private bool colorChange;
    [SerializeField] private int measuresPerShape;
    [SerializeField] private float markerMinScale;
    [SerializeField] private float markerMaxScale;
    [SerializeField] private float earlyThreshold;

    private CoordinateGenerator coordinateGenerator;
    private LineRenderer lineRenderer;
    private GameObject marker;
    private Coroutine beatFollower;
    private bool readyForNewMeasure = true;
    private int currentBeat = 1;
    private Vector3 defaultMarkerScale;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        coordinateGenerator = GetComponent<CoordinateGenerator>();
    }

    void Start()
    {
        lineRenderer.useWorldSpace = false;
    }

    public void DrawLine()
    {
        lineRenderer.enabled = drawLine;

        coordinateGenerator.GenerateShape();
        Destroy(marker);
        marker = null;
        if (beatFollower != null)
        {
            StopCoroutine(beatFollower);
        }

        List<CoordinateCollider> points = coordinateGenerator.GetColliders();
        lineRenderer.positionCount = points.Count;
        int i = 0;
        foreach (CoordinateCollider point in points)
        {
            Vector3 position = point.transform.localPosition;
            lineRenderer.SetPosition(i, position);
            i++;
        }

        beatFollower = StartCoroutine(FollowBeat());
    }

    private IEnumerator FollowBeat()
    {
        int totalBeats = lineRenderer.positionCount - 1;
        int halfBeats = 2 * (totalBeats - (int)SongInfo.Instance.getBeatsPerMeasure());
        int fullBeats = totalBeats - halfBeats;
        float accumulatedTime = 0F;
        float beatProgress = 0F;

        marker = GameObject.Instantiate(markerPrefab, transform);
        defaultMarkerScale = marker.transform.localScale;

        float beatDuration = SongInfo.Instance.getSecondsPerBeat() * measuresPerShape;
        List<CoordinateCollider> points = coordinateGenerator.GetColliders();

        while (accumulatedTime < (beatDuration * SongInfo.Instance.getBeatsPerMeasure()))
        {
            accumulatedTime += Time.deltaTime;
            beatProgress = (accumulatedTime % beatDuration) / beatDuration;

            int beatsPassed = Mathf.FloorToInt(accumulatedTime / beatDuration);
            if (beatsPassed >= SongInfo.Instance.getBeatsPerMeasure())
            {
                beatsPassed = 0;
            }

            Vector3 previous, next;

            if (colorChange && beatProgress >= earlyThreshold)
            {
                marker.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
            } 
            else
            {
                marker.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            }

            if (beatsPassed < fullBeats)
            {
                previous = points[beatsPassed].transform.position;

                if (beatsPassed + 1 < points.Count )
                {
                    next = points[beatsPassed+1].transform.position;
                }
                else
                {
                    next = points[beatsPassed].transform.position;
                }

                if (lerpToPoint)
                {
                    marker.transform.position = Vector3.Lerp(previous, next, beatProgress);
                }
                else
                {
                    marker.transform.position = previous;
                    marker.transform.localScale = defaultMarkerScale * Mathf.Lerp(markerMaxScale, markerMinScale, beatProgress * measuresPerShape);
                }
            }
            else
            {
                if (beatProgress > 0.5)
                {
                    previous = points[beatsPassed+1].transform.position;
                    if (beatsPassed + 2 < points.Count )
                    {
                        next = points[beatsPassed+2].transform.position;
                    }
                    else
                    {
                        next = points[beatsPassed].transform.position;
                    }
                    
                    if (lerpToPoint)
                    {
                        marker.transform.position = Vector3.Lerp(previous, next, (beatProgress - 0.5F) * measuresPerShape);
                    }
                    else
                    {
                        marker.transform.position = previous;
                        marker.transform.localScale = defaultMarkerScale * Mathf.Lerp(markerMaxScale, markerMinScale, beatProgress * measuresPerShape);
                    }

                }
                else
                {
                    previous = points[beatsPassed].transform.position;
                    if (beatsPassed + 1 < points.Count )
                    {
                        next = points[beatsPassed+1].transform.position;
                    }
                    else
                    {
                        next = points[beatsPassed].transform.position;
                    }

                    if (lerpToPoint)
                    {
                        marker.transform.position = Vector3.Lerp(previous, next, beatProgress * measuresPerShape);
                    }
                    else
                    {
                        marker.transform.position = previous;
                        marker.transform.localScale = defaultMarkerScale * Mathf.Lerp(markerMaxScale, markerMinScale, beatProgress * measuresPerShape);
                    }
                }
            }

            PassLineInfo(previous, next);

            currentBeat = beatsPassed;

            yield return null;
        }

        Destroy(marker);
        marker = null;
    }

    private void PassLineInfo(Vector3 previous, Vector3 next)
    {
        Vector3 mouseLocation = new Vector3(pourTool.transform.position.x, previous.y, pourTool.transform.position.z);
        Vector3 mouseOffset = mouseLocation - marker.transform.position;
        Vector3 tangentVector = Vector3.Normalize(next - previous);
        Vector3 leftTangent = Vector3.Cross(tangentVector, Vector3.up);

        Vector3 frontProjection = Vector3.Project(mouseOffset, tangentVector);
        Vector3 leftProjection = Vector3.Project(mouseOffset, leftTangent);

        float frontDistance = frontProjection.magnitude;
        if (Vector3.Angle(frontProjection, tangentVector) > 90)
        {
            frontDistance *= -1;
        }
        float leftDistance = leftProjection.magnitude;
        if (Vector3.Angle(leftProjection, leftTangent) > 90)
        {
            leftDistance *= -1;
        }

        Vector2 markerSpace = new Vector2(frontDistance, -leftDistance);
        if (markerSpace.magnitude > maxDistance)
        {
            markerSpace = maxDistance * markerSpace.normalized;
        }

        Station.HandlePathUpdate(markerSpace);
    }

    public int GetCurrentBeat() { return currentBeat; }
}
