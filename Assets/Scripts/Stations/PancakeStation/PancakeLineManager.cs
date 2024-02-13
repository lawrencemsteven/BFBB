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
        int halfBeats = 2 * (totalBeats - SongInfo.Instance.getBeatsPerMeasure());
        int fullBeats = totalBeats - halfBeats;
        int index = 0;
        float accumulatedTime = 0F;

        marker = GameObject.Instantiate(markerPrefab, transform);

        while (index < totalBeats)
        {
            accumulatedTime += Time.deltaTime;
            if (index < fullBeats)
            {
                Vector3 previous = lineRenderer.GetPosition(index);
                Vector3 next = lineRenderer.GetPosition(index + 1);
                marker.transform.localPosition = Vector2.Lerp(previous, next, SongInfo.Instance.getBeatProgress());
            }
            yield return null;
        }

        GameObject.Destroy(marker);
        marker = null;
    }
}
