using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystem
{
	Dictionary<char, string> rules;
	public float angle = 25; // the change in angle that + and - make (in degrees).
	int maxN;

	List<string> results;

	public LSystem (string start, Dictionary<char, string> rules, float angle, int maxN)
	{
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
		string result = s; // result is the string to manipulate and return
		for (int i = s.Length - 1; i >= 0; i--) // start from the back and work forwards
		{
			char key = s[i]; // this is the character to analyze
			if (rules.ContainsKey(key)) // need to see if the character is a variable
			{
				result = ApplyRule(rules[key], result, i); // applies the rule at that location
			}
		}
		return result;
	}

	// Replaces character at 'index' of 's' with 'replacement'
	public string ApplyRule(string replacement, string s, int index)
	{
		s = s.Remove(index, 1);
		return s.Insert(index, replacement);
	}
}

