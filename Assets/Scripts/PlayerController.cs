using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 100.0f;
	Rigidbody body;

	void OnEnable(){
		Joystick.joyMoved += JoyMoved;

	}

	void OnDisable(){
		Joystick.joyMoved += JoyMoved;		
	}

	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	void JoyMoved(Vector2 pos){
		body.velocity = new Vector3(pos.x, 0, -pos.y) * speed;
	}
}
