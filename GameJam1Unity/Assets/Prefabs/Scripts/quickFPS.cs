using UnityEngine;
using System.Collections;

public class quickFPS : MonoBehaviour {

	void Awake () {
		StartCoroutine (LogFPS());
	}
	
	private IEnumerator LogFPS(){
		while (true) {
			Debug.Log (Time.deltaTime.ToString());
			yield return new WaitForSeconds(1);     
		}
	}
}
