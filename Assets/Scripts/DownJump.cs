using UnityEngine;
using System.Collections;

public class DownJump : GameMaster 
{
	public MeshCollider step;
	private bool stay;

	IEnumerator GOO()
	{
		while(stay)
		{
			if(Input.GetButtonDown("Jump") && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
			{
				if(_CharacterController.isGrounded)
				{
					_AudioEffects.PlayJumpSound();
					JumpToDown();
				}
			}
			yield return null;
		}
		yield return null;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			stay = true;
		StartCoroutine("GOO");
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
			stay = false;
		StopCoroutine("GOO");
	}

	void JumpToDown()
	{
		step.enabled = false;
	}
}