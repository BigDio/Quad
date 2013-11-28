using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flags : MonoBehaviour 
{
	public static Flags Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("[Flags]").AddComponent<Flags>();
			return instance;
		}
	}
	private static Flags instance = null;
	public Dictionary<string, bool> Flag;

	public string[] flagList =
	{
		"FirstRiddle",
		"StrawManNoArm",
		"Chest01"
	};

	void Awake()
	{
		if(instance)
			DestroyImmediate(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		Setup();
	}

	public void SetFlag(string flagName)
	{
		if(Flag.ContainsKey(flagName))
			return;
		else
		{
			Flag.Add(flagName,true);
			Debug.Log("增加了Flag:" + flagName);
		}
	}

	public void RipFlag(string flagName)
	{
		if(Flag.ContainsKey(flagName))
		{
			Flag.Remove(flagName);
			Debug.Log("移除了Flag:" + flagName);
		}
		else
			return;
	}

	public bool HasFlag(string flagName)
	{
		if(Flag.ContainsKey(flagName))
			return true;
		else
			return false;
	}

	void Start()
	{
		if(PlayerPrefs.GetInt("IsFromLoad", 0) == 0)
			Setup();
	}

	public void Setup()
	{
		Flag = new Dictionary<string, bool>();
	}

	public void SaveFlag()
	{
		int cnt = 0;
		ClearSavedFlags();
		foreach (string i in Flag.Keys)
		{
			if(Flag.ContainsKey(i))
			{
				PlayerPrefs.SetString("Slot_" + SLGames.Instance.TargetGameSlot + "_Flag_" + cnt, i);
				cnt++;
			}
		}
	}

	public void LoadFlag()
	{
		Flag = new Dictionary<string, bool>();
		for(int cnt = 0; cnt < 100; cnt++)
		{
			if(PlayerPrefs.HasKey("Slot_" + SLGames.Instance.TargetGameSlot + "_Flag_" + cnt))
			{
				SetFlag(PlayerPrefs.GetString("Slot_" + SLGames.Instance.TargetGameSlot + "_Flag_" + cnt));
			}
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			ListFlag();
		}
	}

	public void ListFlag()
	{
		foreach(string i in Flag.Keys)
		{
			BottomMessageSender.Instance.Send("当前树立的Flag:" + i);
		}
		if(Flag.Count == 0)
		{
			BottomMessageSender.Instance.Send("当前没有树立任何Flag");
		}
	}

	public void ClearSavedFlags()
	{
		for(int i = 0; i < flagList.Length; i++)
		{
			if(PlayerPrefs.HasKey("Slot_" + SLGames.Instance.TargetGameSlot + "_Flag_" + i))
			{
				PlayerPrefs.DeleteKey("Slot_" + SLGames.Instance.TargetGameSlot + "_Flag_" + i);
				Debug.Log("存档槽"+ SLGames.Instance.TargetGameSlot + "中的Flag : " + flagList[i] + "已被清除");
			}
		}
	}
}
