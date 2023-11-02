using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_1;
    public GameObject Camera_2;
    public GameObject Camera_3;
    public GameObject Camera_4;
    public int CamState;

    void Awake()
    {
        CamState = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && CamState != 0)
        {
            Cam_1();
            CamState = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && CamState != 1)
        {
            Cam_2();
            CamState = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && CamState != 2)
        {
            Cam_3();
            CamState = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && CamState != 3)
        {
            Cam_4();
            CamState = 3;
        }
    }

    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }
    public void ManageCamera()
    {
        if(CamState == 0)
        {
            Cam_1();
            CamState = 1;
        }
        else if(CamState == 1)
        {
            Cam_2();
            CamState = 2;
        }
        else if (CamState == 2)
        {
            Cam_3();
            CamState = 3;
        }
        else
        {
            Cam_4();
            CamState = 0;
        }
    }

    void Cam_1()
    {
        Camera_1.SetActive(true);
        Camera_2.SetActive(false);
        Camera_3.SetActive(false);
        Camera_4.SetActive(false);
    }

    void Cam_2()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(true);
        Camera_3.SetActive(false);
        Camera_4.SetActive(false);
    }

    void Cam_3()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        Camera_3.SetActive(true);
        Camera_4.SetActive(false);
    }

    void Cam_4()
    {
        Camera_1.SetActive(false);
        Camera_2.SetActive(false);
        Camera_3.SetActive(false);
        Camera_4.SetActive(true);
    }
}
