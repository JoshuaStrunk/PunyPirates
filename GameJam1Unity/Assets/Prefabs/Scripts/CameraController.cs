﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float smoothing = .5f;
	public float boarder = 1f;
	public float minViewSize = 20f;
	public float maxViewSize = 200f;
	static public int[] playerScores = new int[2] {0,0};
	public GUISkin skin;
	// Use this for initialization
	

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
		maxDiff = maxDiff  + boarder;
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

	void OnGUI() {

		GUIStyle player1= new GUIStyle();
		player1.fontSize = 50;
		player1.normal.textColor = Color.red;
		GUIStyle player2= new GUIStyle();
		player2.fontSize = 50;
		player2.normal.textColor = Color.yellow;

		GUI.Label(new Rect(10,0, 100, 30), playerScores[0].ToString(), player1);
		GUI.Label(new Rect(Screen.width - 50, 0, 100, 30), playerScores[1].ToString(), player2);
	}
}
