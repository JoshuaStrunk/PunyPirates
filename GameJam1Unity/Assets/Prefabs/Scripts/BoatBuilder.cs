using UnityEngine;
using System.Collections;

public class BoatBuilder : MonoBehaviour {

	public int boatType = 0;
	public GameObject boatChunk;
	public Texture2D[] boatTypes;

	GameObject[,] boatChunks;

	int width = 16;
	int height = 16;
	//int boatsNum = 2;
	int[,,] boats;

	// Use this for initialization
	void Start () {

		boats = new int[boatTypes.Length,height,width];
		for(int i=0; i<boatTypes.Length; i++) {
			for(int j=0; j<width; j++) {
				for(int k=0; k<height; k++) {
					boats[i,k,j] = 1 - (int) boatTypes[i].GetPixel(j, height - k - 1).grayscale;
				}
			}
		}

		//boatsNum, height, width
		/*
		boats = new int[,,]{
			{
				{1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
				{1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
			},
			{
				{0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
				{0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
			}

		};
		*/


		boatChunks = new GameObject[width,height];

		//Creation
		for(int i = 0; i < width; i++) {
			for(int j = 0; j<height; j++) {

				if(boats[boatType,height - j - 1,i] == 1) {
					boatChunks[i,j] = (GameObject) Instantiate (boatChunk, transform.position + new Vector3(i,j,0), transform.rotation);
					boatChunks[i,j].transform.parent = gameObject.transform;
				}
				

			}
		}
		//Horizontal Connections
		for(int i = 0; i<width-1; i++) {
			for(int j=0; j<height; j++) {
				if((boats[boatType,height - j-1,i] == 1) && (boats[boatType,height -j-1,i+1] == 1)) {
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
		}

		//Horizontal Connections
		for(int i = 0; i<width; i++) {
			for(int j=0; j<height-1; j++) {
				if((boats[boatType,height-j-2,i] == 1) && (boats[boatType,height-j-1,i] == 1)) {
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
		}
		//Big O scary

		for(int i = 0; i < width; i++) {
			for(int j = 0; j<height; j++) {
				for(int ii = 0; ii <width; ii++) {
					for(int jj=0; jj < height; jj++) {
						if((boats[boatType,height - j - 1,i] == 1) && (boats[boatType,height - jj - 1,ii] == 1)) {
							Physics2D.IgnoreCollision(boatChunks[i,j].collider2D, boatChunks[ii,jj].collider2D, true);
						}
					}
				}
			}
		}


	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(1, 0, 0, 0.5F);
		Gizmos.DrawCube(transform.position + new Vector3(width/2, height/2, 0),new Vector3(width,height,1));
	}
	
}
