/// <summary>
/// 故事版核心模块
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryBoardEngine : GameMaster 
{

	#region 访问接口
	private Dictionary<int, string> WannaSay = new Dictionary<int, string>();
	
	public void Dialog(string dialog)
	{
		WannaSay.Clear();
		WannaSay.Add(0,dialog);
		_ProDialog.Conversation(WannaSay, "Idle");
	}
	
	public void Dialog(string dialog, string nextMission)
	{
		if(_ProDialog.state == ProDialog.State.Set || _ProDialog.state == ProDialog.State.Done)
		{
			WannaSay.Clear();
			WannaSay.Add(0,dialog);
			_ProDialog.Conversation(WannaSay, nextMission);
		}
	}
	
	public void Dialog(string[] dialogs)
	{
		int cnt = 0;
		WannaSay.Clear();
		foreach(string dialog in dialogs)
		{
			WannaSay.Add(cnt,dialog);
			cnt++;
		}
		_ProDialog.Conversation(WannaSay, "Idle");
	}
	
	public void Dialog(string[] dialogs, string nextMission)
	{
		int cnt = 0;
		WannaSay.Clear();
		foreach(string dialog in dialogs)
		{
			WannaSay.Add(cnt,dialog);
			cnt++;
		}
		_ProDialog.Conversation(WannaSay, nextMission);
	}
	public void Ask(string question, string answers)
	{
		_SelectionBox.ShowSelectionBox(question, answers, "Idle");
	}
	public void Ask(string question, string answers, string choice1)
	{
		_SelectionBox.ShowSelectionBox(question, answers, choice1);
	}
	public void Ask(string question, string answers, string choice1, string choice2)
	{
		_SelectionBox.ShowSelectionBox(question, answers, choice1, choice2);
	}
	public void Ask(string question, string answers, string choice1, string choice2, string choice3)
	{
		_SelectionBox.ShowSelectionBox(question, answers, choice1, choice2, choice3);
	}
	public void Ask(string question, string answers, string choice1, string choice2, string choice3, string choice4)
	{
		_SelectionBox.ShowSelectionBox(question, answers, choice1, choice2, choice3, choice4);
	}
	#endregion 

	private string goon = "Idle";

	private State state;
	public enum State
	{
		run,
		go,
		idle
	}
	public void Setup()
	{
		StartCoroutine("Run");
	}
	
	void Start()
	{
		goon = "Idle";
		state = State.idle;
		StartCoroutine("Run");
	}
	
	public void Mission(string go)
	{
		goon = go;
	}
	
	IEnumerator Run () 
	{
		while(true)
		{
			switch(state)
			{
			case State.run:
				Goon();
				state = State.go;
				break;
				
			case State.go:
				goon = "Idle";
				state = State.idle;
				break;
				
			case State.idle:
				if(goon != "Idle")
					state = State.run;
				break;
				
			default :
				goon = "Idle";
				state = State.idle;
				break;
			}
			yield return null;
		}
	}
	
	void Goon()
	{
		if(goon == "Idle")
		{
			state = State.idle;
		}
		else
		{
			StartCoroutine(goon);
			state = State.idle;
		}
	}
}
