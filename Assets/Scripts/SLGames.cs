using UnityEngine;
using System.Collections;

public class SLGames : MonoBehaviour 
{
	public static SLGames Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("[SLGames]").AddComponent<SLGames>();
			return instance;
		}
	}
	private static SLGames instance = null;
	
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

	private int _currentGameSlot;
	public int CurrentGameSlot
	{
		set{_currentGameSlot = value;}
		get{return _currentGameSlot;}
	}

	private int _targetGameSlot;
	public int TargetGameSlot
	{
		set{_targetGameSlot = value;}
		get{return _targetGameSlot;}
	}

	private Vector3 playerPos;
	private int saverSlot;

	void Start()
	{
		if(PlayerPrefs.GetInt("IsFromLoad", 0) != 0)
		{
			TargetGameSlot = PlayerPrefs.GetInt("IsLoadFrom");
			AfterLoad();
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			BottomMessageSender.Instance.Send("正在保存存档1");
			SaveGame(1);
		}

		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			BottomMessageSender.Instance.Send("正在保存存档2");
			SaveGame(2);
		}

		if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			BottomMessageSender.Instance.Send("正在保存存档3");
			SaveGame(3);
		}

		if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			BottomMessageSender.Instance.Send("读取存档1");
			LoadGame(1);
		}

		if(Input.GetKeyDown(KeyCode.Keypad5))
		{
			BottomMessageSender.Instance.Send("读取存档2");
			LoadGame(2);
		}

		if(Input.GetKeyDown(KeyCode.Keypad6))
		{
			BottomMessageSender.Instance.Send("读取存档3");
			LoadGame(3);
		}

	}

	public void AfterLoad()
	{
		LoadPlayerPosition();
		LoadPlayerItems();
		LoadPlayerFlags();
		CurrentGameSlot = PlayerPrefs.GetInt("IsLoadFrom");
		Invoke("CoolDown",1.0f);
		BottomMessageSender.Instance.Send("存档"+ TargetGameSlot + "已读取");
	}

	public void CoolDown()
	{
		PlayerPrefs.SetInt("IsFromLoad", 0);
		PlayerPrefs.SetInt("IsLoadFrom", 0);
	}

	public void SaveGame(int index)
	{
		TargetGameSlot = index;

		PlayerPrefs.SetInt("Slot_" + TargetGameSlot + "_HasData", 1);
		SaveScene();
		SavePlayerPosition();
		SavePlayerItems();
		SavePlayerFlags();
		BottomMessageSender.Instance.Send("存档"+ TargetGameSlot + "已保存");
	}

	public void LoadGame(int index)
	{
		TargetGameSlot = index;
		if(PlayerPrefs.HasKey("Slot_" + TargetGameSlot + "_HasData"))
		{
			if(PlayerPrefs.GetInt("Slot_" + TargetGameSlot + "_HasData", 0) == 1)
			{
				PlayerPrefs.SetInt("IsFromLoad", TargetGameSlot);
				PlayerPrefs.SetInt("IsLoadFrom", index);
				LoadScene();

				return;
			}
		}
		BottomMessageSender.Instance.Send("存档"+ TargetGameSlot + "没有存档！");
	}

	void SavePlayerPosition()
	{
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_PlayerPosX", playerPos.x);
		PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_PlayerPosY", playerPos.y);
		PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_PlayerPosZ", playerPos.z);
		Debug.Log(TargetGameSlot);
	}

	void LoadPlayerPosition()
	{
		if(PlayerPrefs.GetInt("Slot_" + TargetGameSlot + "_HasData", 0) == 0)
		{
			Debug.Log(TargetGameSlot);
			return;
		}
		else
		{
			float tempX = PlayerPrefs.GetFloat("Slot_" + TargetGameSlot + "_PlayerPosX", playerPos.x);
			float tempY = PlayerPrefs.GetFloat("Slot_" + TargetGameSlot + "_PlayerPosY", playerPos.y) + 0.2f;
			float tempZ = PlayerPrefs.GetFloat("Slot_" + TargetGameSlot + "_PlayerPosZ", playerPos.z);
			GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(tempX,tempY,tempZ);
			Debug.Log(TargetGameSlot);
		}
	}

	public void SaveScene()
	{
		PlayerPrefs.SetInt("Slot_" + TargetGameSlot + "_Level", Application.loadedLevel);
	}

	public void LoadScene()
	{
		if(PlayerPrefs.HasKey("Slot_" + TargetGameSlot + "_Level"))
			Application.LoadLevel(PlayerPrefs.GetInt("Slot_" + TargetGameSlot + "_Level"));
	}

	public void SavePlayerItems()
	{
		CharacterItems.Instance.SaveItems();
	}

	public void LoadPlayerItems()
	{
		CharacterItems.Instance.LoadItems();
	}

	public void LoadPlayerFlags()
	{
		Flags.Instance.LoadFlag();
	}

	public void SavePlayerFlags()
	{
		Flags.Instance.SaveFlag();
	}

//	public void SaveNPCsPos()
//	{
//		GameObject[] NPCs;
//		NPCs = GameObject.FindGameObjectsWithTag("NPC");
//		foreach(GameObject go in NPCs)
//		{
//			PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_NPC_" + go.name + "_xPos",go.transform.position.x);
//			PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_NPC_" + go.name + "_yPos",go.transform.position.y);
//			PlayerPrefs.SetFloat("Slot_" + TargetGameSlot + "_NPC_" + go.name + "_zPos",go.transform.position.z);
//		}
//	}

//	public void LoadNPCsPos()
//	{
//
//	}

	public void ClearSaveData(int index)
	{
		TargetGameSlot = index;
		PlayerPrefs.SetInt("Slot_" + TargetGameSlot + "_HasData", 0);
	}
}
