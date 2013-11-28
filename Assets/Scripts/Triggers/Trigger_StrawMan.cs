/// <summary>
/// 路牌的触发机制
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class Trigger_StrawMan : GameMaster 
{
	public float distance = 10.0f;
	public float maxDistance = 24.0f;

	public StrawState strawState;
	public enum StrawState
	{
		diable,
		idle,
		change
	}

	private bool stay;

	void Start()
	{
		CheckState();
	}
	
	void Update()
	{
		if(strawState == StrawState.change)
		{
			if(Player.transform.position.x > transform.position.x + distance)
			{
				transform.position = new Vector3(Mathf.Min(Camera.main.transform.position.x - distance, maxDistance), transform.position.y, transform.position.z);
			}
			if(Player.transform.position.x < transform.position.x - distance)
			{
				transform.position = new Vector3(Camera.main.transform.position.x + distance, transform.position.y, transform.position.z);
			}
		}
	}

	
	IEnumerator GOO()
	{
		while(stay)
		{
			if(Input.GetButtonDown("Exam"))
			{
				if(!_GamePause.paused)
				{
					switch(strawState)
					{
					case StrawState.diable:
						_StoryBoard.Mission ("StrawMan000");
						break;
					case StrawState.idle:
						_StoryBoard.Mission ("StrawMan001");
						break;
					case StrawState.change:
						_StoryBoard.Mission ("StrawMan004");
						break;
					}
				}
			}
			yield return null;
		}
		yield return null;
	}

	void CheckState()
	{
		if(Flags.Instance.HasFlag("StrawManNoArm"))
		{
			gameObject.GetComponent<AtlasManager>().SetAtlas(1,2,0,1);
			strawState = StrawState.change;
		}
		else if(Flags.Instance.HasFlag("FirstRiddle"))
		{
			strawState = StrawState.idle;
		}
		else
		{
			strawState = StrawState.diable;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			stay = true;
			CheckState();
		}
		StartCoroutine("GOO");
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			stay = false;
			CheckState();
		}
		StopCoroutine("GOO");
	}
}
