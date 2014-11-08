using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float smoothing = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * smoothing;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			transform.position -= Vector3.up* smoothing;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right* smoothing;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position -= Vector3.right* smoothing;
		}
		if(Input.GetMouseButton(3)) {
			camera.orthographicSize += smoothing;
		}
		if(Input.GetMouseButton(4)) {
			camera.orthographicSize -= smoothing;
		}

	}
}
