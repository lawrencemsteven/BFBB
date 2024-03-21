using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
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
    public Button m_purchaseButton;
    public GameObject m_cost;

    private int[] allAssetCosts = new int[18 * 8];
    private ShopStyles[] m_currAppliedAssets = new ShopStyles[18];
    private ShopStyles[] m_tempAppliedAssets = new ShopStyles[18];

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
        All,

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
        m_cost.GetComponent<TextMeshProUGUI>().text = "";
        for (int i = 0; i < 17; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                allAssetCosts[i * 8 + j] = 0;
                if (AssetManager.GetAvailableSwapsForAsset(convertSectionToString((ShopSections)i)).Contains(convertStyleToString((ShopStyles)j)))
                {
                    allAssetCosts[i * 8 + j] = 20;
                    allAssetCosts[17 * 8 + j] += 20;
                }
                else
                {
                    allAssetCosts[i * 8 + j] = -1;
                }
            }
        }
        for (int i = 0; i < 18; i++)
        {
            m_currAppliedAssets[i] = ShopStyles.Default;
            m_tempAppliedAssets[i] = ShopStyles.Default;
        }
    }

    public int getStyleCost(ShopSections shopSection, ShopStyles shopStyle)
    {
        return allAssetCosts[(int)shopSection * 8u + (int)shopStyle];
    }

    public bool purchaseStyle(ShopSections shopSection, ShopStyles shopStyle)
    {
        int cost = getStyleCost(shopSection, shopStyle);
        if (MoneyManager.getMoney() >= cost)
        {
            m_cost.GetComponent<TextMeshProUGUI>().text = "";
            m_purchaseButton.interactable = false;
            MoneyManager.addMoney(-((int)cost));
            if (shopSection != ShopSections.All)
            {
                allAssetCosts[(int)ShopSections.All * 8u + (int)shopStyle] -= allAssetCosts[(int)shopSection * 8u + (int)shopStyle];
                allAssetCosts[(int)shopSection * 8u + (int)shopStyle] = 0;
                m_currAppliedAssets[(int)shopSection] = shopStyle;
                m_tempAppliedAssets[(int)shopSection] = shopStyle;
            }
            else
            {
                for (int i = 0; i < 18; i++)
                {
                    if (allAssetCosts[i * 8 + (int)shopStyle] != -1)
                    {
                        allAssetCosts[i * 8 + (int)shopStyle] = 0;
                        m_currAppliedAssets[i] = shopStyle;
                        m_tempAppliedAssets[i] = shopStyle;
                    }
                }
            }
        }
        return false;
    }

    public void setSection(ShopSections shopSection)
    {
        m_selectedSection = shopSection;
    }

    public void activateButtons()
    {
        if (m_selectedSection == ShopSections.All)
        {
            m_defaultButton.interactable = true;
            m_pinkButton.interactable = true;
            m_orangeButton.interactable = true;
            m_greenButton.interactable = true;
            m_purpleButton.interactable = true;
            m_cyberpunkButton.interactable = true;
            m_spaceButton.interactable = true;
            m_seaButton.interactable = true;
        }

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
    public void backButtonClicked()
    {
        if (m_tempAppliedAssets[(int)m_selectedSection] != m_currAppliedAssets[(int)m_selectedSection])
        {
            setStyle(m_currAppliedAssets[(int)m_selectedSection]);
        }
        m_purchaseButton.interactable = false;
        m_cost.GetComponent<TextMeshProUGUI>().text = "";
    }
    public void setStyle(ShopStyles style)
    {
        if (m_selectedSection == ShopSections.All && getStyleCost(m_selectedSection, style) != 0)
        {
            m_purchaseButton.interactable = true;
            m_cost.GetComponent<TextMeshProUGUI>().text = "$" + getStyleCost(m_selectedSection, style);
            for (int i = 0; i < 18; i++)
            {
                if (getStyleCost((ShopSections)i, style) != -1)
                {
                    m_tempAppliedAssets[i] = style;
                }
            }
        }
        else if (getStyleCost(m_selectedSection, style) != 0)
        {
            m_tempAppliedAssets[(int)m_selectedSection] = style;
            m_purchaseButton.interactable = true;
            m_cost.GetComponent<TextMeshProUGUI>().text = "$" + getStyleCost(m_selectedSection, style);
        }
        else
        {
            if (m_selectedSection == ShopSections.All)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (getStyleCost((ShopSections)i, style) != -1)
                    {
                        m_currAppliedAssets[i] = style;
                        m_tempAppliedAssets[i] = style;
                    }
                }
            }
            else
            {
                m_currAppliedAssets[(int)m_selectedSection] = style;
                m_tempAppliedAssets[(int)m_selectedSection] = style;
            }
            m_purchaseButton.interactable = false;
            m_cost.GetComponent<TextMeshProUGUI>().text = "";
        }

        if (m_selectedSection == ShopSections.All)
        {
            for (int i = 0; i < (int)ShopSections.All; i++)
            {
                AssetManager.ApplyAssetSwap(convertSectionToString((ShopSections)i), convertStyleToString(style));
            }
            return;
        }

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
    public void purchaseButton()
    {
        if (purchaseStyle(m_selectedSection, m_tempAppliedAssets[(int)m_selectedSection]))
        {
            m_purchaseButton.interactable = false;
        }
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
