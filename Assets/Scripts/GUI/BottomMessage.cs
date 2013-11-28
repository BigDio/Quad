using UnityEngine;
using System.Collections;

public class BottomMessage : MonoBehaviour 
{
	private string showText;
	private float showTime = 3.0f;
	private float showSpeed = 10.0f;
	private float fadeSpeed = 0.5f;
	public Color color = Color.white;
	private int size = 26;

	private float goalTime;
	private enum ShowState
	{
		setup,
		fadeIn,
		show,
		fadeOut,
		done
	}
	private ShowState nowState;

	public void Send(string _showText)
	{
		showText = _showText;
		StartCoroutine("Run");
	}

	IEnumerator Run()
	{	
		nowState = ShowState.setup;
		while(true)
		{
			switch(nowState)
			{
			case ShowState.setup:
				guiText.fontSize = size;
				guiText.text = showText;
				guiText.color = new Color(color.r,color.g,color.b,0.0f);
				nowState = ShowState.fadeIn;
				break;
				
			case ShowState.fadeIn:
				guiText.color += new Color(0.0f,0.0f,0.0f, showSpeed * Time.deltaTime);
				if(guiText.color.a >= 1.0f)
				{
					guiText.color = color;
					goalTime = Time.time + showTime;
					nowState = ShowState.show;
				}
				break;
				
			case ShowState.show:
				guiText.color = color;
				if(goalTime <= Time.time)
				{
					nowState = ShowState.fadeOut;
				}
				break;
				
			case ShowState.fadeOut:
				guiText.color -= new Color(0.0f,0.0f,0.0f, fadeSpeed * Time.deltaTime);
				if(guiText.color.a <= 0.0f)
				{
					guiText.color = new Color(color.r,color.g,color.b,0.0f);
					nowState = ShowState.done;
				}
				break;
				
			case ShowState.done:
				Destroy (this.gameObject);
				break;
				
			default:
				break;
			}
			yield return null;
		}
	}
}
