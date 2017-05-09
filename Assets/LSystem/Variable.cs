using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Variable
{
	public char character { get; set; }
	public int maxAge { get; set; }

	public Variable (char character, int maxAge)
	{
		this.character = character;
		this.maxAge = maxAge;
	}
}

