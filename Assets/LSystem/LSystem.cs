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

	List<SymbolString> results;

	public LSystem (Symbol start, RuleSet rules, float pitch, float yaw, float roll, int maxN)
	{
		Random.InitState(System.Environment.TickCount);
		this.rules = rules;

		this.pitch = pitch;
		this.yaw = yaw;
		this.roll = roll;

		this.maxN = maxN;
		this.results = new List<SymbolString>(maxN + 1) {(SymbolString)start};
	}

	public SymbolString GetResult(int n)
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

	public SymbolString CalculateResult(int n)
	{
		SymbolString result = results[results.Count - 1]; // the last string that has been calculated so far

		for (int j = results.Count - 1; j < n; j++)
		{
			result = ApplyRules(result);
			results.Add(result);
		}

		return result;
	}

	public SymbolString ApplyRules(SymbolString s)
	{
		SymbolString result = s; // the string to manipulate
		for (int i = s.Length() - 1; i >= 0; i--) // back to front
		{
			Symbol key = result.GetAt(i); // the char to analyze
			if (key.age > 0)
			{
				key.age++;
			}
			if (rules.ContainsKey(key)) // ensure the char has a rule
			{
				result = result.ReplaceAt(i, rules.GetValue(key)); // apply the rule
			}
		}
		return result;
	}
}

