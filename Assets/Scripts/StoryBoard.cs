/// <summary>
/// 故事版
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryBoard : StoryBoardEngine
{
	public GameObject Strawman;
	public GameObject Chest01;
	
	#region 剧情
	private IEnumerator Sign001()
	{
		Dialog("路牌|前方正在施工。","Idle");
		yield return null;
	}
	private IEnumerator Sign002()
	{
		string[] temp = 
		{
			"更新说明|按下空格键继续对话",
			"更新说明|本次更新内容如下:",
			"更新说明|1.增加了物品UI",
			"更新说明|按下I键可以打开物品栏",
			"更新说明|2.增加部分美术资源",
			"更新说明|3.优化了按键设置"
		};
		Dialog(temp);
		yield return null;
	}
	private IEnumerator Chest001_01()
	{
		Dialog("一只箱子，里面似乎有什么东西。","Chest001_02");
		yield return null;
	}
	private IEnumerator Chest001_02()
	{
		Ask("是否打开箱子？","是\n否","Chest001_03","Idle");
		yield return null;
	}
	private IEnumerator Chest001_03()
	{
		if(CharacterItems.Instance.CarryItem.ContainsKey(6))
		{
			Chest01.GetComponent<AtlasManager>().SetAtlas(16,16,0,1);
			Dialog("使用稻草人的手臂撬开了箱子","Chest001_04");
		}
		else
		{
			Dialog("箱子锁似乎死死地卡住了……","Idle");
			Flags.Instance.SetFlag("FirstRiddle");
			Chest01.GetComponent<Trigger_Chest01>().state = Trigger_Chest01.ChestState.wait;
		}
		yield return null;
	}
	private IEnumerator Chest001_04()
	{
		Dialog("获得了道具 " + Item.GetItemName(0) + " 。","Idle");
		CharacterItems.Instance.AddItem(0,1);
		Flags.Instance.SetFlag("Chest01");
		Chest01.GetComponent<Trigger_Chest01>().state = Trigger_Chest01.ChestState.empty;
		yield return null;
	}
	private IEnumerator Chest001_05()
	{
		Dialog("一只空箱子。","Idle");
		yield return null;
	}
	private IEnumerator StrawMan000()
	{
		Dialog("一只稻草人。","Idle");
		yield return null;
	}
	private IEnumerator StrawMan001()
	{
		Dialog("一只稻草人，它的手臂看起来似乎很结实。","StrawMan002");
		yield return null;
	}
	private IEnumerator StrawMan002()
	{
		Ask("是否将手臂拔下来？","是\n否","StrawMan003","Idle");
		yield return null;
	}
	private IEnumerator StrawMan003()
	{
		Flags.Instance.SetFlag("StrawManNoArm");
		CharacterItems.Instance.AddItem(6,1);
		Dialog("获得了" + Item.GetItemName(6) + "!" ,"Idle");
		Strawman.GetComponent<Trigger_StrawMan>().strawState = Trigger_StrawMan.StrawState.change;
		Strawman.GetComponent<AtlasManager>().SetAtlas(1,2,0,1);
		yield return null;
	}
	private IEnumerator StrawMan004()
	{
		Dialog("……","Idle");
		yield return null;
	}
	#endregion

}