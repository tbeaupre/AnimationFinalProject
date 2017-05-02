using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolString
{
	List<Symbol> symList = new List<Symbol>();

	public SymbolString ()
	{
	}

	public SymbolString (SymbolString symString)
	{
		for (int i = 0; i < symString.Length(); i++)
		{
			this.symList.Add(new Symbol(symString.GetAt(i)));
		}
	}

	public SymbolString (string s)
	{
		for (int i = 0; i < s.Length; i += 2)
		{
			if (s[i + 1] == '@') // no age
			{
				symList.Add(new Symbol(s[i], -1));
			} else
			{
				symList.Add(new Symbol(s[i], (int)s[i + 1] - (int)'A'));
			}
		}
	}

	public void AddSymbol(Symbol sym)
	{
		symList.Add(sym);
	}

	public int Length()
	{
		return symList.Count;
	}

	public Symbol GetAt(int index)
	{
		return symList[index];
	}

	public void ReplaceAt(int index, SymbolString replacement)
	{
		symList.RemoveAt(index);
		symList.InsertRange(index, new SymbolString(replacement).symList);
	}

	public static explicit operator string(SymbolString symString)
	{
		string result = "";
		foreach (Symbol sym in symString.symList)
		{
			if (sym.age == -1)
			{
				result = string.Concat(result, sym.character);
				
			} else
			{
				result = string.Concat(result, string.Format("{0}({1})", sym.character, sym.age));
			}
		}
		return result;
	}

	public string toString()
	{
		return (string)this;
	}
}

