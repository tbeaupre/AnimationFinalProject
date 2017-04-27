using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystem
{
	Dictionary<char, List<Rule>> rules;
	public float angle = 25; // the change in angle that + and - make (in degrees).
	int maxN;

	List<string> results;

	public LSystem (string start, Dictionary<char, List<Rule>> rules, float angle, int maxN)
	{
		Random.InitState(System.Environment.TickCount);
		this.rules = rules;
		this.angle = angle;
		this.maxN = maxN;
		this.results = new List<string>(maxN + 1) {start};
	}

	public string GetResult(int n)
	{
		// First. check if the iteration asked for is greater than the max for this system.
		if (n > maxN)
		{
			n = maxN;
		}
		if (n < 0)
		{
			n = 0;
		}

		// Check if that iteration has already been calculated.
		if (n < results.Count)
		{
			return results[n];
		} else
		{
			return CalculateResult(n);
		}
	}

	public string CalculateResult(int n)
	{
		string result = results[results.Count - 1]; // the last string that has been calculated so far

		for (int j = results.Count - 1; j < n; j++)
		{
			result = ApplyRules(result);
			results.Add(result);
		}

		return result;
	}

	public string ApplyRules(string s)
	{
		string result = s; // the string to manipulate
		for (int i = s.Length - 1; i >= 0; i--) // back to front
		{
			char key = s[i]; // the char to analyze
			if (rules.ContainsKey(key)) // ensure the char has a rule
			{
				result = ApplyRule(rules[key], result, i); // apply the rule
			}
		}
		return result;
	}

	// Replaces character at 'index' of 's' with a random string from the 'rule'
	public string ApplyRule(List<Rule> rule, string s, int index)
	{
		string replacement = ChooseRule(rule);

		s = s.Remove(index, 1);
		return s.Insert(index, replacement);
	}

	public string ChooseRule(List<Rule> rule)
	{
		float rand = Random.value;
		foreach (Rule option in rule)
		{
			if (rand < option.prob)
			{
				return option.output;
			} else
			{
				rand -= option.prob;
			}
		}
		return "Error"; // This should never happen.
	}
}

