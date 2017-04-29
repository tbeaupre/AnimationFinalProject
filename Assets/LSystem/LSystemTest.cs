using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemTest : MonoBehaviour
{
	LSystem lSystem;

	// Use this for initialization
	void Start ()
	{
		string start = "X";
		float angle = 25;
		int maxN = 6;
		RuleSet rules = new RuleSet();
		rules.AddRule('X', 0.32f, "[[//+FX]\\+FX]\\-FX");
		rules.AddRule('X', 0.32f, "[\\+FX]\\-FX");
		rules.AddRule('X', 0.32f, "[/+FX]/-FX");
		rules.AddRule('X', 0.03f, "\\-FX");
		rules.AddRule('X', 0.03f, "/-FX");

		rules.AddRule('F', 0.7f, "FF");
		rules.AddRule('F', 0.3f, "F");
		this.lSystem = new LSystem(start, rules, angle, maxN);

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

