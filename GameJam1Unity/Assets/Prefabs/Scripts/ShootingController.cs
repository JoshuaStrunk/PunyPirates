using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class ShootingController : MonoBehaviour {

	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public GameObject cannon;
	public int playerID = 0;

	private GameObject thisCannon;
	// Use this for initialization
	void Start () {
		playerIndex = (PlayerIndex) playerID;
		thisCannon = (GameObject)Instantiate (cannon, transform.position, transform.rotation);
		thisCannon.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		state = GamePad.GetState(playerIndex);

		//Left click to shoot
		if ( ((Mathf.Abs(state.ThumbSticks.Right.X) > .2f) || (Mathf.Abs(state.ThumbSticks.Right.Y) > .2f)))
		{
			CannonController c = thisCannon.GetComponent<CannonController>();
			c.fire (-state.ThumbSticks.Right.X, -state.ThumbSticks.Right.Y);
		}
	}
}
