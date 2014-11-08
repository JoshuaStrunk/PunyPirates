using UnityEngine;
using System.Collections;

public class BoatBuilder : MonoBehaviour {


	public int width = 5;
	public int height = 2;
	public GameObject boatChunk;

	GameObject[,] boatChunks;


	// Use this for initialization
	void Start () {
		boatChunks = new GameObject[5,2]; 
		for(int i = 0; i < width; i++) {
			for(int j = 0; j<height; j++) {
				boatChunks[i,j] = (GameObject) Instantiate (boatChunk, transform.position + new Vector3(i,j,0), transform.rotation);
				boatChunks[i,j].transform.parent = gameObject.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
