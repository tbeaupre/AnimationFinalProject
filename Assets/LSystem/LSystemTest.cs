using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemTest : MonoBehaviour
{
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void PrintLSystem(LSystem lSystem)
	{
		if (lSystem != null)
		{
			for (int n = 0; n < 10; n++)
			{
				Debug.Log(string.Format("n{0} = {1}", n, lSystem.GetResult(n)));
			}
		}
	}
}

