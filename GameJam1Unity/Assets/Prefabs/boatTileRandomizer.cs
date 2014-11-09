using UnityEngine;
using System.Collections;

public class boatTileRandomizer : MonoBehaviour {

	public Texture2D[] boatTiles;

	// Use this for initialization
	void Start () {
		renderer.material.SetTexture("_MainTex", boatTiles[ (int)Random.Range(0, (float)boatTiles.Length)]);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
