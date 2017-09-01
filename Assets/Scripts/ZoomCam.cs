using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour {

	public float zoomDist = 1.0f;

	float prevAng;
	float prevDist;
	Vector2 prevPos;
	float panDist = -10.0f;

	void Update(){
		if(Input.touchCount < 2) return;
		Touch a = Input.GetTouch(0);
		Touch b = Input.GetTouch(1);

		if(b.phase == TouchPhase.Began){
			Vector2 diff = b.position - a.position;
			prevAng = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			prevDist = Vector3.Distance(a.position, b.position);
			prevPos = (a.position + b.position) * 0.5f;
		}
		else if(b.phase == TouchPhase.Moved || b.phase == TouchPhase.Moved){
			Vector2 diff = b.position - a.position;
			float dist = Vector3.Distance(a.position, b.position);
			Zoom(dist - prevDist);
			prevDist = dist;

			Vector2 pos = (a.position + b.position) * 0.5f;
			Pan(pos - prevPos);
			prevPos = pos;

			float ang = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			Rotate(ang - prevAng);
			prevAng = ang;

		}
	}

	void Zoom(float change){
		float frac = change / Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
	
		transform.position += transform.forward * frac * zoomDist;
	}

	void Pan(Vector2 change){
		change.x /= (float)Screen.width;
		change.y /= (float)Screen.height;
		transform.position += (Vector3)change * panDist;
		transform.position += (transform.up * change.y + transform.right * change.x) * panDist;
	}

	void Rotate(float change){
		transform.Rotate(Vector3.forward * change);
	}
}
