using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : LaserView {

    LineRenderer line;

	protected override void Awake () {
        base.Awake();
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
	}
	
	// Update is called once per frame
	protected override void LateUpdate () {
        base.LateUpdate();

        line.SetPosition(0, transform.position);
        line.SetPosition(1, laser.hitPoint);
	}
}
