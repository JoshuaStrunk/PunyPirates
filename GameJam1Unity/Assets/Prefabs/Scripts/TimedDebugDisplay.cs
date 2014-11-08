using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedDebugDisplay : MonoBehaviour {

	class TimedDebug
	{
		public float StartTime;
		public float Age;
		public string debugString;
	
		public TimedDebug(string s, float a)
		{
            debugString = s;
			Age = a;
			StartTime = Time.time;
		}
	}

    private static List<TimedDebug> timedStrings = new List<TimedDebug>();

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
        foreach (TimedDebug ts in timedStrings)
        {
            int i = timedStrings.IndexOf(ts);
            GUI.Label(new Rect(posX, posY + i * 20, 300, 20), ts.debugString);

            if (Time.time - ts.StartTime > ts.Age)
                timedStrings.Remove(ts);
        }
    }

    public void addTimedString(string str, float lifespan)
	{
        timedStrings.Add(new TimedDebug(str, lifespan));
	}
}
