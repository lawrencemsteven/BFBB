using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleBatterPour : MonoBehaviour
{
    public float bpm = 135f;
    public GameObject batterFill;
    public Animator waffleMakerAnim;
    private bool isPouring = false, isClosed = false;
    private float timeToPourBatter, timeToStopBatter, elapsedTime, accuracy1, accuracy2, timeBetweenPours, speedOfBatterPour;

    // Start is called before the first frame update
    void Start()
    {
        speedOfBatterPour = (1.7623f - 1.749f) / (4.0f / (bpm / 60f));
        timeToStopBatter = (24 / bpm) * 60;
        timeToPourBatter = (20 / bpm) * 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= timeToPourBatter)
        {
            //Debug.Log("NOWWW");
        }
        if (isPouring)
        {
            batterFill.transform.position += Vector3.up * speedOfBatterPour * Time.deltaTime;
        }
        elapsedTime += Time.deltaTime;
        timeBetweenPours += Time.deltaTime;
        if (isPouring && timeBetweenPours > 0.045)
        {
            isPouring = false;
            accuracy2 = Mathf.Abs(elapsedTime - timeToStopBatter);
            if (accuracy2 < .4)
            {
                GlobalVariables.score += 1;
            }
        }
        if ((elapsedTime >= timeToStopBatter) && (!isClosed))
        {
            waffleMakerAnim.SetTrigger("Close");
            isClosed = true;
            
        }
    }

    private void OnParticleTrigger()
    {
        if (!isPouring)
        {
            accuracy1 = Mathf.Abs(elapsedTime - timeToPourBatter);
            if (accuracy1 < .4)
            {
                GlobalVariables.score += 1;
            }
        }
        isPouring = true;
        timeBetweenPours = 0;
    }
}
