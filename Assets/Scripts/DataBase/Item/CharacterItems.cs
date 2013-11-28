using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterItems : MonoBehaviour 
{
	public Dictionary<int,int> CarryItem;
	private Item item;

	public static CharacterItems Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("[CharacterItems]").AddComponent<CharacterItems>();
			return instance;
		}
	}
	private static CharacterItems instance = null;

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
	
	void Start()
	{
		if(PlayerPrefs.GetInt("IsFromLoad", 0) == 0)
			Setup();
	}

	public void Setup()
	{
		CarryItem = new Dictionary<int, int>();
		Debug.Log("新建道具字典");
		item = new Item();
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			AddItem(0,1);
			AddItem(1,1);
			AddItem(2,1);
			AddItem(3,1);
			AddItem(4,1);
			AddItem(5,1);
			AddItem(6,1);
			AddItem(7,1);
			AddItem(8,1);
			AddItem(9,1);
			AddItem(10,1);
			AddItem(11,1);
			AddItem(12,1);
			AddItem(13,1);
			AddItem(14,1);
			AddItem(15,1);
		}
//		if(Input.GetKeyDown(KeyCode.I))
//		{
//			ListItem();
//		}
		if(Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			AddItem(0,-1);
			AddItem(1,-1);
			AddItem(2,-1);
			AddItem(3,-1);
			AddItem(4,-1);
			AddItem(5,-1);
			AddItem(6,-1);
			AddItem(7,-1);
			AddItem(8,-1);
			AddItem(9,-1);
			AddItem(10,-1);
			AddItem(11,-1);
			AddItem(12,-1);
			AddItem(13,-1);
			AddItem(14,-1);
			AddItem(15,-1);
		}
	}

	public void AddItem(int index, int quantity)
	{
		item.ID = index;
		if(CarryItem.ContainsKey(index))
		{
			CarryItem[index] += quantity;
			CarryItem[index] = Mathf.Max(0,CarryItem[index]);
		}
		else
		{
			CarryItem.Add(index, quantity);
			CarryItem[index] = Mathf.Max(0,CarryItem[index]);
		}
		BottomMessageSender.Instance.Send("获得" + quantity + "个 " + item.Name + " ，目前一共有" + CarryItem[index] + "个。");
		OptimizeItem();
		ItemUI.Instance.InitializeItemList();
	}

	public void SetItem(int index, int quantity)
	{
		item.ID = index;
		if(CarryItem.ContainsKey(index))
			CarryItem[index] = quantity;
		else
			CarryItem.Add(index, quantity);
		BottomMessageSender.Instance.Send("将道具 "+ item.Name + " 的数量设置为" + CarryItem[index] + "个。");
		OptimizeItem();
		ItemUI.Instance.InitializeItemList();
	}

	public void ListItem()
	{
		foreach(int index in CarryItem.Keys)
		{
			item.ID = index;
			BottomMessageSender.Instance.Send(item.Name + " : " +  CarryItem[index]);
		}
		OptimizeItem();
		if(CarryItem.Count == 0)
		{
			BottomMessageSender.Instance.Send("当前未持有任何道具");
		}
	}

//	public Dictionary<int, int> ListItem()
//	{
//		Dictionary<int, int> temp;
//
//		foreach(int index in CarryItem.Keys)
//		{
//			item.ID = index;
//
//			BottomMessageSender.Instance.Send(item.Name + " : " +  CarryItem[index]);
//		}
//		OptimizeItem();
//		if(CarryItem.Count == 0)
//		{
//			BottomMessageSender.Instance.Send("当前未持有任何道具");
//		}
//	}
	public void OptimizeItem()
	{
		for(int index = 0; index < Item.ItemCount; index++)
		{
			item.ID = index;
			if(CarryItem.ContainsKey(index))
			{
				if(CarryItem[index] <= 0)
				{
					CarryItem.Remove(index);
					Debug.Log("移除了"+item.Name+"条目");
				}
			}
		}
	}

	public void LoadItems()
	{
		Setup();
		int targetSlot = SLGames.Instance.TargetGameSlot;
		for(int index = 0; index < Item.ItemCount; index++)
		{
			item.ID = index;
			CarryItem.Add(index,PlayerPrefs.GetInt("Slot_" + targetSlot + "_Item_" + index,0));
			Debug.Log("载入道具" + item.Name + " : " + PlayerPrefs.GetInt("Slot_" + targetSlot + "_Item_" + index,0));
		}
		OptimizeItem();
	}
	
	public void SaveItems()
	{
		OptimizeItem();
		int targetSlot = SLGames.Instance.TargetGameSlot;
		ClearSavedItem();
		foreach(int index in CarryItem.Keys)
		{
			item.ID = index;
			PlayerPrefs.SetInt("Slot_" + targetSlot + "_Item_" + index,CarryItem[index]);
			Debug.Log("存入道具 : " + "Slot_" + targetSlot + "_Item_" + index + " : " + CarryItem[index]);
		}
	}

	public void ClearSavedItem()
	{
		int targetSlot = SLGames.Instance.TargetGameSlot;
		for(int i = 0; i < Item.ItemCount; i++)
		{
			if(PlayerPrefs.HasKey("Slot_" + targetSlot + "_Item_" + i))
			{
				PlayerPrefs.DeleteKey("Slot_" + targetSlot + "_Item_" + i);
				item.ID = i;
				Debug.Log("存档槽"+ targetSlot + "中的" +  item.Name + "被移除");
			}
		}
	}
}
