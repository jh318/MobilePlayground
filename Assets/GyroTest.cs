using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroTest : MonoBehaviour {

	void Start(){
		Input.gyro.enabled = true;
	}

	void Update(){
		transform.rotation = Input.gyro.attitude;
	}
}
