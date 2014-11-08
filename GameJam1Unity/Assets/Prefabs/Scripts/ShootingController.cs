using UnityEngine;
using System.Collections;

public class ShootingController : MonoBehaviour {

	public GameObject cannon;
	public int playerID = 0;

	private GameObject thisCannon;
	// Use this for initialization
	void Start () {
		thisCannon = (GameObject)Instantiate (cannon, transform.position, transform.rotation);
		thisCannon.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Left click to shoot
		if ( ((Mathf.Abs(Input.GetAxis("FireHorizontal"+playerID)) > .2f) || (Mathf.Abs(Input.GetAxis("FireVertical"+playerID)) > .2f)))
		{
			 CannonController c = thisCannon.GetComponent<CannonController>();
			c.fire (Input.GetAxis("FireHorizontal" + playerID), Input.GetAxis("FireVertical"+playerID));
		}
	}
}
