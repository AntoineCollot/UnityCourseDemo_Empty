using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the transfrom toward the cursor
/// </summary>
public class LookAtCursorTarget : MonoBehaviour {

    Camera cam;

	void Awake () {
        //Saves a reference to the camera to avoid calling Camera.Main (which actually does FindObjectWithTag("MainCamera")) multiple times.
        cam = Camera.main;
	}
	
	void Update () {
        //Gets a ray from the cursor position and along the camera forward direction
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Send the ray into the scene and check if it hits something
        if(Physics.Raycast(camRay,out hit,50))
        {
            //Gets the hit position
            Vector3 targetLookAt = hit.point;

            //Moves the target on the same height as our transform
            targetLookAt.y = transform.position.y;

            //Makes the transform look at the target
            transform.LookAt(targetLookAt);

            //Ignore x and z rotations
            Vector3 rot = transform.localEulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.localEulerAngles = rot;
        }
	}
}
