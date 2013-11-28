using UnityEngine;
using System.Collections;

public class Sound_StepSound : GameMaster
{
	public AudioClip soil;
	public AudioClip stone;
	public AudioClip wood;

	public enum SoundKind
	{
		soil,
		stone,
		wood
	}
	public SoundKind soundMaterial;

	void OnTriggerStay(Collider other)
	{
		Debug.Log("Stay");
		if(other.tag == "Player")
		{
			if(!_GamePause.paused &&_GameData.SFXActive == 1 && (Input.GetAxis("Vertical") > -0.5) && _CharacterController.isGrounded && (Input.GetAxis("Horizontal") != 0))
			{
				if(!audio.isPlaying)
				{
					switch(soundMaterial)
					{
					case SoundKind.soil:
						audio.clip = soil;
						break;
					case SoundKind.stone:
						audio.clip = stone;
						break;
					case SoundKind.wood:
						audio.clip = wood;
						break;
					default:
						audio.clip = soil;
						break;
					}
					audio.Play();
				}
			}
		}
	}
}
