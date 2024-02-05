using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatInputMouse : MonoBehaviour
{
    // A series of calculations to get the number of fixed updates per beat
    float beatsPerMinute = 0f;
    float beatsPerSecond = 0f;
    float secondsPerBeat = 0f;
    float fixedUpdatePerBeat = 0f;
    float fixedUpdateCounter = 0f;
    float beatFraction = 0f;
    public Vector3 storedMousePosition = new Vector3(0,0,0);
    public void storeMousePosition() {
        storedMousePosition = Input.mousePosition;
    }

    public Vector3 getStoredMouseDelta() {
        Vector3 currentMousePosition = Input.mousePosition;
        return new Vector3((currentMousePosition.x - storedMousePosition.x), (currentMousePosition.y - storedMousePosition.y), (currentMousePosition.z - storedMousePosition.z));
    }

    public Vector3 getMouseDelta(Vector3 oldMousePosition) {
        Vector3 currentMousePosition = Input.mousePosition;
        return new Vector3((currentMousePosition.x - oldMousePosition.x), (currentMousePosition.y - oldMousePosition.y), (currentMousePosition.z - oldMousePosition.z));
    }

// uses InvokeRepearing and a beats per minute value to call a method on every beat
    public void beatUpdate(string methodCall, float bpm) {
        beatsPerSecond = bpm/60;
        InvokeRepeating(methodCall, 0f, beatsPerSecond);
    }
// Calculates the number of fixed updates per beat based on bpm, sets up values for fixedUpdate
    public void fieldSetup(float bpm) {
        beatsPerMinute = bpm;
        beatsPerSecond = beatsPerMinute/60;
        secondsPerBeat = 1/beatsPerSecond;
        fixedUpdatePerBeat = secondsPerBeat/0.02f;
    }
// uses fixedUpdate to measure what fraction of a beat the game is on, needs to measure offset
    void FixedUpdate() {
        fixedUpdateCounter += 1;
        if (fixedUpdateCounter >= fixedUpdatePerBeat) {
            fixedUpdateCounter = 0f;
        }
        beatFraction = ((fixedUpdateCounter * 0.02f)/secondsPerBeat);
    }

    // returns BeatFraction calc when called
    public float getBeatFraction() {
        return beatFraction;
    }

    // Returns true if Beatfraction is less than the allowed offset and false otherwise
    public bool testInput(float offset) {
        if (beatFraction > offset) {
            return false;
        }
        else {
            return true;
        }
    }

}
