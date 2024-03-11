using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeCursorManager : MonoBehaviour
{
    private GameObject spatula, batter;
    // Start is called before the first frame update
    void Start()
    {
        spatula = transform.GetChild(0).gameObject;
        batter = transform.GetChild(1).gameObject;
        batter.SetActive(true);
        spatula.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            batter.SetActive(false);
            spatula.SetActive(true);
        }
        else
        {
            batter.SetActive(true);
            spatula.SetActive(false);
        }
    }
}
