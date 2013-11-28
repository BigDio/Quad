using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FlagType
{
	Bool = 0,
	Int = 1,
	String = 2
}

public class FlagBase
{
	public int ID;
	public FlagType flagType;
}
