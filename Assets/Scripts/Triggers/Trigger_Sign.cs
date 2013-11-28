/// <summary>
/// 路牌的触发机制
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class Trigger_Sign : StoryBoard 
{
	public enum Signs
	{
		sign001,
		sign002
	}
	public Signs signIndex;

	private bool stay;
	IEnumerator GOO()
	{
		while(stay)
		{
			if(Input.GetButtonDown("Exam"))
			{
				if(!_GamePause.paused)
				{
					switch(signIndex)
					{
					case Signs.sign001:
						Mission ("Sign001");
						break;
					case Signs.sign002:
						Mission ("Sign002");
						break;
					default:
						break;
					}
				}
			}
			yield return null;
		}
		yield return null;
	}
	
	void OnTriggerEnter(Collider other)
	{
		_PlayerHint.ShowUpHint();
		if(other.tag == "Player")
			stay = true;
		StartCoroutine("GOO");
	}
	
	void OnTriggerExit(Collider other)
	{
		_PlayerHint.HideUpHint();
		if(other.tag == "Player")
			stay = false;
		StopCoroutine("GOO");
	}
}
