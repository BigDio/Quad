using UnityEngine;
using System.Collections;

public class ItemBase
{
	private int _id;
	public int ID
	{
		get{return _id;}
		set{_id = value;}
	}
	
	public string Name
	{
		get{return _names[ID];}
		set{_names[ID] = value;}
	}

	private string _description;
	public string Description
	{
		get{return _description[ID].ToString();}
		set{_descriptions[ID] = value;}
	}
	
	public static int ItemCount
	{
		get
		{
			return _names.Length;
		}
	}

	public static string GetItemName(int index)
	{
		return _names[index];
	}
	public static string GetItemDescription(int index)
	{
		return _descriptions[index];
	}
	
	private static string[] _names = 
	{
		"铜质钥匙",				//00
		"打火石",				//01
		"柴刀",					//02
		"生猫肉",				//03
		"熟猫肉",				//04
		"窗帘",					//05
		"稻草人的手臂",			//06
		"自制火把",				//07
		"火把",					//08
		"汽油",					//09
		"木柴",					//10
		"木棍",					//11
		"干草",					//12
		"鱼竿",					//13
		"生鱼",					//14
		"烤鱼"					//15
	};
	
	private static string[] _descriptions = 
	{
		"一把锈迹斑斑的铜质钥匙。",								//00
		"两块打火石，可以用来生火。",								//01
		"有些钝了的柴刀，不过杀伤力应该还不错。",					//02
		"带血的猫肉，味道不怎么样。",								//03
		"烤熟的猫肉，味道应该不错。",			//04
		"一块窗帘布。",											//05
		"竹制的稻草人的手臂，只是根普通的竹棍。",					//06
		"自己制作的火把。",										//07
		"沾满汽油的火把，应该可以烧一阵子。",						//08
		"一桶汽油。",												//09
		"笨重的木柴，可以当燃料。",								//10
		"一根结实的木棍。",										//11
		"一堆干草，可以当做燃料。",								//12
		"长长的竹制鱼竿，上面还挂着饵料。",						//13
		"刚抓上来活蹦乱跳的生鱼。",								//14
		"闻起来很香的烤鱼。"										//15
	};
}