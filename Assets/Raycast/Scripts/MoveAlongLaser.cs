using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongLaser : LaserView {

    [Range(0,1)]
    public float relativeDistance;
	
	// Update is called once per frame
	protected override void LateUpdate () {
        base.LateUpdate();

        //Interpolate linearely the position along the raycast
        transform.position = Vector3.Lerp(laser.transform.position, laser.hitPoint, relativeDistance);
    }
}
