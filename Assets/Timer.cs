using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	int dt = 10;
	int timer = 1;
	public LSystemImplement lSystemImplement;
	Stem plant;

	// Use this for initialization
	void Start () {
		plant = lSystemImplement.CreateGameObject(0);
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if (timer <= dt * lSystemImplement.maxN)
		{
			if (timer % dt == 0)
			{
				if (lSystemImplement)
				{
					Destroy(plant.gameObject);
					plant = lSystemImplement.CreateGameObject(timer / dt);
				}
			}
		}
	}
}
