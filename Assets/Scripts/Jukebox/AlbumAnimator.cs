using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumAnimator : MonoBehaviour
{
    public Transform[] m_cds;

    private float startX;
    private float startZ;
    private float endX;
    private float endZ;

    void Start()
    {
        startX = m_cds[0].position.x;
        startZ = m_cds[0].position.z;
        endX = m_cds[m_cds.Length - 1].position.x;
        endZ = m_cds[m_cds.Length - 1].position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
