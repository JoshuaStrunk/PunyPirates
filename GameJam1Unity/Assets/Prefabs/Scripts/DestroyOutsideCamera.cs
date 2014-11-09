using UnityEngine;
using System.Collections;

public class DestroyOutsideCamera : MonoBehaviour {


	public float time = 1f;
	public float massaging = 1f;
	private float outsideAt;
	private bool outside;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!outside && (Camera.main.transform.position - transform.position).magnitude > Camera.main.orthographicSize * Camera.main.aspect + massaging) {
			outside = true;
			outsideAt = Time.time;
		}
		if(outside) {
			if((Camera.main.transform.position - transform.position).magnitude < Camera.main.orthographicSize * Camera.main.aspect + massaging) {
				outside = false;
			}
			else if (Time.time - outsideAt > time) {
				SendMessage("kill");
			}
		}
	}
}
