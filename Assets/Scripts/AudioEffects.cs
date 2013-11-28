/// <summary>
/// 角色动作音效
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class AudioEffects : GameMaster 
{
	public AudioClip jumpSound;
	public AudioClip stepSound;
	public AudioClip collideSound;
	public AudioClip injureSound;
	public AudioClip dieSound;

	private bool sfxActive;
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

	public void PlayJumpSound()
	{
		if(sfxActive)
		{
			audio.clip = jumpSound;
			audio.pitch = 1.0f;
			audio.Play();
		}
	}

	public void PlayDoubleJumpSound()
	{
		if(sfxActive)
		{
			audio.clip = jumpSound;
			audio.pitch = 1.2f;
			audio.Play();
		}
	}

	public void PlayStepSound()
	{
		if(sfxActive)
		{
			audio.clip = stepSound;
			audio.pitch = 0.6f;
			if(!audio.isPlaying)
				audio.Play();
		}
	}

	public void PlayCollideSound()
	{
		if(sfxActive)
		{
			audio.clip = collideSound;
			audio.pitch = 1.0f;
			audio.Play();
		}
	}
	public void PlayDieSound()
	{
		if(sfxActive)
		{
			audio.clip = dieSound;
			audio.pitch = 1.0f;
			audio.Play();
		}
	}
}
