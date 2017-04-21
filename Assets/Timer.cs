using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	int dt = 60;
	int timer = 1;
	//int bend = 180;
	public Stem root;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if (timer % dt == 0)
		{
			Debug.Log("Timer Activated");
			if (root)
			{
				root.Grow();
			}
			//if (timer == bend)
			//{
			//	root.Bend();
			//}
		}
	}
}
