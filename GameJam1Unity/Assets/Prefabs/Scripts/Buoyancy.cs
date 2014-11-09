using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {


	public float BuoantForce = 100f;


	private float prevDrag;
	private float prevGrav;

	void OnTriggerEnter2D() {
		rigidbody2D.velocity = Vector2.zero; //rigidbody.velocity * .1f;

		
	}
	void OnTriggerStay2D() {
		rigidbody2D.AddForce(Vector2.up * BuoantForce);
	}

}
