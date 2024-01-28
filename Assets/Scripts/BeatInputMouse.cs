using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatInputMouse
{

    public Vector3 storedMousePosition = new Vector3(0,0,0);
    public void storeMousePosition() {
        storedMousePosition = Input.mousePosition;
    }

    public Vector3 getMouseDelta() {
        Vector3 currentMousePosition = Input.mousePosition;
        return new Vector3((currentMousePosition.x - storedMousePosition.x), (currentMousePosition.y - storedMousePosition.y), (currentMousePosition.z - storedMousePosition.z));
    }
}
