using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayScript : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private SwitchCamera switchCamera;
    public GameObject m_moneyDisplay;

    private float m_animationAmount = 0.0f;
    private float m_animationTime = 2.0f;
    private bool m_animating = false;

    public void Update()
    {
        if (m_animating)
        {
            m_animationAmount += Time.deltaTime;
            float lerpAmount = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Slow, m_animationAmount / m_animationTime);
            lerpAmount = Mathf.Lerp(1620.0f, 540.0f, lerpAmount);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, lerpAmount, gameObject.transform.position.z);
            if (m_animationAmount >= m_animationTime)
            {
                m_animating = false;
            }
        }
    }

    public void showDisplay(float time = 2.0f)
    {
        m_animationTime = time;
        m_animating = true;
    }

    public void displayMoney(int money)
    {
        money = Math.Max(money, 0);
        m_moneyDisplay.GetComponent<TextMeshPro>().text = "$" + money;
    }

    public void continueButton()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1620.0f, gameObject.transform.position.z);
        cameraController.SetUseGameCameras(false);
        mainMenuButtons.SetActive(true);
        switchCamera.DeactivateAll();
        Cursor.visible = true;
        mainMenuButtons.GetComponent<MainMenuButtons>().backToMainMenu();
    }
}
