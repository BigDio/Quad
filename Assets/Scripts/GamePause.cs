/// <summary>
/// 暂停功能
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class GamePause : GameMaster
{
	public bool paused;
	public bool canPause;

	void Awake () 
	{
		Application.runInBackground = true;
		paused = false;
	}
	
	void Start()
	{
		canPause = true;
	}
	public void OnlyPauseGame()
	{
		if(!paused)
		{
//			if(_BGM.bgmActive)
//				_BGM.gameObject.GetComponent<AudioSource>().Pause();
			_AnimateTexture.enabled = false;
			_AudioEffects.enabled = false;
			//_CharacterMovement.StopAllCoroutines();
			_CharacterMovement.enabled = false;
			_SmoothFollow2.enabled = false;
			_StoryBoard.StopAllCoroutines();
			_StoryBoard.enabled = false;
			paused = true;
		}
	}

	public void OnlyResumeGame()
	{
		if(paused)
		{
//			if(_BGM.bgmActive && !_BGM.gameObject.GetComponent<AudioSource>().isPlaying)
//				_BGM.gameObject.GetComponent<AudioSource>().Play();
			_AnimateTexture.enabled = true;
			_AnimateTexture.Setup();
			_AudioEffects.enabled = true;
			_CharacterMovement.enabled = true;
			//_CharacterMovement.Setup();
			_SmoothFollow2.enabled = true;
			_StoryBoard.enabled = true;
			_StoryBoard.Setup();
			paused = false;
		}
	}

	public void PauseGame()
	{
		if(!paused)
		{
			if(_BGM.bgmActive)
				_BGM.gameObject.GetComponent<AudioSource>().Pause();
			_AnimateTexture.enabled = false;
			_AudioEffects.enabled = false;
			//_CharacterMovement.StopAllCoroutines();
			_CharacterMovement.enabled = false;
			_SmoothFollow2.enabled = false;
			_StoryBoard.StopAllCoroutines();
			_StoryBoard.enabled = false;
			paused = true;
			_MainMenu.OpenMainMenu();
		}
	}
	
	public void ResumeGame()
	{
		if(paused)
		{
			if(_BGM.bgmActive && !_BGM.gameObject.GetComponent<AudioSource>().isPlaying)
				_BGM.gameObject.GetComponent<AudioSource>().Play();
			_AnimateTexture.enabled = true;
			_AnimateTexture.Setup();
			_AudioEffects.enabled = true;
			_CharacterMovement.enabled = true;
			//_CharacterMovement.Setup();
			_SmoothFollow2.enabled = true;
			_StoryBoard.enabled = true;
			_StoryBoard.Setup();
			paused = false;
			_MainMenu.CloseMainMenu();
		}
	}

	public void PauseGameSafe()
	{
		if(!paused)
		{
			_AnimateTexture.enabled = false;
			_AudioEffects.enabled = false;
			_CharacterMovement.StopAllCoroutines();
			_CharacterMovement.enabled = false;
			_SmoothFollow2.enabled = false;
			_StoryBoard.enabled = false;
			paused = true;
		}
	}
	
	public void ResumeGameSafe()
	{
		if(paused)
		{
			_AnimateTexture.enabled = true;
			_AnimateTexture.Setup();
			_AudioEffects.enabled = true;
			_CharacterMovement.enabled = true;
			_CharacterMovement.Setup();
			_SmoothFollow2.enabled = true;
			_StoryBoard.enabled = true;
			_StoryBoard.Setup();
			paused = false;
		}
	}
}