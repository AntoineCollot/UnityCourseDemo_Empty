using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserView : MonoBehaviour {

    protected LaserPhysic laser;

    protected virtual void Awake () {
        laser = GetComponentInParent<LaserPhysic>();
    }
	
	protected virtual void LateUpdate () {
        if (laser == null)
            return;
    }
}
