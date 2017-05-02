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
		float pitch = 35;
		float yaw = 70;
		float roll = 35;
		int maxN = 6;
		RuleSet rules = new RuleSet();
		rules.AddRule("X", 0.32f, "[[//+F(0)X]\\+F(0)X]\\-F(0)X");
		rules.AddRule("X", 0.32f, "[\\+F(0)X]\\-F(0)X");
		rules.AddRule("X", 0.32f, "[/+F(0)X]/-F(0)X");
		rules.AddRule("X", 0.03f, "\\-F(0)X");
		rules.AddRule("X", 0.03f, "/-F(0)X");
		this.lSystem = new LSystem(start, rules, pitch, yaw, roll, maxN);

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

