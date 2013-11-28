using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemUI : GameMaster 
{
	public static ItemUI Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("[ItemUI]").AddComponent<ItemUI>();
			return instance;
		}
	}
	private static ItemUI instance = null;
	
	void Awake()
	{
		if(instance)
			DestroyImmediate(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		showItemList = false;
	}

	public bool showItemList;
	public GUISkin LeftBackSkin;
	public GUISkin MiddleBackSkin;
	public GUISkin FrontSkin;
	public Texture2D ItemTexture;

	private int listWidth = 500;
	private int listHeight = 305;
	private int offset = 5;
	private int titleHeight = 50;
	private int descriptionHeight = 92;
	private int selectionJumpOffset = 32;
	private int selectionBoxHeight = 37;
	private int iconSize = 72;

	public int iconNum = 8;

	private Rect ItemTitleRect;
	private Rect ItemListRect;
	private Rect ItemListTextRect;
	private Rect ItemDescriptionRect;
	private Rect ItemDescriptionTextRect;
	private Rect ItemListNumberRect;
	private Rect SelectionBoxRect;
	private Rect IconBoxRect;
	public Rect IconUV;


	private string finalItemNumberString;
	private string finalItemNameString;
	private string finalItemDescriptionString;

	private string itemTitleString = "道具列表";
	private int totalPages;
	private int nowPage;
	private int nowLine;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			ToggleItemList();
		}
		if(showItemList && CharacterItems.Instance.CarryItem.Count !=0)
		{
			WaitForKey();
			WaitForKey2();
		}
		else if(showItemList)
		{
			WaitForKey2();
		}
	}
	void WaitForKey2()
	{
		if(Input.GetButtonDown("Use") || Input.GetKeyDown(KeyCode.Escape))
		{
			if(showItemList)
			{
				ToggleItemList();
			}
		}
	}

	void WaitForKey()
	{

		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			_GUISFX.PlaySelectSound();
			nowLine ++;
			RefreshSelectionBox();
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			_GUISFX.PlaySelectSound();
			nowLine --;
			RefreshSelectionBox();
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_GUISFX.PlaySelectSound();
			nowPage --;
			RefreshSelectionBox();
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			_GUISFX.PlaySelectSound();
			nowPage ++;
			RefreshSelectionBox();
		}
	}

	public void ToggleItemList()
	{

		showItemList = !showItemList;
		if(showItemList)
		{
			if(_GamePause.canPause && !_GamePause.paused)
			{
				_GamePause.OnlyPauseGame();
				InitializeItemList();
				_GUISFX.PlayEnterMenuSound();
			}
			else
			{
				showItemList = !showItemList;
				_GUISFX.PlayQuitMenuSound();
			}
		}
		else
		{
			CloseItemList();
		}
	}

	void OnGUI()
	{
		if(showItemList)
		{
			ShowItemTitleRect();
			ShowDescriptionRect();
			ShowItemListRect();
			if(CharacterItems.Instance.CarryItem.Count !=0)
			{
				ShowItemIconRect();
				ShowSelectionBox();
			}
			ShowItemListName();
			ShowItemNumberRect();

		}
	}

	#region 显示窗口区域方法

	void ShowItemTitleRect()
	{
		GUI.skin = MiddleBackSkin;
		GUI.Box(ItemTitleRect,itemTitleString);
	}

	void ShowItemListRect()
	{
		GUI.skin = LeftBackSkin;
		GUI.Box(ItemListRect,"");
	}

	void ShowItemNumberRect()
	{
		GUI.skin = LeftBackSkin;
		GUI.Label(ItemListNumberRect,finalItemNumberString);
	}

	void ShowItemListName()
	{
		GUI.skin = LeftBackSkin;
		GUI.Label(ItemListTextRect,finalItemNameString);
	}

	void ShowDescriptionRect()
	{
		GUI.skin = LeftBackSkin;
		GUI.Box(ItemDescriptionRect,"");
		if(CharacterItems.Instance.CarryItem.Count !=0)
			GUI.Label(ItemDescriptionTextRect,finalItemDescriptionString);
	}

	void ShowSelectionBox()
	{
		GUI.skin = FrontSkin;
		GUI.Box(SelectionBoxRect,"");
	}

	void ShowItemIconRect()
	{
		GUI.skin = MiddleBackSkin;
		GUI.DrawTextureWithTexCoords(IconBoxRect,ItemTexture,IconUV);
	}

	#endregion

	#region 初始化道具窗口

	public void InitializeItemList()
	{
		ItemTitleRect = new Rect((int)((float)(Screen.width - listWidth)/2),
		                         (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2),
		                         listWidth,titleHeight);
		ItemListTextRect = new Rect((int)((float)(Screen.width - listWidth)/2 ) + offset*4,
		                        (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset*2,
		                        listWidth - 2*offset,listHeight - 3*offset);
		ItemListNumberRect = new Rect((int)((float)(Screen.width - listWidth)/2 ) + listWidth - 50,
		                            (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset*2,
		                            50,listHeight - 3*offset);
		ItemListRect = new Rect((int)((float)(Screen.width - listWidth)/2),
		                            (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset,
		                            listWidth,listHeight);
		ItemDescriptionRect = new Rect((int)((float)(Screen.width - listWidth)/2),
		                               (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset*2 + listHeight,
		                               listWidth,descriptionHeight);
		ItemDescriptionTextRect = new Rect((int)((float)(Screen.width - listWidth)/2 + offset * 3 + iconSize),
		                                   (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset*4 + listHeight,
		                                   listWidth - 4*offset - iconSize,descriptionHeight - offset);
		IconBoxRect = new Rect((int)((float)(Screen.width - listWidth)/2 + offset * 2),
		                 	  (int)((float)(Screen.height - listHeight - titleHeight - offset*3 - descriptionHeight)/2) + titleHeight + offset*4 + listHeight,
		                       iconSize,iconSize);
		RefreshSelectionBox();
	}

	void RefreshSelectionBox()
	{
		totalPages = (int)(CharacterItems.Instance.CarryItem.Count / 9);
		if(totalPages > 0)
		{
			if(nowPage < 0)
				nowPage = totalPages;

			else if(nowPage > totalPages)
				nowPage = 0;

			if(nowPage != totalPages)
			{
				if(nowLine < 0)
					nowLine = 8;
				else if(nowLine >8)
					nowLine = 0;
			}
			else
			{
				if(nowLine < 0)
					nowLine = CharacterItems.Instance.CarryItem.Count % 9 - 1;
				else if(nowLine > CharacterItems.Instance.CarryItem.Count % 9 - 1)
					nowLine = 0;
			}
		}
		else
		{
			nowPage = 0;
			if(nowLine < 0)
				nowLine = CharacterItems.Instance.CarryItem.Count % 9 - 1;
			else if(nowLine > CharacterItems.Instance.CarryItem.Count % 9 - 1)
				nowLine = 0;
		}
		SelectionBoxRect = new Rect((int)((float)(Screen.width - listWidth)/2 ) + offset*2 - 2,
	                               (int)((float)(Screen.height - listHeight - titleHeight - offset*2 - descriptionHeight)/2) + titleHeight + offset + 4 + nowLine*selectionJumpOffset,
	                               listWidth - 3*offset -2,selectionBoxHeight);
		RefreshItemStrings();
	}

	public void RefreshItemStrings()
	{
		Debug.Log(totalPages);
		finalItemNumberString = "";
		finalItemNameString = "";
		if(totalPages > 0)
		{
			itemTitleString = "<- 道具列表 第" + (nowPage + 1).ToString() + "/" + (totalPages+1).ToString() + "页 ->";
		}
		int temp = 0;//物品列表中第几个物品
		int nowIndex = nowPage * 9 + nowLine; //光标是第几个物品
		foreach(int index in CharacterItems.Instance.CarryItem.Keys)
		{
			if(temp == nowIndex)
			{
				finalItemDescriptionString = Item.GetItemDescription(index);
				int uIndex = index % 8;
				int vIndex = index / 8;
				IconUV = new Rect(uIndex /(float)(iconNum),(float)(iconNum-1)/(float)(iconNum) - vIndex/(float)(iconNum),1.0f/(float)(iconNum),1.0f/(float)(iconNum));
			}

			if(nowPage == 0 && temp <= 8)
			{
				finalItemNumberString += CharacterItems.Instance.CarryItem[index] + "\n";
				finalItemNameString += Item.GetItemName(index) + "\n";
			}
			else if(nowPage == 1 && temp > 8 && temp <= 17)
			{
				finalItemNumberString += CharacterItems.Instance.CarryItem[index] + "\n";
				finalItemNameString += Item.GetItemName(index) + "\n";
			}
			else if(nowPage == 2 && temp > 17 && temp <= 26)
			{
				finalItemNumberString += CharacterItems.Instance.CarryItem[index] + "\n";
				finalItemNameString += Item.GetItemName(index) + "\n";
			}
			else if(nowPage == 3 && temp > 26 && temp <= 35)
			{
				finalItemNumberString += CharacterItems.Instance.CarryItem[index] + "\n";
				finalItemNameString += Item.GetItemName(index) + "\n";
			}
			else if(nowPage == 3 && temp > 35 && temp <= 44)
			{
				finalItemNumberString += CharacterItems.Instance.CarryItem[index] + "\n";
				finalItemNameString += Item.GetItemName(index) + "\n";
			}
			temp++;
		}
	}

	#endregion 

	void CloseItemList()
	{
		_GamePause.OnlyResumeGame();
	}
}
