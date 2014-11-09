using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

	public int playerID = 0;
	public void kill() {

		GameController.playerLoss(playerID);

		Application.LoadLevel(Application.loadedLevel);
	}
}
