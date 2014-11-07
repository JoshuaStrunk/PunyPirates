using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugDisplay : MonoBehaviour {

	private static List<string> debugStrings = new List<string>();
	
	public int posX = 25;
	public int posY = 25;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		foreach (string s in debugStrings)
		{
			int i = debugStrings.IndexOf(s);
			GUI.Label(new Rect(posX, posY + i*20, 300, 20), s);
		}

        debugStrings.Clear();
	}

	public void addString(string str)
	{
        debugStrings.Add(str);
	}
}
