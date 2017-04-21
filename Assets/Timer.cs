using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	int dt = 60;
	int timer = 1;
	int i;
	//int bend = 180;
	public Stem root;

	// Use this for initialization
	void Start () {
		i = 6;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= i * dt)
		{
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
}
