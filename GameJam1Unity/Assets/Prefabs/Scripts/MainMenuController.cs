using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class MainMenuController : MonoBehaviour {

	GamePadState state1;
	GamePadState prevState1;
	GamePadState state2;
	GamePadState prevState2;


	void Update() {
		prevState1 = state1;
		state1 = GamePad.GetState((PlayerIndex)0);
		prevState2 = state2;
		state2 = GamePad.GetState((PlayerIndex)1);
		if((state1.Buttons.Start == ButtonState.Pressed && prevState1.Buttons.Start == ButtonState.Released)
		   || (state2.Buttons.Start == ButtonState.Pressed && prevState2.Buttons.Start == ButtonState.Released) ) {

			Application.LoadLevel(2);
		}
	}
}
