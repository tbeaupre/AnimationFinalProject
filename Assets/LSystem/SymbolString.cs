using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolString
{
	List<Symbol> symString;

	public SymbolString ()
	{
	}

	public SymbolString (string s)
	{
		for (int i = 0; i < s.Length / 2; i += 2)
		{
			symString.Add(new Symbol(s[i], (int)s[i+1]));
		}
	}

	public void AddSymbol(Symbol sym)
	{
		symString.Add(sym);
	}

	public int Length()
	{
		return symString.Count;
	}

	public Symbol GetAt(int index)
	{
		return symString[index];
	}

	public SymbolString ReplaceAt(int index, SymbolString replacement)
	{
		symString.RemoveAt(index);
		symString.InsertRange(index, (List<Symbol>)replacement);
		return this;
	}

	public static explicit operator List<Symbol>(SymbolString symString)
	{
		return symString.symString;
	}
}

