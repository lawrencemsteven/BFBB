using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopAssetManager : MonoBehaviour
{
    public Button m_pinkButton;
    public Button m_orangeButton;
    public Button m_greenButton;
    public Button m_purpleButton;
    public Button m_cyberpunkButton;
    public Button m_spaceButton;
    public Button m_seaButton;
    public Button m_defaultButton;
    private ShopSections m_selectedSection;

    public enum ShopSections
    {
        Lights,
        Condiments,
        WallDiamonds,
        Doors,
        Windows,
        Counter,
        Stools,
        Tables,
        Seats,
        Sink,
        Floor,
        Ceiling,
        Walls,
        PrepStation,
        WallArt,
        Background,
        GriddleStation,

    }

    public enum ShopStyles
    {
        Default,
        Pink,
        Orange,
        Green,
        Purple,
        Cyberpunk,
        Space,
        Sea,
    }

    void Start()
    {
        ICollection<string> test = AssetManager.GetAssetNames();
        foreach (string testString in test)
        {
            Debug.Log(testString);
            foreach (string testString2 in AssetManager.GetAvailableSwapsForAsset(testString))
            {
                Debug.Log(testString2);
            }
        }
        Debug.Log(test);
    }

    public void setSection(ShopSections shopSection)
    {
        m_selectedSection = shopSection;
    }

    public void activateButtons()
    {
        ICollection<string> styles = AssetManager.GetAvailableSwapsForAsset(convertSectionToString(m_selectedSection));
        foreach (string style in styles)
        {
            switch (style)
            {
                case "Default":
                    m_defaultButton.interactable = true;
                    break;
                case "Pink":
                    m_pinkButton.interactable = true;
                    break;
                case "Orange":
                    m_orangeButton.interactable = true;
                    break;
                case "Green":
                    m_greenButton.interactable = true;
                    break;
                case "Purple":
                    m_purpleButton.interactable = true;
                    break;
                case "Cyberpunk":
                    m_cyberpunkButton.interactable = true;
                    break;
                case "Space":
                    m_spaceButton.interactable = true;
                    break;
                case "Sea":
                    m_seaButton.interactable = true;
                    break;
            }
        }
    }
    public void setStyle(ShopStyles style)
    {
        if (m_selectedSection == ShopSections.Walls)
        {
            AssetManager.ApplyAssetSwap(convertSectionToString(ShopSections.Doors), convertStyleToString(style));
        }
        else if (m_selectedSection == ShopSections.Floor)
        {
            AssetManager.ApplyAssetSwap(convertSectionToString(ShopSections.Ceiling), convertStyleToString(style));
        }

        AssetManager.ApplyAssetSwap(convertSectionToString(m_selectedSection), convertStyleToString(style));
    }

    private string convertSectionToString(ShopSections shopSection)
    {
        switch (shopSection)
        {
            case ShopSections.Lights: return "Lights";
            case ShopSections.Condiments: return "Condiments";
            case ShopSections.WallDiamonds: return "Wall Diamonds";
            case ShopSections.Doors: return "Doors";
            case ShopSections.Windows: return "Windows";
            case ShopSections.Counter: return "Counter";
            case ShopSections.Stools: return "Stools";
            case ShopSections.Tables: return "Tables";
            case ShopSections.Seats: return "Seats";
            case ShopSections.Sink: return "Sink";
            case ShopSections.Floor: return "Floor";
            case ShopSections.Ceiling: return "Ceiling";
            case ShopSections.Walls: return "Walls";
            case ShopSections.PrepStation: return "Prep Station";
            case ShopSections.WallArt: return "Wall Art";
            case ShopSections.Background: return "Background";
            case ShopSections.GriddleStation: return "Griddle Station";
        }
        return null;
    }

    private string convertStyleToString(ShopStyles shopStyle)
    {
        switch (shopStyle)
        {
            case ShopStyles.Default: return "Default";
            case ShopStyles.Pink: return "Pink";
            case ShopStyles.Orange: return "Orange";
            case ShopStyles.Green: return "Green";
            case ShopStyles.Purple: return "Purple";
            case ShopStyles.Cyberpunk: return "Cyberpunk";
            case ShopStyles.Space: return "Space";
            case ShopStyles.Sea: return "Sea";
        }
        return null;
    }
}