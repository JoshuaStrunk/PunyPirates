using UnityEngine;
using System.Collections;

public class dummyObjct : MonoBehaviour {

    private DebugDisplay debugDisplay;
    private TimedDebugDisplay timedDebugDisplay;

	// Use this for initialization
	void Start () {
        debugDisplay = GameObject.Find("DebugContainer").GetComponent<DebugDisplay>();
        timedDebugDisplay = GameObject.Find("TimedDebugContainer").GetComponent<TimedDebugDisplay>();
	}
	
	// Update is called once per frame
	void Update () {
        debugDisplay.addString("test external");	    

        if (Input.GetKeyDown("space"))
        {
            timedDebugDisplay.addTimedString("this will live for 2 seconds", .5f);
        }
	}

 
}
