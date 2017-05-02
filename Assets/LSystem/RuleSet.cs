using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleSet
{
	Dictionary<string, List<Rule>> ruleset = new Dictionary<string, List<Rule>>();

	public RuleSet ()
	{
	}

	public void AddRule(string input, float probability, string output)
	{
		if (ruleset.ContainsKey(input))
		{
			ruleset[input].Add(new Rule(probability, output));
		} else
		{
			ruleset.Add(input, new List<Rule>(){ new Rule(probability, output) });
		}
	}

	public bool ContainsKey(string input)
	{
		return ruleset.ContainsKey(input);
	}

	public string GetValue(string key)
	{
		return ChooseRule(ruleset[key]);
	}

	public string ChooseRule(List<Rule> rule)
	{
		string result = "Error"; // This is the default, but it should never happen.
		if (rule.Count > 0)
		{
			result = rule[rule.Count - 1].output; // Default assuming there actually are some options

			float rand = Random.value;
			foreach (Rule option in rule)
			{
				if (rand < option.prob)
				{
					result = option.output;
					break;
				} else
				{
					rand -= option.prob;
				}
			}
		}
		return result;
	}
}

