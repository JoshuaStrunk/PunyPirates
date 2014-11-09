using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {

	public float force = 100f;
	public float rateOfFire = 5f;
	public float spread = 0f;
	private float lastFire = 0f;

	public AudioClip shoot1;

	public GameObject ammo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fire(float x, float y) {
		if ( (Time.time - lastFire) > (1 / rateOfFire )) {
			Vector2 aimVec = new Vector2(x, y) + new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
			aimVec.Normalize();
			
			
			GameObject newProjectile = Instantiate(ammo, new Vector2(transform.position.x, transform.position.y) + aimVec*-2, Quaternion.identity) as GameObject;
			//newProjectile.transform.parent = transform;
			newProjectile.rigidbody2D.AddForce(aimVec*-force, ForceMode2D.Impulse);

			audio.PlayOneShot(shoot1);
			
			lastFire = Time.time;
		}
	}
}
