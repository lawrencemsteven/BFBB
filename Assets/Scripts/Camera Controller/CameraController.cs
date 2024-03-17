using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera[] m_mainCameras;
    public Camera[] m_jukeboxCameras;
    public Camera[] m_boothCameras;
    public Camera[] m_tableCameras;
    public Camera[] m_counterCameras;
    public Camera[] m_stoolCameras;
    public Camera[] m_wallCameras;
    public Camera[] m_floorCameras;
    public Camera[] m_dishStationCameras;
    public Camera[] m_griddleStationCameras;
    public Camera[] m_prepStationCameras;
    public Camera[] m_tableAccessoryCameras;
    public Camera[] m_windowCameras;
    public Camera[] m_lightCameras;

    public int m_numMainOutsideCameras;
    private int m_numOutsideCamerasCounter;
    public float m_individualTransitionTime = 3.0f;

    private bool m_useGameCameras = false;

    private int m_currentCamera = 0;
    private int m_nextCamera = 1;
    private float m_currentTransitionAmount = 0.0f;

    private Vector3[] animatedCameraPositions;
    private Quaternion[] animatedCameraRotations;


    private enum CameraPoses
    {
        MAIN,
        JUKEBOX,
        BOOTH,
        TABLE,
        COUNTER,
        STOOLS,
        WALLS,
        FLOORS,
        DISH_STATION,
        GRIDDLE_STATION,
        PREP_STATION,
        TABLE_ACCESSORIES,
        WINDOWS,
        LIGHTS,



        TOTAL_LENGTH
    }

    void Start()
    {
        animatedCameraPositions = new Vector3[(int)CameraPoses.TOTAL_LENGTH];
        animatedCameraRotations = new Quaternion[(int)CameraPoses.TOTAL_LENGTH];
    }

    void Update()
    {
        animatedCameraPositions[(int)CameraPoses.MAIN] = Vector3.Lerp(m_mainCameras[m_currentCamera].transform.position, m_mainCameras[m_nextCamera].transform.position, m_currentTransitionAmount / m_individualTransitionTime);
        animatedCameraRotations[(int)CameraPoses.MAIN] = Quaternion.Lerp(m_mainCameras[m_currentCamera].transform.rotation, m_mainCameras[m_nextCamera].transform.rotation, m_currentTransitionAmount / m_individualTransitionTime);
        m_currentTransitionAmount += Time.deltaTime;
        if (m_currentTransitionAmount >= m_individualTransitionTime)
        {
            m_currentCamera = (m_currentCamera + 1) % m_mainCameras.Length;
            m_nextCamera = (m_nextCamera + 1) % m_mainCameras.Length;
            m_currentTransitionAmount = 0.0f;
            if (m_numOutsideCamerasCounter < m_numMainOutsideCameras - 1)
            {
                m_numOutsideCamerasCounter += 1;
            }
            else
            {
                m_currentCamera = Math.Max(m_currentCamera, m_numMainOutsideCameras);
                m_nextCamera = Math.Max(m_nextCamera, m_numMainOutsideCameras);
            }
        }

        transform.SetPositionAndRotation(animatedCameraPositions[(int)CameraPoses.MAIN], animatedCameraRotations[(int)CameraPoses.MAIN]);
    }

    public bool useGameCameras()
    {
        return m_useGameCameras;
    }

    private Camera[] getStationCameras(CameraPoses cameraPose)
    {
        // Unity UI does not support 2D arrays so this is the best solution that I could come up with.
        // If it ain't broke, don't fix it
        // P.S. This did cause me A LOT of pain to write - Steven

        switch (cameraPose)
        {
            case CameraPoses.MAIN:
                return m_mainCameras;
            case CameraPoses.JUKEBOX:
                return m_jukeboxCameras;
            case CameraPoses.BOOTH:
                return m_boothCameras;
            case CameraPoses.TABLE:
                return m_tableCameras;
            case CameraPoses.COUNTER:
                return m_counterCameras;
            case CameraPoses.STOOLS:
                return m_stoolCameras;
            case CameraPoses.WALLS:
                return m_wallCameras;
            case CameraPoses.FLOORS:
                return m_floorCameras;
            case CameraPoses.DISH_STATION:
                return m_dishStationCameras;
            case CameraPoses.GRIDDLE_STATION:
                return m_griddleStationCameras;
            case CameraPoses.PREP_STATION:
                return m_prepStationCameras;
            case CameraPoses.TABLE_ACCESSORIES:
                return m_tableAccessoryCameras;
            case CameraPoses.WINDOWS:
                return m_windowCameras;
            case CameraPoses.LIGHTS:
                return m_lightCameras;
        }
        return null;
    }
}
