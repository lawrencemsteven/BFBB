using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoordinateGenerator))]
[RequireComponent(typeof(LineRenderer))]
public class PancakeLineManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;

    private CoordinateGenerator coordinateGenerator;
    private LineRenderer lineRenderer;
    private GameObject marker;
    private Coroutine beatFollower;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        coordinateGenerator = GetComponent<CoordinateGenerator>();
        coordinateGenerator.afterShapeGenerated.AddListener(DrawLine);
    }

    void Start()
    {
        lineRenderer.useWorldSpace = false;
    }

    public void DrawLine()
    {
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
        int index = 0;
        float accumulatedTime = 0F;
        float lastBeatTime = 0F;
        float beatProgress = 0F;

        marker = GameObject.Instantiate(markerPrefab, transform);

        float beatDuration = SongInfo.Instance.getSecondsPerBeat();

        while (accumulatedTime < (beatDuration * SongInfo.Instance.getBeatsPerMeasure()))
        {
            accumulatedTime += Time.deltaTime;

            float beatsPassed = Mathf.Floor(accumulatedTime / beatDuration);



            /*lastBeatTime = beatProgress;
            beatProgress = SongInfo.Instance.getBeatProgress();
            if (beatProgress < lastBeatTime)
            {
                index++;
                if (index >= fullBeats)
                {
                    index++;
                }
            }

            if (index < fullBeats)
            {
                Vector3 previous = lineRenderer.GetPosition(index);
                Vector3 next = lineRenderer.GetPosition(index + 1);
                marker.transform.localPosition = Vector2.Lerp(previous, next, beatProgress);
            } else
            {
                if (beatProgress < 0.5)
                {
                    Vector3 previous = lineRenderer.GetPosition(index);
                    Vector3 next = lineRenderer.GetPosition(index + 1);
                    marker.transform.localPosition = Vector2.Lerp(previous, next, beatProgress * 2);
                } else
                {
                    Vector3 previous = lineRenderer.GetPosition(index + 1);
                    Vector3 next = lineRenderer.GetPosition(index + 2);
                    marker.transform.localPosition = Vector2.Lerp(previous, next, (beatProgress - 0.5F) * 2);
                }
            }*/
            yield return null;
        }

        GameObject.Destroy(marker);
        marker = null;
    }
}
