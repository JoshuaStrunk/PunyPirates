using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OceanBuilder : MonoBehaviour {

	//Controllable chunk width
	public float chunkWidth = 1.0f;
	public float amplitude = 7.0f;
	public float frequency = 1.0f;

	//Chunk prefab
	public GameObject oceanChunkPrefab;

	private int numChunks;
	private List<GameObject> chunkList = new List<GameObject>();
	private float heightOffset;

	// Use this for initialization
	void Start () {

		//Solves for number of chunks based off ocean width and chunkWidth
		numChunks = (int)(transform.localScale.x / chunkWidth);

		heightOffset = transform.position.y;

		//Creates and places the correct number of chunks
		for(int i = 0; i < numChunks; i++)
		{
			GameObject oceanChunk = Instantiate(oceanChunkPrefab, new Vector3((transform.position.x - transform.localScale.x/2.0f) + chunkWidth*i + .25f, transform.position.y, transform.position.z), transform.rotation) as GameObject;   
			oceanChunk.transform.localScale = new Vector3(chunkWidth, oceanChunk.transform.localScale.y, transform.localScale.z);
			oceanChunk.transform.parent = transform;

			//Addots to list
			chunkList.Add(oceanChunk);
		}
	}
	
	// Update is called once per frame
	void Update () {

		//Iterates through chunks, does sine transformation
		foreach(GameObject chunk in chunkList)
		{
			float i = chunkList.IndexOf(chunk);

			float TwoPi = Mathf.PI*2.0f;

			float numChunksF = (float)numChunks;

			//Wow look at this piece of shit periodic sine function, it fucking WORKS
			chunk.transform.position = new Vector3(chunk.transform.position.x, Mathf.Sin((((Time.time+(TwoPi/numChunksF*i))*frequency)%TwoPi))*amplitude + heightOffset, chunk.transform.position.z);
		}

	}

	void FixedUpdate() {
		amplitude += .001f;
	}

	//GO GO GADGET GIZMO!
	void OnDrawGizmos() {
		Gizmos.color = new Color(0, .2f, .6f, 0.5F);
		Gizmos.DrawCube(transform.position,transform.localScale);
	}
}
