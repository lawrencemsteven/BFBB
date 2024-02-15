using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
[RequireComponent(typeof(LineRenderer))]
public class PancakeLineManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject pourTool;
    [SerializeField] private float maxDistance = 0.3F;

    public GameObject debugMarker;

    private CoordinateGenerator coordinateGenerator;
    private LineRenderer lineRenderer;
    private GameObject marker;
    private Coroutine beatFollower;
    private bool readyForNewMeasure = true;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        coordinateGenerator = GetComponent<CoordinateGenerator>();
        SongInfo.Instance.onMeasure.AddListener(NewMeasure);
    }

    void Start()
    {
        lineRenderer.useWorldSpace = false;
        NewMeasure();
    }

    public void NewMeasure()
    {
        if (readyForNewMeasure)
        {
            readyForNewMeasure = false;
            DrawLine();
        } else
        {
            readyForNewMeasure = true;
        }
    }

    public void DrawLine()
    {
        coordinateGenerator.GenerateShape();
        GameObject.Destroy(marker);
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

        float beatDuration = SongInfo.Instance.getSecondsPerBeat() * 2;
        List<CoordinateCollider> points = coordinateGenerator.GetColliders();

        while (accumulatedTime < (beatDuration * SongInfo.Instance.getBeatsPerMeasure()))
        {
            accumulatedTime += Time.deltaTime;
            beatProgress = accumulatedTime % beatDuration;

            int beatsPassed = Mathf.FloorToInt(accumulatedTime / beatDuration);
            if (beatsPassed >= SongInfo.Instance.getBeatsPerMeasure())
            {
                beatsPassed = 0;
            }

            Vector3 previous, next;
            if (beatsPassed < fullBeats)
            {
                previous = points[beatsPassed].transform.position;
                next = points[beatsPassed+1].transform.position;
                marker.transform.position = Vector3.Lerp(previous, next, beatProgress);
            } else
            {
                if (beatProgress > 0.5)
                {
                    previous = points[beatsPassed+1].transform.position;
                    next = points[beatsPassed+2].transform.position;
                    marker.transform.position = Vector3.Lerp(previous, next, (beatProgress - 0.5F) * 2);
                } else
                {
                    previous = points[beatsPassed].transform.position;
                    next = points[beatsPassed+1].transform.position;
                    marker.transform.position = Vector3.Lerp(previous, next, beatProgress * 2);
                }
            }

            PassLineInfo(previous, next);

            yield return null;
        }

        GameObject.Destroy(marker);
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
        if (Vector3.Angle(leftProjection, tangentVector) > 90)
        {
            leftDistance *= -1;
        }

        debugMarker.transform.position = marker.transform.position + new Vector3(-leftDistance, 0, frontDistance);

        Station.HandlePathUpdate(new Vector2(frontDistance, -leftDistance));
    }
}
