using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithOffset : MonoBehaviour {

    public Transform target;
    Vector3 offset;
    public bool ignoreY;

	// Use this for initialization
	void Start () {
        offset = transform.position- target.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPos = target.position + offset;
        if (ignoreY)
            newPos.y = transform.position.y;

        transform.position = newPos;
    }
}
