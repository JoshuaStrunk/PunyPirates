using UnityEngine;
using System.Collections;

public class PlayerDeathScript : MonoBehaviour {

	void kill() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
