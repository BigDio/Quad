/// <summary>
/// 游戏中出现的选择框
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionBox : GameMaster
{
	private int count;
	private int nowLine;
	private bool showing;
	private bool choosed;
	private string[] temp;
	private string _next1;
	private string _next2;
	private string _next3;
	private string _next4;
	private string question;
	
	public int xPos = 10;
	public int yPos = 10;
	public int length = 100;
	
	public GUISkin backgroundSkin;
	public GUISkin foregroundSkin;
	public string selections;
	public State state;
	public enum State
	{
		Set,
		Show,
		Done
	}

	void Start () 
	{
		state = State.Done;
	}

	public void SendOutResult()
	{
//		if(_next1 != "Idle")
//		{
//			if(_next2 == "Idle")
//			{
//				_next2 = _next1;
//			}
//			if(_next3 == "Idle")
//			{
//				_next3 = _next1;
//			}
//			if(_next4 == "Idle")
//			{
//				_next4 = _next1;
//			}
//		}

		switch(nowLine)
		{
		case 0:
			_StoryBoard.Mission(_next1);
			break;
		case 1:
			_StoryBoard.Mission(_next2);
			break;
		case 2:
			_StoryBoard.Mission(_next3);
			break;
		case 3:
			_StoryBoard.Mission(_next4);
			break;
		default:
			_StoryBoard.Mission("Idle");
			break;
		}
		_next1 = "Idle";
		_next2 = "Idle";
		_next3 = "Idle";
		_next4 = "Idle";
	}
	
	public void ShowSelectionBox(string qst, string slt, string next1)
	{
		_GUISFX.PlayQuestionSound();
		question = qst;
		_next1 = next1;
		_next2 = "Idle";
		_next3 = "Idle";
		_next4 = "Idle";
		selections = slt;
		_GamePause.canPause = false;
		_GamePause.PauseGameSafe();
		StartCoroutine("ShowBox");
	}

	public void ShowSelectionBox(string qst, string slt, string next1, string next2)
	{
		_GUISFX.PlayQuestionSound();
		question = qst;
		_next1 = next1;
		_next2 = next2;
		_next3 = "Idle";
		_next4 = "Idle";
		selections = slt;
		_GamePause.canPause = false;
		_GamePause.PauseGameSafe();
		StartCoroutine("ShowBox");
	}

	public void ShowSelectionBox(string qst, string slt, string next1, string next2, string next3)
	{
		_GUISFX.PlayQuestionSound();
		question = qst;
		_next1 = next1;
		_next2 = next2;
		_next3 = next3;
		_next4 = "Idle";
		selections = slt;
		_GamePause.canPause = false;
		_GamePause.PauseGameSafe();
		StartCoroutine("ShowBox");
	}

	public void ShowSelectionBox(string qst, string slt, string next1, string next2, string next3, string next4)
	{
		_GUISFX.PlayQuestionSound();
		question = qst;
		_next1 = next1;
		_next2 = next2;
		_next3 = next3;
		_next4 = next4;
		selections = slt;
		_GamePause.canPause = false;
		_GamePause.PauseGameSafe();
		StartCoroutine("ShowBox");
	}
	
	IEnumerator ShowBox()
	{
		state = State.Set;
		while(state != State.Done)
		{
			switch(state)
			{
			case State.Set:
				choosed = false;
				nowLine = 0;
				temp = selections.Split('\n');
				count = temp.Length;
				state = State.Show;
				break;
			case State.Show:
				PlayerChoose();
				if(choosed)
				{
					state = State.Done;
				}
				break;
			case State.Done:
				break;
			default:
				break;
			}
			yield return null;
		}
		_GamePause.canPause = true;
		_GamePause.ResumeGameSafe();

		SendOutResult();
		yield return null;
	}
	void PlayerChoose()
	{
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			_GUISFX.PlaySelectSound();
			nowLine ++;
			if(nowLine > count - 1)
				nowLine = 0;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			_GUISFX.PlaySelectSound();
			nowLine --;
			if(nowLine < 0)
				nowLine = count - 1;
		}
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Use"))
		{
			_GUISFX.PlayPickSound();
			choosed = true;
			state = State.Done;
		}
	}

	void OnGUI()
	{
		if(state == State.Show)
		{
			if(_GameData.Lang != 0)
				GUI.skin = _MainMenu.backgroundSkinCHDia;
			else
				GUI.skin = _MainMenu.backgroundSkinENDia;
			GUI.Box(new Rect(40,Screen.height- 128 - count*34,length,count*34 + 8),"");
			GUI.Box(new Rect(40,Screen.height - 120, Screen.width - 80.0f, 100),"");
			GUI.Label(new Rect(50,Screen.height - 110,Screen.width - 100,80), question);
			
			
			if(_GameData.Lang != 0)
				GUI.skin = _MainMenu.foregroundSkinCHDia;
			else
				GUI.skin = _MainMenu.foregroundSkinENDia;
			GUI.Box(new Rect(48,Screen.height-128 - count*34 + 4 + Mathf.Abs (nowLine*31),length - 17,33),"");
			GUI.Label(new Rect(55,Screen.height-128 - count*34,length, count*33), selections);
		}
	}
}