using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Symbol
{
	public char character { get; set; }
	public int age { get; set; }
	public int id { get; set; }

	public Symbol (char character, int age)
	{
		this.character = character;
		this.age = age;
		this.id = null;
	}

	public Symbol (char character, int age, int id)
	{
		this.character = character;
		this.age = age;
		this.id = id;
	}

	public static explicit operator SymbolString(Symbol sym)
	{
		SymbolString result = new SymbolString();
		result.AddSymbol(sym);
		return result;
	}
}

