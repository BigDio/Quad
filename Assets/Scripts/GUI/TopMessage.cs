using UnityEngine;
using System.Collections;

public class TopMessage : MonoBehaviour 
{
	private string _showText;
	private float _showTime;
	private float _showSpeed;
	private float _fadeSpeed;
	private Color _color;
	private int _size;

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

	public void Setup(string showText,int size, float showTime, float showSpeed, float fadeSpeed, Color color)
	{
		_size = size;
		_showText = showText;
		_showTime = showTime;
		_showSpeed = showSpeed;
		_fadeSpeed = fadeSpeed;
		_color = color;
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
				guiText.fontSize = _size;
				guiText.text = _showText;
				guiText.color = new Color(_color.r,_color.g,_color.b,0.0f);
				nowState = ShowState.fadeIn;
				break;

			case ShowState.fadeIn:
				guiText.color += new Color(0.0f,0.0f,0.0f, _showSpeed * Time.deltaTime);
				if(guiText.color.a >= 1.0f)
				{
					guiText.color = _color;
					goalTime = Time.time + _showTime;
					nowState = ShowState.show;
				}
				break;

			case ShowState.show:
				guiText.color = _color;
				if(goalTime <= Time.time)
				{
					nowState = ShowState.fadeOut;
				}
				break;

			case ShowState.fadeOut:
				guiText.color -= new Color(0.0f,0.0f,0.0f, _fadeSpeed * Time.deltaTime);
				if(guiText.color.a <= 0.0f)
				{
					guiText.color = new Color(_color.r,_color.g,_color.b,0.0f);
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
