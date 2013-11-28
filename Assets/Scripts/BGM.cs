/// <summary>
/// 游戏BGM控制
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class BGM : GameMaster 
{
	public bool bgmActive;
	public void GetBGM()
	{
		if(_GameData.BGMActive == 0)
			bgmActive = false;
		else
			bgmActive = true;

		if(bgmActive)
			gameObject.audio.Play();
		else
			gameObject.audio.Pause();
	}

	void Start () 
	{
		GetBGM();
	}

}
