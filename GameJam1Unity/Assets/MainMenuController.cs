using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void OnGUI() {
		GUI.Label(new Rect(Screen.width/2-50, 0, 100, 50), "tiny PIRATES!!!");
		if(GUI.Button(new Rect(Screen.width/2-50, Screen.height/2, 100, 30), "Start!")) {
			Application.LoadLevel(1);
		}
	}
}
