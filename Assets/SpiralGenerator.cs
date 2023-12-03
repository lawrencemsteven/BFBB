using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpiralGenerator : MonoBehaviour
{
    public ParticleSystem batter;
    public int segments = 50; // Number of segments in the spiral
    public float radiusIncrease = 0.0001f; // Rate at which the radius increases
    public float rotationSpeed = 5f; // Rotation speed
    public float colliderRadius = .01f;
    public List<SphereCollider> collidersList = new List<SphereCollider>();

    private LineRenderer lineRenderer;
    private MeshCollider meshCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        meshCollider = GetComponent<MeshCollider>();
        GenerateSpiral();
       // GenerateMeshCollider();
    }
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, .1f);
    }

    void GenerateSpiral()
    {
        lineRenderer.positionCount = segments;

        float angle = 0f;
        float radius = .05f;
        for (int i = 0; i < segments; i++)
        {
            float x = (Mathf.Cos(angle) * radius);
            float z = (Mathf.Sin(angle) * radius);
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            
            angle += Mathf.Deg2Rad * rotationSpeed;
            radius += radiusIncrease;
        }
    }
    /*
    private void GenerateMeshCollider()
    {
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, camera, true);
        meshCollider.sharedMesh = mesh;
    }
    */
}
