using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float smoothing = .5f;
	public float boarder = 1f;
	public float minViewSize = 20f;
	public float maxViewSize = 200f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] players;
		players = GameObject.FindGameObjectsWithTag("Player");
		Vector3 averagePlayerPos = Vector3.zero;
		foreach(GameObject player in players) {
			averagePlayerPos += player.transform.position;
		}
		averagePlayerPos /= players.Length;

		transform.position = new Vector3(averagePlayerPos.x, averagePlayerPos.y, transform.position.z);

		float maxDiff = 0;

		foreach(GameObject player in players) {
			float diffOffAverage = (player.transform.position - averagePlayerPos).magnitude;
			if(diffOffAverage > maxDiff ) {
				maxDiff = diffOffAverage;
			}
		}
		maxDiff = maxDiff * camera.aspect + boarder;
		if(maxDiff < minViewSize) {
			camera.orthographicSize = minViewSize;
		}
		else if(maxDiff > maxViewSize) {
			camera.orthographicSize = maxViewSize;
		}
		else {
			camera.orthographicSize = maxDiff;
		}

		if (Input.GetKeyDown(KeyCode.R)) 
		{
			Application.LoadLevel(Application.loadedLevel);
			
		}
	}
}
