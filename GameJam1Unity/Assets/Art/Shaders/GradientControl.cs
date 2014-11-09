using UnityEngine;
using System.Collections;

public class GradientControl : MonoBehaviour {

	public Color startColor = Color.red;
	public Color endColor = Color.blue;

	// Use this for initialization	
		void Start () {
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			Color[] colors = new Color[mesh.vertices.Length];
			colors[0] = startColor;
			colors[1] = endColor;
			colors[2] = startColor;
			colors[3] = endColor;
			mesh.colors = colors;
		}
	}
	
