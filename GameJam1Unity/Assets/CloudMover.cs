using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {

	public float moveRate = .1f;

	// Use this for initialization
	void Start () {
		float randomHeight = Random.Range(10,40);

		transform.position = new Vector3(transform.position.x, randomHeight, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += new Vector3(moveRate, 0, 0);

		if (transform.position.x > 180)
		{
			transform.position = new Vector3( -160, Random.Range(10,40), transform.position.z);
		}
	}
}
