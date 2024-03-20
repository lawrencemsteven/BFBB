using UnityEngine;
using UnityEngine.UI; 
using TMPro;// Import the UI namespace

public class JukeboxUI : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the UI Text component

    void Start()
    {
        // Access and modify the text property of the Text component
        uiText.text = "Boogie Slow"; // Set initial text
    }

    public void UpdateUI() {
        uiText.text = GlobalVariables.songUI;
    }
}
