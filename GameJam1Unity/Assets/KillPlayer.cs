using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public AudioClip death;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag == "Player") {
			StartCoroutine(kill(coll));
		}
	}

	IEnumerator kill(Collider2D coll) {
		audio.PlayOneShot(death);
		yield return new WaitForSeconds(1f);
		((PlayerDeathScript)coll.GetComponent<PlayerDeathScript>()).kill();
	}
}
