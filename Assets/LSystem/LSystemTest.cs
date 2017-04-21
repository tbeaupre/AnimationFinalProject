using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemTest : MonoBehaviour
{
	LSystem lSystem;

	// Use this for initialization
	void Start ()
	{
		Dictionary<char, string> rules = new Dictionary<char, string>();
		rules.Add('X', "F-[[X]+X]+F[+FX]-X");
		rules.Add('F', "FF");
		this.lSystem = new LSystem("X", rules, 25, 6);

		for (int n = 0; n < 10; n++)
		{
			Debug.Log(string.Format("n{0} = {1}", n, lSystem.GetResult(n)));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}

