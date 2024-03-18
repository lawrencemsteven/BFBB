using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // All Camera Positions
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
    public float[] m_individualTransitionTime = new float[(int)CameraPoses.TOTAL_LENGTH];
    private float[] m_individualTransitionAmounts = new float[(int)CameraPoses.TOTAL_LENGTH];
    private int[] m_previousPosition = new int[(int)CameraPoses.TOTAL_LENGTH];
    private bool m_useGameCameras = false;


    // Camera Posing
    private bool m_poseTransitioning = false;
    public float m_poseTransitioningTime = 3.0f;
    private float m_poseTransitioningAmount = 0.0f;
    private CameraPoses m_previousCameraPose = CameraPoses.MAIN;
    private CameraPoses m_currentCameraPose = CameraPoses.MAIN;


    // All poses
    public enum CameraPoses
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
        // ICollection<string> test = AssetManager.GetAssetNames();
        // foreach (string testString in test)
        // {
        //     Debug.Log(testString);
        // }
        // Debug.Log(test);
    }

    void Update()
    {
        if (!m_poseTransitioning)
        {
            transform.SetPositionAndRotation(getPosePosition(m_currentCameraPose), getPoseRotation(m_currentCameraPose));
        }
        else
        {
            Vector3 oldPosition = getPosePosition(m_previousCameraPose);
            Vector3 newPosition = getPosePosition(m_currentCameraPose);

            Quaternion oldRotation = getPoseRotation(m_previousCameraPose);
            Quaternion newRotation = getPoseRotation(m_currentCameraPose);

            float lerpAmount = m_poseTransitioningAmount / m_poseTransitioningTime;
            newPosition = Vector3.Lerp(oldPosition, newPosition, lerpAmount);
            newRotation = Quaternion.Lerp(oldRotation, newRotation, lerpAmount);

            transform.SetPositionAndRotation(newPosition, newRotation);

            m_poseTransitioningAmount += Time.deltaTime;
            if (m_poseTransitioningAmount > m_poseTransitioningTime)
            {
                m_poseTransitioning = false;
            }
        }

        for (int i = 0; i < (int)CameraPoses.TOTAL_LENGTH; i++)
        {
            updatePoseValues((CameraPoses)i);
        }
    }

    public Vector3 getPosePosition(CameraPoses cameraPose)
    {
        Camera[] stationCameras = getStationCameras(cameraPose);

        if (stationCameras.Length == 1)
        {
            return stationCameras[0].transform.position;
        }

        Camera previous = stationCameras[m_previousPosition[(int)cameraPose]];
        Camera next = stationCameras[(m_previousPosition[(int)cameraPose] + 1) % stationCameras.Length];

        return Vector3.Lerp(previous.transform.position, next.transform.position, m_individualTransitionAmounts[(int)cameraPose] / m_individualTransitionTime[(int)cameraPose]);
    }

    public Quaternion getPoseRotation(CameraPoses cameraPose)
    {
        Camera[] stationCameras = getStationCameras(cameraPose);

        if (stationCameras.Length == 1)
        {
            return stationCameras[0].transform.rotation;
        }

        Camera previous = stationCameras[m_previousPosition[(int)cameraPose]];
        Camera next = stationCameras[(m_previousPosition[(int)cameraPose] + 1) % stationCameras.Length];

        return Quaternion.Lerp(previous.transform.rotation, next.transform.rotation, m_individualTransitionAmounts[(int)cameraPose] / m_individualTransitionTime[(int)cameraPose]);
    }

    public void updatePoseValues(CameraPoses cameraPose)
    {
        Camera[] stationCameras = getStationCameras(cameraPose);

        if (stationCameras.Length <= 1)
        {
            return;
        }

        m_individualTransitionAmounts[(int)cameraPose] += Time.deltaTime;
        if (m_individualTransitionAmounts[(int)cameraPose] >= m_individualTransitionTime[(int)cameraPose])
        {
            m_individualTransitionAmounts[(int)cameraPose] %= m_individualTransitionTime[(int)cameraPose];
            m_previousPosition[(int)cameraPose] = (m_previousPosition[(int)cameraPose] + 1) % stationCameras.Length;
        }
    }

    public void changeTarget(CameraPoses newCameraPose, float time)
    {
        m_poseTransitioning = true;
        m_poseTransitioningTime = time;
        m_poseTransitioningAmount = 0.0f;
        m_previousCameraPose = m_currentCameraPose;
        m_currentCameraPose = newCameraPose;
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

    public bool useGameCameras()
    {
        return m_useGameCameras;
    }
}
