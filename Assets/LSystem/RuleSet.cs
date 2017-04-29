using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleSet
{
	Dictionary<char, List<Rule>> ruleset = new Dictionary<char, List<Rule>>();

	public RuleSet ()
	{
	}

	public void AddRule(char input, float probability, string output)
	{
		if (ruleset.ContainsKey(input))
		{
			ruleset[input].Add(new Rule(probability, output));
		} else
		{
			ruleset.Add(input, new List<Rule>(){ new Rule(probability, output) });
		}
	}

	public bool ContainsKey(char input)
	{
		return ruleset.ContainsKey(input);
	}

	public List<Rule> GetValue(char key)
	{
		return ruleset[key];
	}
}

