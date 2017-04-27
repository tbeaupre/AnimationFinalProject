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
		Dictionary<char, List<Rule>> rules = new Dictionary<char, List<Rule>>();
		rules.Add('X', new List<Rule>(){new Rule("F-[[X]+X]+F[+FX]-X")});
		rules.Add('F', new List<Rule>(){new Rule("FF")});
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

