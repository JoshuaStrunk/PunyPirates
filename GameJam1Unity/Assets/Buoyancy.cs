using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {


	public float BuoantForce = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D() {
		rigidbody2D.velocity = Vector2.zero; //rigidbody.velocity * .1f;
	}
	void OnTriggerStay2D() {
		Debug.Log("In Watter");
		rigidbody2D.AddForce(Vector2.up * BuoantForce);
	}
}
