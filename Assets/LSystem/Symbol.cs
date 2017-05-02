using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Symbol
{
	public char character { get; set; }
	public int age { get; set; }
	public int id { get; set; }

	public Symbol (Symbol sym)
	{
		this.character = sym.character;
		this.age = sym.age;
		this.id = sym.id;
	}

	public Symbol (char character, int age)
	{
		this.character = character;
		this.age = age;
		this.id = -1;
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

	public static explicit operator string(Symbol sym)
	{
		if (sym.age == -1)
		{
			return string.Format("{0}{1}", sym.character, '@');
		} else
		{
			return string.Format("{0}{1}", sym.character, (char)(sym.age + (int)'A'));
		}
	}
}

