/// <summary>
/// 游戏中所需要的数据的调用方法
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour 
{
	private static GameData instance;
	public static GameData Instance
	{
		get
		{
			if(instance == null)
				instance = new GameData();
			return instance;
		}
	}

	void Awake()
	{
		if(instance)
			DestroyImmediate(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}


	public int ResolutionX
	{
		get{return PlayerPrefs.GetInt("Resolution X", 1024);}
		set{PlayerPrefs.SetInt("Resolution X", value);
		}
	}
	
	public int ResolutionY
	{
		get{return PlayerPrefs.GetInt("Resolution Y", 768);}
		set{PlayerPrefs.SetInt("Resolution Y", value);
		}
	}
	
	public int Lang
	{
		get{return PlayerPrefs.GetInt("Language", 1);}
		set{PlayerPrefs.SetInt("Language", value);
		}
	}
	
	public string PlayerName
	{
		get{return PlayerPrefs.GetString("Player Name", "No Name");}
		set{PlayerPrefs.SetString("Player Name", value);
		}
	}

	public int SFXActive
	{
		get{return PlayerPrefs.GetInt("SFX Active", 1);}
		set{PlayerPrefs.SetInt("SFX Active", value);
		}
	}

	public int BGMActive
	{
		get{return PlayerPrefs.GetInt("BGM Active", 1);}
		set{PlayerPrefs.SetInt("BGM Active", value);
		}
	}
}
