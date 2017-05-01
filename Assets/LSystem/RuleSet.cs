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

	public List<Rule> GetValue(string key)
	{
		return ruleset[key];
	}
}

