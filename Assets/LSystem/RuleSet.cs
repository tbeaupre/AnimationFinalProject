using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleSet
{
	Dictionary<string, List<Rule>> ruleset = new Dictionary<string, List<Rule>>();

	public RuleSet ()
	{
	}

	// Adds a rule to the set.
	public void AddRule(Symbol input, float probability, SymbolString output)
	{
		if (ruleset.ContainsKey((string)input))
		{
			ruleset[(string)input].Add(new Rule(probability, output));
		} else
		{
			ruleset.Add((string)input, new List<Rule>(){ new Rule(probability, output) });
		}
	}

	// Checks to see if the ruleset has an a rule associated with the given symbol
	public bool ContainsKey(Symbol input)
	{
		return ruleset.ContainsKey((string)input);
	}

	// Returns a randomly selected symbolstring from the outputs associated with the key
	public SymbolString GetValue(Symbol key)
	{
		return ChooseRule(ruleset[(string)key]);
	}

	// Chooses a random output string from the list
	public SymbolString ChooseRule(List<Rule> rule)
	{
		SymbolString result = new SymbolString(); // This is the default, but it should never happen.
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

