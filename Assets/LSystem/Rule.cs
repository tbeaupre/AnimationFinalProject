using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rule
{
	public float prob;
	public string output;

	public Rule (float prob, string output)
	{
		this.prob = prob;
		this.output = output;
	}

	public Rule (string output)
	{
		this.prob = 1;
		this.output = output;
	}
}

