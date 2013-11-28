/// <summary>
/// 对话框中的小箭头
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class GUI_DownArrow : GameMaster
{
	void Awake()
	{

	}

	void Update ()
	{
		if(_ProDialog.state == ProDialog.State.Wait)
		{
			gameObject.GetComponent<GUITexture>().enabled = true;
			gameObject.GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2,60,1,1);
		}
		else
		{
			gameObject.GetComponent<GUITexture>().enabled = false;
		}
	}


}
