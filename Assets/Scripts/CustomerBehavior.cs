using Unity.VisualScripting;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    private float patienceTimer = 15f;
    private float currentPatienceTimer;
    private bool active = false;
    private CustomerManager customerManager;
    private GameObject customerMesh;
    private GameObject patienceMeter;
    private Transform hats;
    private float patienceMeterMaxScale = 40f;
    private int moodCount = 5;
    private int currentMood;
    private float moodInterval;

    private void Start()
    {
        customerManager = transform.parent.GetComponent<CustomerManager>();
        customerMesh = transform.GetChild(0).gameObject;
        patienceMeter = customerMesh.transform.GetChild(0).gameObject;
        hats = customerMesh.transform.GetChild(1);
        moodInterval = patienceTimer / moodCount;
        DeactivateCustomer();
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }

        currentPatienceTimer -= Time.deltaTime;

        if (currentPatienceTimer <= 0)
        {
            DeactivateCustomer();
        }   

        UpdateMood();
        UpdatePatienceMeter();
    }

    public void ActivateCustomer()
    {
        customerMesh.SetActive(true);
        //generate hat
        SetHat();
        //generate order
        active = true;
        currentPatienceTimer = patienceTimer;
    }

    public void DeactivateCustomer()
    {
        customerMesh.SetActive(false);
        active = false;
    }

    private void UpdateMood()
    {
        currentMood = (int)Mathf.Ceil(currentPatienceTimer / moodInterval);
        Material moodMaterial = customerManager.GetMaterialFromMood(currentMood);
        customerMesh.GetComponent<MeshRenderer>().material = moodMaterial;
    }

    private void UpdatePatienceMeter()
    {
        float patiencePercent = currentPatienceTimer / patienceTimer;
        patienceMeter.transform.localScale = new Vector3(patienceMeterMaxScale * patiencePercent, patienceMeter.transform.localScale.y, patienceMeter.transform.localScale.z);
        Color meterColor = new Color(1 - patiencePercent, patiencePercent, 0);
        patienceMeter.GetComponent<MeshRenderer>().material.color = meterColor;
    }

    private void SetHat()
    {
        foreach(Transform hat in hats)
        {
            hat.gameObject.SetActive(false);
        }

        hats.GetChild(Random.Range(0, hats.childCount)).gameObject.SetActive(true);
    }

    public bool IsActive() { return active; }
}
