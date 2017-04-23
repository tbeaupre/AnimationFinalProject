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
		Dictionary<char, string> rules = new Dictionary<char, string>();
		rules.Add('X', "F-[[X]+X]+F[+FX]-X");
		rules.Add('F', "FF");
		this.lSystem = new LSystem("X", rules, 25, maxN);
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
			Stem currentStem = plant;
			currentStem.Init(stemPrefab, new List<Stem>());
			Stack<Stem> stemStack = new Stack<Stem>();
			float angle = 0;

			for (int i = 0; i < blueprint.Length; i++)
			{
				switch (blueprint[i])
				{
				case 'F':
					currentStem = currentStem.AddStemSegment();
					currentStem.transform.Rotate(0, 0, angle);
					angle = 0;
					break;
				case '-':
					angle -= lSystem.angle;
					break;
				case '+':
					angle += lSystem.angle;
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

