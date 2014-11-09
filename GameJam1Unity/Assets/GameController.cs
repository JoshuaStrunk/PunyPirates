using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	static public bool firstTo = true;
	static public int numberOfRounds = 3;
	static public int currentRound = 1;
	private static int[] playerScores = new int[2] {0,0};
	private static bool exists = false;
	private static bool setup = true;
	private static bool afterScreen = false;
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
		playerScores[1 - (i % 2)] += 1;
		currentRound += 1;
		toAfterScreen();
	}
	

	private static bool winCheck() {
		if(playerScores[0] == numberOfRounds || playerScores[1] == numberOfRounds) {
			return true;
		}
		else if(currentRound > numberOfRounds) {
			return true;
		}
		return false;
	}

	private static void toAfterScreen() {
		afterScreen = true;
		Debug.Log("FUCKING LOAD ");
		Application.LoadLevel(2);
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
			if(Application.loadedLevel != 2)
				Application.LoadLevel(2);

			if(!(winCheck())) {
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height/2-60, 100, 30), "Player 1: " + playerScores[0]);
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height/2-30, 100, 30), "Player 2: " + playerScores[1]);
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
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height/2-60, 100, 30), "Player 1: " + playerScores[0]);
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height/2-30, 100, 30), "Player 2: " + playerScores[1]);
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
			player2.normal.textColor = Color.yellow;
			
			GUI.Label(new Rect(10,0, 100, 30), playerScores[0].ToString (), player1);
			GUI.Label(new Rect(Screen.width - 50, 0, 100, 30), playerScores[1].ToString(), player2);
		}
	}
	
}
