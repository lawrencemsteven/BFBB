using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumAnimator : MonoBehaviour
{
    public Transform[] m_cds;

    private float m_startX;
    private Quaternion m_startRot;
    private float m_endX;
    private Quaternion m_endRot;

    private bool m_transitioning = false;
    public float m_transitionTime = 1.0f;
    private float m_transitionAmount = 0.0f;
    private uint m_target = 0;
    private uint m_current = 0;

    // Left most Z = 0.08;
    // Right most Z = 0.16;
    // transition Z amount = 0.04;
    // 5 - min
    void Start()
    {
        m_startX = m_cds[0].localPosition.x;
        m_startRot = m_cds[0].localRotation;
        m_endX = m_cds[m_cds.Length - 1].localPosition.x;
        m_endRot = m_cds[m_cds.Length - 1].localRotation;
        setTarget(0);
    }

    void Update()
    {
        if (m_transitioning)
        {
            m_transitionAmount += Time.deltaTime;

            float lerpAmount = Interpolawrence.Lerp(Interpolawrence.InterpolawrenceSpeeds.Slow, Interpolawrence.InterpolawrenceSpeeds.Slow, Math.Min(m_transitionAmount / m_transitionTime, 1));
            lerpAmount = m_current < m_target ? 1.0f - lerpAmount : lerpAmount;
            lerpAmount = Math.Clamp(lerpAmount, 0.0f, 1.0f);

            uint actualCurrent = m_current;
            if (m_current > m_target)
            {
                actualCurrent -= 1;
            }

            m_cds[5 + actualCurrent].SetLocalPositionAndRotation(new Vector3(Mathf.Lerp(m_startX, m_endX, lerpAmount), m_cds[5 + actualCurrent].localPosition.y, Mathf.Lerp(0.08f + 0.04f * (actualCurrent + 1.0f), 0.04f + 0.04f * actualCurrent, lerpAmount)), Quaternion.Lerp(m_startRot, m_endRot, lerpAmount));
            if (m_transitionAmount >= m_transitionTime)
            {
                if (m_current < m_target)
                {
                    m_current++;
                }
                else
                {
                    m_current--;
                }
                m_transitionAmount = 0.0f;
                if (m_current == m_target)
                {
                    m_transitioning = false;
                }
            }
        }
    }

    private void setTarget(uint target)
    {
        if (target == m_target)
        {
            return;
        }

        m_target = target;
        m_transitioning = true;
    }

    public void incrementTarget()
    {
        if (m_target < 3)
        {
            setTarget(m_target + 1);
        }
    }

    public void decrementTarget()
    {
        if (m_target > 0)
        {
            setTarget(m_target - 1);
        }
    }
}
