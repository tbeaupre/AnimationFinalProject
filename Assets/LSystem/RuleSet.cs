using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleSet
{
	Dictionary<Symbol, List<Rule>> ruleset = new Dictionary<Symbol, List<Rule>>();

	public RuleSet ()
	{
	}

	public void AddRule(Symbol input, float probability, SymbolString output)
	{
		if (ruleset.ContainsKey(input))
		{
			ruleset[input].Add(new Rule(probability, output));
		} else
		{
			ruleset.Add(input, new List<Rule>(){ new Rule(probability, output) });
		}
	}

	public bool ContainsKey(Symbol input)
	{
		return ruleset.ContainsKey(input);
	}

	public SymbolString GetValue(Symbol key)
	{
		return ChooseRule(ruleset[key]);
	}

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

