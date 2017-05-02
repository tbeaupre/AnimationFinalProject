﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemImplement : MonoBehaviour
{
	public Stem stemPrefab;
	LSystem lSystem;
	public int maxN = 100;
	public int maxX = 20;
	public int maxF = 120;
	List<Stem> stemList = new List<Stem>();
	Stem root;

	// Use this for initialization
	void Start ()
	{
		RuleSet rules = new RuleSet();
		rules.AddRule(new Symbol('X', maxX), 0.32f, new SymbolString("[@[@/@/@+@FAXA]@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(new Symbol('X', maxX), 0.32f, new SymbolString("[@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(new Symbol('X', maxX), 0.32f, new SymbolString("[@/@+@FAXA]@/@-@FAXA"));
		rules.AddRule(new Symbol('X', maxX), 0.03f, new SymbolString("\\@-@FAXA"));
		rules.AddRule(new Symbol('X', maxX), 0.03f, new SymbolString("/@-@FAXA"));

		this.lSystem = new LSystem(new Symbol('X', 10), rules, 30, 70, 30, maxN);
		root = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public Stem CreateGameObject (int n)
	{
		if (lSystem != null)
		{
			SymbolString blueprint = lSystem.GetResult(n);
			Debug.Log(string.Format("n({0}) = {1}", n, (string)blueprint));

			SymbolString result = blueprint; // The blueprint edited to include the id of the stem segment

			Stack<Stem> stemStack = new Stack<Stem>();
			float roll = 0;
			float pitch = 0;
			float yaw = 0;

			Stem currentStem = root;
			currentStem.stemPrefab = stemPrefab;

			for (int i = 0; i < blueprint.Length(); i++)
			{
				Symbol sym = blueprint.GetAt(i);
				switch (sym.character)
				{
				case 'F':
					if (sym.id == -1)
					{
						Stem newStem = currentStem.AddStemSegment(sym.age, pitch, yaw, roll);
						stemList.Add(newStem);
						result.ReplaceAt(i, (SymbolString)new Symbol(sym.character, sym.age, stemList.Count - 1));
						currentStem = newStem;
					} else
					{
						stemList[sym.id].UpdateStem(sym.age);
						currentStem = stemList[sym.id];
					}
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
			return root;
		} else
		{
			this.Start();
			return this.CreateGameObject(n);
		}
	}
}

