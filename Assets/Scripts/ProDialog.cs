/// <summary>
/// 游戏中的对话框功能
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProDialog : GameMaster 
{
	public string text;
	public float delay = 0.07f;
	public Dictionary<int, string> Say;

	public Texture Arrow;

	private string _next;
	private int count;
	private int nowLine;
	private int nowChar;
	private int length;
	private string[] tempSay;
	private string currentSay;
	private bool clicked; 

	private int rollSpeed = 800;
	private int currentHeight;
	private int goalHeight = 160;

	public enum State
	{
		Set,
		Roll,
		Show,
		Wait,
		Done
	}
	public State state;

	public bool sfxActive;
	public void GetSFX()
	{
		if(_GameData.SFXActive == 0)
			sfxActive = false;
		else
			sfxActive = true;
	}
	
	void Start()
	{
		GetSFX();
	}
	
	public void Conversation(Dictionary<int, string> _say, string next)
	{
		_next = next;
		_GamePause.canPause = false;
		_GamePause.PauseGameSafe();
		Say = _say;
		nowLine = 0;
		state = State.Set;
		StartCoroutine("WaitForClick");
		StartCoroutine("Speak");
	}

	IEnumerator Speak()
	{
		while(state != State.Done)
		{
			switch(state)
			{
			case State.Set:
				currentHeight = 0;
				currentSay = "";
				count = Say.Count;
				tempSay = Say[nowLine].Split('|');

				if(tempSay.Length > 1)
					length = tempSay[1].Length;
				else
					length = tempSay[0].Length;

				state = State.Roll;
				break;

			case State.Roll:
				while(currentHeight < goalHeight)
				{
					if(clicked)
					{
						currentHeight = goalHeight;
						nowChar = length - 1;
						clicked = false;
					}
					currentHeight += (int)((float)rollSpeed * Time.deltaTime);
					yield return null;
				}
				state = State.Show;
				break;

			case State.Show:
				while(nowChar < length)
				{
					if(clicked)
					{
						nowChar = length - 1;
						clicked = false;
					}
					if(sfxActive)
					{
						gameObject.audio.pitch = Random.Range(0.5f,1.0f);
						gameObject.audio.Play();
					}
					if(tempSay.Length > 1)
						currentSay = tempSay[1].Remove(nowChar);
					else
						currentSay = tempSay[0].Remove(nowChar);
					nowChar ++;
					yield return new WaitForSeconds(delay);
				}
				if(tempSay.Length > 1)
					currentSay = tempSay[1];
				else
					currentSay = tempSay[0];

				nowChar = 0;
				nowLine++;
				state = State.Wait;
				break;

			case State.Wait:
				if(clicked)
				{
					if(nowLine < count)
					{
						state = State.Set;
					}
					else
					{
						state = State.Done;
					}
					clicked = false;
				}
				break;

			case State.Done:
				break;

			default:
				break;
			}
			yield return null;
		}
		_GamePause.ResumeGameSafe();
		_GamePause.canPause = true;

		_StoryBoard.Mission(_next);
		yield return null;
	}

	IEnumerator WaitForClick()
	{
		while(state != State.Done)
		{
			if(Input.GetButtonDown("Skip") || Input.GetButtonDown("Use") || Input.GetButtonDown("Jump"))
			{
				clicked = true;
			}
			yield return null;
		}
		yield return null;
	}
	
	void OnGUI()
	{
		if(state != State.Done && state != State.Set)
		{
			if(_GameData.Lang != 0)
				GUI.skin = _MainMenu.backgroundSkinCHDia;
			else
				GUI.skin = _MainMenu.backgroundSkinENDia;

			if(state == State.Roll)
			{
				GUI.Box(new Rect(40,Screen.height - 180, Screen.width - 80, Mathf.Clamp(currentHeight,20,goalHeight)),"");
			}
			if(state == State.Show || state == State.Wait)
			{
				GUI.Box(new Rect(40,Screen.height - 180, Screen.width - 80, goalHeight),"");
				GUI.Label(new Rect(60,Screen.height - 170,Screen.width - 110,140), currentSay);

			}

			if(state == State.Wait)
			{
				if((int)(Time.time*2)%2 == 0)
					GUI.DrawTexture(new Rect(Screen.width/2 - 16, Screen.height - 60,32,32), Arrow);
			}

			if(tempSay.Length > 1)
			{
				GUI.Box(new Rect(40,Screen.height - 222,tempSay[0].Length * 50,45),"");
				GUI.Label(new Rect(60,Screen.height - 221,Screen.width - 110,40), tempSay[0]);
			}
		}
	}
}