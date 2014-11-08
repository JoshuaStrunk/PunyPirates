using UnityEngine;
using System.Collections;

public class BoatBuilder : MonoBehaviour {


	public int width = 4;
	public int height = 2;

	public float scale = 1f;
	public GameObject boatChunk;

	GameObject[,] boatChunks;


	// Use this for initialization
	void Start () {

		boatChunks = new GameObject[width,height];

		//Creation
		for(int i = 0; i < width; i++) {
			for(int j = 0; j<height; j++) {		
				boatChunks[i,j] = (GameObject) Instantiate (boatChunk, transform.position + new Vector3(i,j,0), transform.rotation);
				boatChunks[i,j].transform.parent = gameObject.transform;
				

			}
		}
		//Horizontal Connections
		for(int i = 0; i<width-1; i++) {
			for(int j=0; j<height; j++) {
				HingeJoint2D tempHinge = boatChunks[i,j].AddComponent<HingeJoint2D>();
				tempHinge.connectedBody = boatChunks[i+1,j].rigidbody2D;
				tempHinge.anchor = new Vector2(.5f, 0f);
				tempHinge.connectedAnchor = new Vector2(-.5f, 0f);
				tempHinge.useLimits = true;

				//Becuase C# is dumb
				JointAngleLimits2D limits = tempHinge.limits;
				limits.min = 0;
				limits.max = 0;
				tempHinge.limits = limits;
			}
		}

		//Horizontal Connections
		for(int i = 0; i<width; i++) {
			for(int j=0; j<height-1; j++) {
				HingeJoint2D tempHinge = boatChunks[i,j].AddComponent<HingeJoint2D>();
				tempHinge.connectedBody = boatChunks[i,j+1].rigidbody2D;
				tempHinge.anchor = new Vector2(0f, .5f);
				tempHinge.connectedAnchor = new Vector2(0f, -.5f);
				tempHinge.useLimits = true;
				
				//Becuase C# is dumb
				JointAngleLimits2D limits = tempHinge.limits;
				limits.min = 0;
				limits.max = 0;
				tempHinge.limits = limits;
			}
		}
		//Big O scary

		for(int i = 0; i < width; i++) {
			for(int j = 0; j<height; j++) {
				for(int ii = 0; ii <width; ii++) {
					for(int jj=0; jj < height; jj++) {
						Physics2D.IgnoreCollision(boatChunks[i,j].collider2D, boatChunks[ii,jj].collider2D, true);
					}
				}
			}
		}


	}
	
	// Update is called once per frame
	void Update () {

	}
}
