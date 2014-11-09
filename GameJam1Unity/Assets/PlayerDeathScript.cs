using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

	public int playerID = 0;
	void kill() {

		CameraController.playerScores[1 - (playerID % 2)] += 1;

		Application.LoadLevel(Application.loadedLevel);
	}
}
