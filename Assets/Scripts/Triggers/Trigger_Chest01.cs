using UnityEngine;
using System.Collections;

public class Trigger_Chest01 : GameMaster
{
	public enum ChestState
	{
		idle,
		wait,
		empty
	}
	public ChestState state;

	private bool stay;

	void Start()
	{
		CheckState();
	}

	void CheckState()
	{
		if(Flags.Instance.HasFlag("Chest01"))
		{
			state = ChestState.empty;
			gameObject.GetComponent<AtlasManager>().SetAtlas(16,16,0,1);
		}
		else if(CharacterItems.Instance.CarryItem.ContainsKey(0))
		{
			state = ChestState.wait;
			gameObject.GetComponent<AtlasManager>().SetAtlas(16,16,0,0);
		}
		else
		{
			state = ChestState.idle;
			gameObject.GetComponent<AtlasManager>().SetAtlas(16,16,0,0);
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

	IEnumerator GOO()
	{
		while(stay)
		{
			if(Input.GetButtonDown("Exam"))
			{
				if(!_GamePause.paused)
				{
					switch(state)
					{
					case ChestState.idle:
						_StoryBoard.Mission ("Chest001_01");
						break;
					case ChestState.wait:
						_StoryBoard.Mission ("Chest001_01");
						break;
					case ChestState.empty:
						_StoryBoard.Mission ("Chest001_05");
						break;
					}
				}
			}
			yield return null;
		}
		yield return null;
	}
}
