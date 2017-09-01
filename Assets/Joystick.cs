using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour {

	public delegate void JoystickMoved(Vector2 pos);
	public static event JoystickMoved joyMoved = delegate{};
	
	public SpriteRenderer joySprite;
	public float maxDist = 0.05f;

	bool touched = false;

	Camera cam;

	void Start(){
		cam = Camera.main;
	}

	void Update(){
		if(Input.touchCount == 0) return; 

		Touch t = Input.GetTouch(0);

		if(t.phase == TouchPhase.Began){
			Ray r = Camera.main.ScreenPointToRay(t.position);
			RaycastHit hit;			
			if (Physics.Raycast(r, out hit)){
				touched = true;
				Moved(t.position);
			}
		}
		else if (touched && t.phase == TouchPhase.Moved){
			Moved(t.position);
		}
		else if(touched && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)){
			touched = false;
			joyMoved(Vector2.zero);
			joySprite.transform.localPosition = Vector3.zero;
		}
	}

	void Moved(Vector3 screenPos){
		screenPos.z = joySprite.transform.position.z - cam.transform.position.z;
		joySprite.transform.position = cam.ScreenToWorldPoint(screenPos);
		if(joySprite.transform.localPosition.magnitude > maxDist) {
			joySprite.transform.localPosition = joySprite.transform.localPosition.normalized * maxDist;
		}

		Vector2 pos = joySprite.transform.localPosition;
		Debug.Log(pos);
		joyMoved(pos);
	}

}
