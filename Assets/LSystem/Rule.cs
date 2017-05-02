using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rule
{
	public float prob { get; set; }
	public SymbolString output { get; set; }

	public Rule (float prob, SymbolString output)
	{
		this.prob = prob;
		this.output = output;
	}

	public Rule (SymbolString output)
	{
		this.prob = 1;
		this.output = output;
	}
}

