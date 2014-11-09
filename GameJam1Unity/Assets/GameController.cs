﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class GameController : MonoBehaviour {

	static public bool firstTo = true;
	static public int numberOfRounds = 3;
	static public int currentRound = 1;
	private static int[] playerScores = new int[2] {0,0};
	private static bool exists = false;
	private static bool setup = true;
	private static bool afterScreen = false;

	PlayerIndex playerIndex = (PlayerIndex)0;
	GamePadState state;
	GamePadState prevState;



	// Use this for initialization
	void Start () {
		if(exists) {
			Destroy(gameObject);
		}
		else {
			exists = true;
			DontDestroyOnLoad(gameObject);
		}
	}

	static public void playerLoss(int i) {
		if(! afterScreen) {
			playerScores[1 - (i % 2)] += 1;
			currentRound += 1;
			toAfterScreen();
		}
	}
	

	private static bool winCheck() {
		if(firstTo && (playerScores[0] >= numberOfRounds || playerScores[1] >= numberOfRounds)) {
			return true;
		}
		else if(!firstTo && currentRound > numberOfRounds+1) {
			return true;
		}
		return false;
	}

	private static void toAfterScreen() {
		afterScreen = true;
	}
	private static void backToSetup() {
		playerScores = new int[2] {0,0};
     	currentRound = 1;
     	playerScores[0] = 0;
    	playerScores[1] = 0;
		setup = true;
		Application.LoadLevel(2);
	}

	void OnGUI() {
		prevState = state;
		state = GamePad.GetState(playerIndex);

		if(setup) {
			if(firstTo) {
				if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2, 100, 30), "First to "+numberOfRounds+ " wins.")){
					firstTo = false;
				}
			}
			else {
				if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2, 100, 30), numberOfRounds +" rounds total.")) {
					firstTo = true;
				}
			}

			numberOfRounds = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50, Screen.height/2 +30, 100, 30), numberOfRounds, 1f, 25f );

			if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2+60, 100, 30), "Start!")){
				setup = false;
				Application.LoadLevel(1);
			}
		}
		else if(afterScreen) {

			if(!(winCheck())) {
				GUIStyle player1= new GUIStyle();
				player1.fontSize = 50;
				player1.normal.textColor = Color.red;
				GUIStyle player2= new GUIStyle();
				player2.fontSize = 50;
				player2.normal.textColor = new Color(151f/255f, 0f, 195f/255f);
				
				GUI.Label(new Rect(10,0, 100, 30), playerScores[0].ToString (), player1);
				GUI.Label(new Rect(Screen.width - 50, 0, 100, 30), playerScores[1].ToString(), player2);
				if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2 +30, 100, 30), "Next Round!") ){
					afterScreen = false;
					currentRound += 1;
					Application.LoadLevel(1);
				}
			}
			else {
				string victoryString;
				if(playerScores[0] > playerScores[1]) {
					victoryString = "Player 1 Wins!";
				}
				else if(playerScores[1] > playerScores[0]) {
					victoryString = "Player 2 Wins!";
				}
				else {
					victoryString = "YOU ALL LOSE";
				}
				GUIStyle player1= new GUIStyle();
				player1.fontSize = 50;
				player1.normal.textColor = Color.red;
				GUIStyle player2= new GUIStyle();
				player2.fontSize = 50;
				player2.normal.textColor = new Color(151f/255f, 0f, 195f/255f);
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height/2, 100, 30), victoryString);
				if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2 +60, 100, 30), "Back to Setup") ){
					afterScreen = false;
					backToSetup();
				}
			}
		}
		else {
			GUIStyle player1= new GUIStyle();
			player1.fontSize = 50;
			player1.normal.textColor = Color.red;
			GUIStyle player2= new GUIStyle();
			player2.fontSize = 50;
			player2.normal.textColor = new Color(151f/255f, 0f, 195f/255f);
			
			GUI.Label(new Rect(10,0, 100, 30), playerScores[0].ToString (), player1);
			GUI.Label(new Rect(Screen.width - 50, 0, 100, 30), playerScores[1].ToString(), player2);
		}
	}
	
}
