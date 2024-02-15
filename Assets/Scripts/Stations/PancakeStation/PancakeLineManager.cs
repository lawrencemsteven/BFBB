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
        int index = 0;
        float accumulatedTime = 0F;
        float lastBeatTime = 0F;
        float beatProgress = 0F;

        marker = GameObject.Instantiate(markerPrefab, transform);

        float beatDuration = SongInfo.Instance.getSecondsPerBeat();
        List<CoordinateCollider> points = coordinateGenerator.GetColliders();

        while (accumulatedTime < (beatDuration * SongInfo.Instance.getBeatsPerMeasure()))
        {
            accumulatedTime += Time.deltaTime;
            beatProgress = SongInfo.Instance.getBeatProgress();

            int beatsPassed = (int)SongInfo.Instance.getBeatsPassed();
            if (beatsPassed >= SongInfo.Instance.getBeatsPerMeasure())
            {
                beatsPassed = 0;
            }

            if (beatsPassed < fullBeats)
            {
                beatProgress = SongInfo.Instance.getBeatProgress();
                Vector3 previous = points[beatsPassed].transform.position;
                Vector3 next = points[beatsPassed+1].transform.position;
                marker.transform.position = Vector3.Lerp(next, previous, beatProgress);
            } else
            {
                if (beatProgress > 0.5)
                {
                    Vector3 previous = points[beatsPassed].transform.position;
                    Vector3 next = points[beatsPassed+1].transform.position;
                    marker.transform.position = Vector3.Lerp(next, previous, (beatProgress - 0.5F) * 2);
                } else
                {
                    Vector3 previous = points[beatsPassed+1].transform.position;
                    Vector3 next = points[beatsPassed+2].transform.position;
                    marker.transform.position = Vector3.Lerp(next, previous, beatProgress * 2);
                }
            }

            yield return null;
        }

        GameObject.Destroy(marker);
        marker = null;
    }
}
