using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera[] m_allCameras;
    public int m_numOutsideCameras;
    private int m_numOutsideCamerasCounter;
    public float m_individualTransitionTime = 3.0f;

    private bool m_useGameCameras = false;

    private int m_currentCamera = 0;
    private int m_nextCamera = 1;
    private float m_currentTransitionAmount = 0.0f;

    void Update()
    {
        transform.SetPositionAndRotation(Vector3.Slerp(m_allCameras[m_currentCamera].transform.position, m_allCameras[m_nextCamera].transform.position, m_currentTransitionAmount / m_individualTransitionTime), Quaternion.Slerp(m_allCameras[m_currentCamera].transform.rotation, m_allCameras[m_nextCamera].transform.rotation, m_currentTransitionAmount / m_individualTransitionTime));
        m_currentTransitionAmount += Time.deltaTime;
        if (m_currentTransitionAmount >= m_individualTransitionTime)
        {
            m_currentCamera = (m_currentCamera + 1) % m_allCameras.Length;
            m_nextCamera = (m_nextCamera + 1) % m_allCameras.Length;
            m_currentTransitionAmount = 0.0f;
            if (m_numOutsideCamerasCounter < m_numOutsideCameras - 1)
            {
                m_numOutsideCamerasCounter += 1;
            }
            else
            {
                m_currentCamera = Math.Max(m_currentCamera, m_numOutsideCameras);
                m_nextCamera = Math.Max(m_nextCamera, m_numOutsideCameras);
            }
        }
    }

    public bool useGameCameras()
    {
        return m_useGameCameras;
    }
}
