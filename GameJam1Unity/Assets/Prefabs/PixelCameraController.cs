using UnityEngine;
using System.Collections;

public class PixelCameraController : MonoBehaviour {


	public float pixelsToUnits = 1f;
	public int gameHeight = 720;

	// Use this for initialization
	void Start () {
		pixelsToUnits = Screen.height / gameHeight;
		if(pixelsToUnits < 1)
			pixelsToUnits = 1;


		float sizeH = (Screen.height / 2f) * (1f / pixelsToUnits);
		float sizeW = Camera.main.aspect * sizeH;
		Camera.main.orthographicSize = sizeH;
		transform.position = new Vector3(sizeW, -sizeH, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
