using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemImplement : MonoBehaviour
{
	public Stem stemPrefab;
	LSystem lSystem;
	public int maxN = 100;
	public int maxX = 15;
	public int maxF = 60;

	// Use this for initialization
	void Start ()
	{
//		RuleSet rules1 = new Dictionary<char, List<Rule>>();
//		rules1.Add('X', new List<Rule>(){new Rule("F-&[[X]\\+^X]/+^F[+^FX]\\-&X")});
//		rules1.Add('F', new List<Rule>(){new Rule(0.8f, "FF"), new Rule(0.2f, "F")});

		char maxXChar = (char)('A' + maxX);
		string maxXStr = string.Concat('X', maxXChar);
		Debug.Log(string.Format("Test: maxXChar = {0}, maxXStr = {1}", maxXChar, maxXStr));

		RuleSet rules2 = new RuleSet();
		rules2.AddRule(maxXStr, 0.32f, "[@[@/@/@+@FAXA]@\\@+@FAXA]@\\@-@FAXA");
		rules2.AddRule(maxXStr, 0.32f, "[@\\@+@FAXA]@\\@-@FAXA");
		rules2.AddRule(maxXStr, 0.32f, "[@/@+@FAXA]@/@-@FAXA");
		rules2.AddRule(maxXStr, 0.03f, "\\@-@FAXA");
		rules2.AddRule(maxXStr, 0.03f, "/@-@FAXA");

		rules2.AddRule(string.Concat('F', (char)('A' + maxF + 1)), 1f, string.Concat('F', (char)('A' + maxF)));

		this.lSystem = new LSystem("XA", rules2, 25, 70, 25, maxN);
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
			Debug.Log(string.Format("n({0}) = {1}", n, blueprint));
			Stem plant = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
			Stack<Stem> stemStack = new Stack<Stem>();
			float roll = 0;
			float pitch = 0;
			float yaw = 0;

			Stem currentStem = plant;
			currentStem.stemPrefab = stemPrefab;

			for (int i = 0; i < blueprint.Length; i+= 2)
			{
				switch (blueprint[i])
				{
				case 'F':
					currentStem = currentStem.AddStemSegment((int)blueprint[i+1]);
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

