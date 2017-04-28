using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemImplement : MonoBehaviour
{
	public Stem stemPrefab;
	LSystem lSystem;
	public int maxN = 6;

	// Use this for initialization
	void Start ()
	{
		Dictionary<char, List<Rule>> rules1 = new Dictionary<char, List<Rule>>();
		rules1.Add('X', new List<Rule>(){new Rule("F-&[[X]\\+^X]/+^F[+^FX]\\-&X")});
		rules1.Add('F', new List<Rule>(){new Rule(0.8f, "FF"), new Rule(0.2f, "F")});

		Dictionary<char, List<Rule>> rules2 = new Dictionary<char, List<Rule>>();
		rules2.Add('X', new List<Rule>(){	new Rule(0.32f, "[[//+FX]\\+FX]\\-FX"),
											new Rule(0.32f, "[\\+FX]\\-FX"),
											new Rule(0.32f, "[/+FX]/-FX"),
											new Rule(0.03f, "\\-FX"),
											new Rule(0.03f, "/-FX")});
		
		rules2.Add('F', new List<Rule>(){	new Rule(0.7f, "FF"),
											new Rule(0.3f, "F")});


		this.lSystem = new LSystem("X", rules2, 25, 70, 25, maxN);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public Stem CreateGameObject (int n)
	{
		if (lSystem != null)
		{
			string blueprint = lSystem.GetResult(n);
			Stem plant = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
			Stack<Stem> stemStack = new Stack<Stem>();
			float roll = 0;
			float pitch = 0;
			float yaw = 0;

			Stem currentStem = plant;
			currentStem.stemPrefab = stemPrefab;

			for (int i = 0; i < blueprint.Length; i++)
			{
				switch (blueprint[i])
				{
				case 'F':
					currentStem = currentStem.AddStemSegment();
					currentStem.transform.Rotate(pitch, yaw, roll);
					pitch = 0;
					yaw = 0;
					roll = 0;
					break;

				case '&':
					pitch -= lSystem.pitch;
					break;
				case '^':
					pitch += lSystem.pitch;
					break;
				case '\\':
					yaw -= lSystem.yaw;
					break;
				case '/':
					yaw += lSystem.yaw;
					break;
				case '-':
					roll -= lSystem.roll;
					break;
				case '+':
					roll += lSystem.roll;
					break;

				case '[':
					stemStack.Push(currentStem);
					break;
				case ']':
					currentStem = stemStack.Pop();
					break;
				default:
					break;
				}
			}
			return plant;
		} else
		{
			this.Start();
			return this.CreateGameObject(n);
		}
	}
}

