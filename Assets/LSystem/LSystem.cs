using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSystem
{
	RuleSet rules;
	public float pitch = 25; // the change in angle that ^ and & make (in degrees).
	public float yaw = 25; // the change in angle that \ and / make (in degrees).
	public float roll = 25; // the change in angle that + and - make (in degrees).
	int maxN;

	List<string> results;

	public LSystem (string start, RuleSet rules, float pitch, float yaw, float roll, int maxN)
	{
		Random.InitState(System.Environment.TickCount);
		this.rules = rules;

		this.pitch = pitch;
		this.yaw = yaw;
		this.roll = roll;

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
		for (int i = s.Length - 2; i >= 0; i-=2) // back to front
		{
			string key = s.Substring(i, 2); // the char to analyze
			if (key[1] > '@')
			{
				string replacement = string.Concat(key[0], (char)(key[1] + 1));
				result = ApplyRule(replacement, result, i);
			}
			if (rules.ContainsKey(key)) // ensure the char has a rule
			{
				result = ApplyRule(rules.GetValue(key), result, i); // apply the rule
			}
		}
		return result;
	}

	// Replaces character at 'index' of 's' with a random string from the 'rule'
	public string ApplyRule(string replacement, string s, int index)
	{
		s = s.Remove(index, 2);
		return s.Insert(index, replacement);
	}
}

