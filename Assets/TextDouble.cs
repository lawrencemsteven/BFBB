using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDouble : MonoBehaviour
{   
    public TextMeshProUGUI textToDouble;
    TextMeshProUGUI thisTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        thisTextMesh = this.GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(thisTextMesh.text != textToDouble.text) thisTextMesh.text = textToDouble.text;
    }
}
