using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayScript : MonoBehaviour
{
    public Text m_moneyDisplay;

    private float m_animationAmount = 0.0f;
    private float m_animationTime = 2.0f;
    private bool m_animating = false;

    public void Update()
    {
        if (m_animating)
        {
            m_animationAmount += Time.deltaTime;
            float lerpAmount = Mathf.Lerp(-1080.0f, 0.0f, m_animationAmount / m_animationTime);
            lerpAmount = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Slow, lerpAmount);
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
        m_moneyDisplay.text = "$" + money;
    }

    public void continueButton()
    {
        // Hopefully reloads the scene?
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
