using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour {

    Transform camT;

    public bool freeY;

	// Use this for initialization
	void Awake () {
        camT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 target = camT.position;

        if(!freeY)
        {
            target.y = transform.position.y;
        }

        transform.LookAt(target);
	}
}
