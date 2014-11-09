using UnityEngine;
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
	
	GamePadState state1;
	GamePadState prevState1;
	GamePadState state2;
	GamePadState prevState2;



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
		prevState1 = state1;
		state1 = GamePad.GetState((PlayerIndex)0);
		prevState2 = state2;
		state2 = GamePad.GetState((PlayerIndex)1);

		GUIStyle setupStyle= new GUIStyle();
		setupStyle.fontSize = 25;
		setupStyle.normal.textColor = Color.white;

		if(setup) {			

			GUI.Label(new Rect(Screen.width / 2 - 85, Screen.height/2-30, 200, 60), "First to "+numberOfRounds+ " wins.", setupStyle);



			if ((state1.ThumbSticks.Left.X > .1f) && (prevState1.ThumbSticks.Left.X < .1f)
			    || (state2.ThumbSticks.Left.X > .1f) && (prevState2.ThumbSticks.Left.X < .1f)){
				if(numberOfRounds < 50 )
					numberOfRounds += 1;
			}
			if ((state1.ThumbSticks.Left.X < -.1f) && (prevState1.ThumbSticks.Left.X > -.1f)
			    || (state2.ThumbSticks.Left.X < -.1f) && (prevState2.ThumbSticks.Left.X > -.1f)){
				if(numberOfRounds > 1)
					numberOfRounds -= 1;
			}
									
			numberOfRounds = (int)GUI.HorizontalSlider(new Rect(Screen.width / 2 - 100, Screen.height/2+20, 200, 30), numberOfRounds, 1f, 25f );

			GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height/2+60, 200, 60), "Press Start to begin!", setupStyle);

			if((state1.Buttons.Start == ButtonState.Pressed && prevState1.Buttons.Start == ButtonState.Released)
			   || (state2.Buttons.Start == ButtonState.Pressed && prevState2.Buttons.Start == ButtonState.Released)){
				setup = false;
				Application.LoadLevel(1);
			}
		}
		else {
			GUIStyle player1= new GUIStyle();
			player1.fontSize = 100;
			player1.normal.textColor = Color.red;
			GUIStyle player2= new GUIStyle();
			player2.fontSize = 100;
			player2.normal.textColor = new Color(151f/255f, 0f, 195f/255f);
			
			GUI.Label(new Rect(40,20, 100, 30), playerScores[0].ToString (), player1);
			GUI.Label(new Rect(Screen.width - 100, 20, 100, 30), playerScores[1].ToString(), player2);
		}
		if(afterScreen) {

			if(!(winCheck())) {

				GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height/2 +30, 200, 30), "Press start to begin the next Round!", setupStyle);

				if((state1.Buttons.Start == ButtonState.Pressed && prevState1.Buttons.Start == ButtonState.Released)
				   || (state2.Buttons.Start == ButtonState.Pressed && prevState2.Buttons.Start == ButtonState.Released))
				{
					afterScreen = false;
					currentRound += 1;
					Application.LoadLevel(1);
				}
			}
			else {
				string victoryString;
				GUIStyle victoryStyle= new GUIStyle();
				victoryStyle.fontSize = 50;

				if(playerScores[0] > playerScores[1]) {
					victoryString = "Player 1 Wins!";
					victoryStyle.normal.textColor = Color.red;
				}
				else if(playerScores[1] > playerScores[0]) {
					victoryString = "Player 2 Wins!";
					victoryStyle.normal.textColor = new Color(151f/255f, 0f, 195f/255f);
				}
				else {
					victoryString = "YOU ALL LOSE";
					victoryStyle.normal.textColor = Color.black;
				}
				GUI.Label(new Rect(Screen.width / 2 - 150, 150, 100, 30), victoryString, victoryStyle);
				GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height/2 +30, 200, 30), "Press start to return to Setup!", setupStyle);

				if((state1.Buttons.Start == ButtonState.Pressed && prevState1.Buttons.Start == ButtonState.Released)
				   || (state2.Buttons.Start == ButtonState.Pressed && prevState2.Buttons.Start == ButtonState.Released)){
					afterScreen = false;
					backToSetup();
				}
			}
		}
	}
	
}
