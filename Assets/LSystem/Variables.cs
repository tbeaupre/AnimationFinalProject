using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Variables
{
	public static int GetMaxAge(List<Variable> vars, Symbol key)
	{
		foreach (Variable variable in vars)
		{
			if (key.character == variable.character)
			{
				return variable.maxAge;
			}
		}
		return 0;
	}
}

