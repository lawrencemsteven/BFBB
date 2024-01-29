using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleBatterPour : MonoBehaviour
{
    public float bpm = 135f;
    public GameObject batterFill;
    public Animator waffleMakerAnim;
    public fmodTimer timer;
    public bool isClosed = false;
    public int positionClosed;
    private bool isPouring = false;
    private Vector3 initialBatter;
    private float timePouring = 0, posBatterStarted, elapsedTime, accuracy1, accuracy2, timeBetweenPours, speedOfBatterPour;

    // Start is called before the first frame update
    void Start()
    {
        speedOfBatterPour = (1.7623f - 1.749f) / (4.0f / (bpm / 60f));
        initialBatter = batterFill.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPouring)
        {
            batterFill.transform.position += Vector3.up * speedOfBatterPour * Time.deltaTime;
            timePouring += Time.deltaTime;
        }
        elapsedTime += Time.deltaTime;
        timeBetweenPours += Time.deltaTime;
        if ((isPouring && timeBetweenPours > 0.1) && (timePouring > (2.0f/(bpm/60f))))
        {
            isPouring = false;
            timePouring = 0;
            accuracy2 = Mathf.Abs((timer.position - posBatterStarted) - timer.positionBarLength);
            if (accuracy2 < 100)
            {
                GlobalVariables.score += 1;
            }
            waffleMakerAnim.SetTrigger("Close");
            positionClosed = timer.position;
            isClosed = true;
        }
        else if (isPouring && timeBetweenPours > 0.1) {
            isPouring = false;
            timePouring = 0;
        }
    }

    public void ResetBatter()
    {
        batterFill.transform.position = initialBatter;
    }

    private void OnParticleTrigger()
    {
        if ((!isPouring))
        {
            if (timer.OnBar())
            {
                GlobalVariables.score += 1;
                posBatterStarted = timer.position;
            }
        }
        isPouring = true;
        timeBetweenPours = 0;
    }
}
