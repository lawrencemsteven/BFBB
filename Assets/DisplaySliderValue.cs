using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    public Slider slider;

    void Start()
    {
        m_TextComponent = GetComponent<TMP_Text>();
        slider.onValueChanged.AddListener(UpdateSliderValueText);
        UpdateSliderValueText(slider.value);
    }
    void UpdateSliderValueText(float value)
    {
        m_TextComponent.text = value.ToString("F2");
    }
}
