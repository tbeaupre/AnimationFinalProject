using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystemImplement : MonoBehaviour
{
	public Stem stemPrefab;
	LSystem lSystem;
	public int numIterations = 130;
	public int branchTime = 15;
	public float pitch = 15;
	public float yaw = 80;
	public float roll = 25;
	List<Stem> stemList = new List<Stem>();
	Stem root = null;

	// Use this for initialization
	void Start ()
	{
		RuleSet rules = new RuleSet();
		Symbol maxXSym = new Symbol('X', branchTime);
		rules.AddRule(new Symbol('S', branchTime), 0.3f, new SymbolString("[@[@/@/@+@FAXA]@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(new Symbol('S', branchTime), 0.3f, new SymbolString("[@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(new Symbol('S', branchTime), 0.2f, new SymbolString("FAXA"));

		rules.AddRule(maxXSym, 0.16f, new SymbolString("[@[@\\@\\@+@FAXA]@/@+@FAXA]@/@-@FAXA"));
		rules.AddRule(maxXSym, 0.16f, new SymbolString("[@[@/@/@+@FAXA]@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(maxXSym, 0.32f, new SymbolString("[@\\@+@FAXA]@\\@-@FAXA"));
		rules.AddRule(maxXSym, 0.32f, new SymbolString("[@/@+@FAXA]@/@-@FAXA"));
		rules.AddRule(maxXSym, 0.03f, new SymbolString("\\@-@FAXA"));
		rules.AddRule(maxXSym, 0.03f, new SymbolString("/@-@FAXA"));

		this.lSystem = new LSystem(new Symbol('S', 10), rules, yaw, pitch, roll, numIterations);
		if (root == null)
		{
			root = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
			root.age = 1;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	// Starts with a symbolstring from an lsystem and creates/updates Stem segments to match the symbolstring
	public void CreateGameObject (int n)
	{
		if (n < numIterations)
		{
			if (lSystem != null)
			{
				SymbolString blueprint = lSystem.GetResult(n);

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
							pitch += Random.Range(-3f, 3f);
							yaw += Random.Range(-3f, 3f);
							roll += Random.Range(-3f, 3f);
							Stem newStem = currentStem.AddStemSegment(sym.age, pitch, yaw, roll);
							stemList.Add(newStem);
							result.ReplaceAt(i, (SymbolString)new Symbol(sym.character, sym.age, stemList.Count - 1));
							newStem.id = result.GetAt(i).id;
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
				root.UpdateChildTransforms(0);
				Debug.Log(string.Format("n({0}) = {1}", n, (string)result));
			} else
			{
				this.Start();
				this.CreateGameObject(n);
			}
		}
	}
}

