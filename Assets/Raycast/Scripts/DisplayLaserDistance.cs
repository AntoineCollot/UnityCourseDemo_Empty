using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLaserDistance : LaserView {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	protected override void LateUpdate () {
        text.text = laser.hitDistance.ToString("N2");
	}
}
