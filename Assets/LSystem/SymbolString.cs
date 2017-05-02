using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolString
{
	List<Symbol> symList;

	public SymbolString ()
	{
		this.symList = new List<Symbol>();
	}

	public SymbolString (string s)
	{
		this.symList = new List<Symbol>();
		for (int i = 0; i < s.Length / 2; i += 2)
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

	public SymbolString ReplaceAt(int index, SymbolString replacement)
	{
		symList.RemoveAt(index);
		symList.InsertRange(index, (List<Symbol>)replacement);
		return this;
	}

	public static explicit operator List<Symbol>(SymbolString symString)
	{
		return symString.symList;
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
}

