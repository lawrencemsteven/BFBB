using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{
    public CameraController cameraController;
    public Transform m_background;
    public Transform m_BEAT;
    public Transform m_FLAVOR;
    public Transform m_BREAKFAST;
    public Transform m_B;
    public Transform m_O1;
    public Transform m_O2;
    public Transform m_G;
    public Transform m_I;
    public Transform m_E;

    // 0 - Pause
    // 1 - BEAT
    // 2 - Pause
    // 3 - FLAVOR
    // 4 - Pause
    // 5 - BREAKFAST
    // 6 - Pause
    // 7 - B
    // 8 - O1
    // 9 - O2
    // 10 - G
    // 11 - I
    // 12 - E
    // 13 - All Fade Out
    private int m_animationPhase = 0;
    private float m_currentTransitionAmount = 0.0f;

    public float[] m_animationTiming;

    void Update()
    {
        // Update Timer
        if (m_currentTransitionAmount >= m_animationTiming[m_animationPhase])
        {
            if (m_animationPhase >= 13)
            {
                cameraController.runAnimation();
                Destroy(gameObject);
            }
            m_animationPhase++;
            m_currentTransitionAmount = 0.0f;
        }

        m_currentTransitionAmount += Time.deltaTime;

        // I'm sorry :(
        switch (m_animationPhase)
        {
            case 1:
                m_BEAT.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 3:
                m_FLAVOR.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 5:
                m_BREAKFAST.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 7:
                m_B.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 8:
                m_O1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 9:
                m_O2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 10:
                m_G.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 11:
                m_I.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 12:
                m_E.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
            case 13:
                m_background.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_BEAT.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_FLAVOR.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_BREAKFAST.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_B.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_O1.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_O2.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_G.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_I.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                m_E.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, 1.0f - m_currentTransitionAmount / m_animationTiming[m_animationPhase]));
                break;
        }
    }
}
