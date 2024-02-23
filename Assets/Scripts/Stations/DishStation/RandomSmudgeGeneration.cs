using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSmudgeGeneration : MonoBehaviour
{
    public GameObject Smudge1;
    public GameObject Smudge2;
    public GameObject plate;
    public GameObject smudgePrefab;
    public float radius;
    public float diameter;
    public float segmentLength;
    public float plateY;
    public float plateX;
    public float plateZ;
    public float smudgeZ;
    public float yPosTop;
    public float yPosBottom;
    public float yPosBottom2;
    public float yPosTop2;
    public float[] yPositions;
    
    void Start() {
        setup();
        generateSmudges(yPositions, Smudge1);
        generateSmudges(yPositions, Smudge2);
    }
    public void setup() {
        //diameter = (plate.transform.localScale.z / 2); This should be how to do it, but it doesn't look right
        diameter = 0.6f;
        radius = diameter/2;
        segmentLength = diameter/4;
        plateY = plate.transform.position.y;
        plateX = plate.transform.position.x;
        plateZ = plate.transform.position.z;
        smudgeZ = plateZ - 0.011f;
        yPosTop = plateY + radius;
        yPosBottom = plateY - radius;
        //This will give you 4 of these, but more need to be added for other rhythms
        yPosBottom2 = plateY - segmentLength;
        yPosTop2 = plateY + segmentLength;
        yPositions = new[] {yPosTop, yPosTop2, yPosBottom2, yPosBottom};
    }

    //Generates the length of a line across the plate at y position chordY factoring in plateY as the y position of the center of the plate
    public float getChordLength(float chordY) {
        float chordDistance;
        if (chordY > plateY) {
            chordDistance = chordY - plateY;
        }
        else {
            chordDistance = plateY - chordY;
        }
        float chordLength = Mathf.Sqrt(((radius * radius) - (chordDistance * chordDistance)));
        return chordLength;
    }

    // Generates a random X position on the plate for a given y position
    public float generateXPos(float yPos) {
        float chordLen = getChordLength(yPos);
        float topRange = plateX + (chordLen/2);
        float bottomRange = plateX - (chordLen/2);
        float xPos = Random.Range(bottomRange, topRange);
        return xPos;
    }

    // generates smudges off of providedy positions
    public void generateSmudges(float[] yPosArray, GameObject parent) {
        int len = yPosArray.Length;
        int i = 0;
        while (i < len) {
            float xPos = generateXPos(yPosArray[i]);
            Vector3 smudgePos = new Vector3(xPos, yPosArray[i], smudgeZ);
            Instantiate(smudgePrefab, smudgePos, Quaternion.Euler(new Vector3(90, 0, 0)), parent.transform);
            i++;
        }
    }
}
