using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public int dt = 10;
	int timer = 1;
	public LSystemImplement lSystemImplement;

	// Use this for initialization
	void Start () {
		lSystemImplement.CreateGameObject(0);
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if (timer <= dt * lSystemImplement.numIterations)
		{
			if (timer % dt == 0)
			{
				if (lSystemImplement)
				{
					lSystemImplement.CreateGameObject(timer / dt);
				}
			}
		}
	}
}
