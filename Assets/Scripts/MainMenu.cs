/// <summary>
/// 主菜单方法
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class MainMenu : GameMaster
{
	//层级说明:
	//nowLayer 0:主菜单
	//nowLayer 1:退出确认窗口
	//nowLayer 2:游戏选项窗口
	//nowLayer 3:重新开始确认窗口
	//nowLayer 4:视频选项窗口
	//nowLayer 5:音频选项窗口
	#region 主菜单变量
	public GUISkin foregroundSkinCH;
	public GUISkin backgroundSkinCH;
	public GUISkin foregroundSkinEN;
	public GUISkin backgroundSkinEN;
	public GUISkin foregroundSkinCHDia;
	public GUISkin backgroundSkinCHDia;
	public GUISkin foregroundSkinENDia;
	public GUISkin backgroundSkinENDia;

	public bool showMenu;
	public bool inMenus;

	private int mainMenuWidth = 260;
	private int mainMenuHeight = 200;
	private int globleOffset = 34;
	private int textOffset = 32;

	private int mainMenuNowLine;
	private int nowLayer;
	private int mainMenuSelectionCount;
	private Rect mainMenuRect;
	private Rect mainMenuTitleRect;
	#endregion
	#region 游戏选项变量
	public bool showGameSettings;
	
	private Rect gameSettingsRect;
	private Rect gameSettingsTitleRect;
	private int gameSettingsMenuNowLine;
	private int gameSettingsMenuSelectionCount;
	
	private int gameSettingsMenuWidth = 360;
	private int gameSettingsMenuHeight = 200;
	private int gameSettingsGlobleOffset = 19;
	private int gameSettingsTextOffset = 32;
	#endregion
	#region 退出确认窗口变量
	private bool showQuitDiaBox;
	private Rect quitDiaBoxRect;
	private Rect quitDiaBoxTitleRect;
	private int quitDiaBoxNowLine;
	private int quitDiaBoxSelectionCount;
	
	private int quitDiaBoxWidth = 400;
	private int quitDiaBoxHeight = 140;
	private int quitDiaBoxGlobleOffset = 36;
	private int quitDiaBoxTextOffset = 32;
	#endregion
	#region 重新开始确认窗口变量

	private bool showRestartDiaBox;
	private Rect restartDiaBoxRect;
	private Rect restartDiaBoxTitleRect;
	private int restartDiaBoxNowLine;
	private int restartDiaBoxSelectionCount;
	
	private int restartDiaBoxWidth = 400;
	private int restartDiaBoxHeight = 140;
	private int restartDiaBoxGlobleOffset = 36;
	private int restartDiaBoxTextOffset = 32;

	#endregion
	#region 视频选项变量
	public bool showGraphicSettings;
	
	private Rect graphicSettingsRect;
	private Rect graphicSettingsTitleRect;
	private int graphicSettingsMenuNowLine;
	private int graphicSettingsMenuSelectionCount;
	
	private int graphicSettingsMenuWidth = 360;
	private int graphicSettingsMenuHeight = 360;
	private int graphicSettingsGlobleOffset = 18;
	private int graphicSettingsTextOffset = 96;

	public int resolutionX;
	public int resolutionY;

	private int resolutionPick;
	private int aePick;
	private string resolutionString;
	private string aeString;

	public int displayLang;
	private string displayLangString;
	#endregion
	#region 音频选项变量
	public bool showAudioSettings;
	
	private Rect audioSettingsRect;
	private Rect audioSettingsTitleRect;
	private int audioSettingsMenuNowLine;
	private int audioSettingsMenuSelectionCount;
	
	public int audioSettingsMenuWidth = 360;
	public int audioSettingsMenuHeight = 260;
	public int audioSettingsGlobleOffset = 18;
	public int audioSettingsTextOffset = 96;

	private int sfxActive;
	private int bgmActive;

	private string SFXSwitchString;
	private string BGMSwitchString;
	#endregion

	#region 初始化变量

	void Start()
	{
		inMenus = false;
		GameText.RefreshLanguage();
		mainMenuSelectionCount = 4;
		gameSettingsMenuSelectionCount = 5;
		quitDiaBoxSelectionCount = 2;
		restartDiaBoxSelectionCount = 2;
		nowLayer = 0;

		resolutionX = _GameData.ResolutionX;
		resolutionY = _GameData.ResolutionY;
		displayLang = _GameData.Lang;
		graphicSettingsMenuSelectionCount = 4;

		sfxActive = _GameData.SFXActive;
		bgmActive = _GameData.BGMActive;
		audioSettingsMenuSelectionCount = 3;
	}

	#endregion 

	//变量设置

	#region 菜单操作判定

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!showMenu && !_GamePause.paused)
			{
				_GamePause.PauseGame();
				inMenus = true;
				return;
			}
			else if(nowLayer == 0 && showMenu)
			{
				_GamePause.ResumeGame();
				inMenus = false;
				return;
			}
		}

		#region 判断玩家是否按下了ESC键
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Use"))
		{
			if(inMenus)
			{
				if(nowLayer == 0 && showMenu)
				{
					_GamePause.ResumeGame();
				}
				else if(nowLayer == 1)
				{
					HideQuitDiaBox();
					BackToMainMenu();
					nowLayer = 0;
				}
				else if(nowLayer == 2)
				{
					HideGameSettings();
					BackToMainMenu();
					nowLayer = 0;
				}
				else if(nowLayer == 3)
				{
					HideRestartDiaBox();
					BackToMainMenu();
					nowLayer = 0;
				}
				else if(nowLayer == 4)
				{
					_GUISFX.PlayQuitMenuSound();
					HideGraphicSettings();
					nowLayer = 2;
					showMenu = false;
					ShowGameSettings();
				}
				else if(nowLayer == 5)
				{
					_GUISFX.PlayQuitMenuSound();
					HideAudioSettings();
					nowLayer = 2;
					showMenu = false;
					ShowGameSettings();
				}
			}
		}

		#endregion

		#region 判断玩家是否在菜单模式中按下了功能键

		if(showMenu && nowLayer == 0)
		{
			if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				PlayerPressedUp(0);
			}
			if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				PlayerPressedDown(0);
			}
			if(Input.GetButtonDown("Jump"))
			{
				PlayerPressedEnter(0);
			}
		}
		else if(nowLayer != 0)
		{
			if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				PlayerPressedUp(nowLayer);
			}
			if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				PlayerPressedDown(nowLayer);
			}
			if(Input.GetButtonDown("Jump"))
			{
				PlayerPressedEnter(nowLayer);
			}
		}

		if(nowLayer == 4 || nowLayer == 5)
		{
			if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				PlayerPressedLeft(nowLayer);
			}
			if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				PlayerPressedRight(nowLayer);
			}
		}

		#endregion 
	}

	#endregion 
	#region 玩家按下了方向键

	public void PlayerPressedUp(int layer)
	{
		switch(layer)
		{
		case 0:
			_GUISFX.PlaySelectSound();
			mainMenuNowLine--;
			if(mainMenuNowLine < 0)
			{
				mainMenuNowLine = mainMenuSelectionCount - 1;
			}
			break;
		case 1:
			_GUISFX.PlaySelectSound();
			quitDiaBoxNowLine--;
			if(quitDiaBoxNowLine < 0)
			{
				quitDiaBoxNowLine = quitDiaBoxSelectionCount - 1;
			}
			break;
		case 2:
			_GUISFX.PlaySelectSound();
			gameSettingsMenuNowLine--;
			if(gameSettingsMenuNowLine < 0)
			{
				gameSettingsMenuNowLine = gameSettingsMenuSelectionCount - 1;
			}
			break;
		case 3:
			_GUISFX.PlaySelectSound();
			restartDiaBoxNowLine--;
			if(restartDiaBoxNowLine < 0)
			{
				restartDiaBoxNowLine = restartDiaBoxSelectionCount - 1;
			}
			break;
		case 4:
			_GUISFX.PlaySelectSound();
			graphicSettingsMenuNowLine--;
			if(graphicSettingsMenuNowLine < 0)
			{
				graphicSettingsMenuNowLine = graphicSettingsMenuSelectionCount - 1;
			}
			break;
		case 5:
			_GUISFX.PlaySelectSound();
			audioSettingsMenuNowLine--;
			if(audioSettingsMenuNowLine < 0)
			{
				audioSettingsMenuNowLine = audioSettingsMenuSelectionCount - 1;
			}
			break;
		}
	}

	public void PlayerPressedDown(int layer)
	{
		switch(layer)
		{
		case 0:
			_GUISFX.PlaySelectSound();
			mainMenuNowLine++;
			if(mainMenuNowLine > mainMenuSelectionCount - 1)
			{
				mainMenuNowLine = 0;
			}
			break;
		case 1:
			_GUISFX.PlaySelectSound();
			quitDiaBoxNowLine++;
			if(quitDiaBoxNowLine > quitDiaBoxSelectionCount - 1)
			{
				quitDiaBoxNowLine = 0;
			}
			break;
		case 2:
			_GUISFX.PlaySelectSound();
			gameSettingsMenuNowLine++;
			if(gameSettingsMenuNowLine > gameSettingsMenuSelectionCount - 1)
			{
				gameSettingsMenuNowLine = 0;
			}
			break;
		case 3:
			_GUISFX.PlaySelectSound();
			restartDiaBoxNowLine++;
			if(restartDiaBoxNowLine > restartDiaBoxSelectionCount - 1)
			{
				restartDiaBoxNowLine = 0;
			}
			break;
		case 4:
			_GUISFX.PlaySelectSound();
			graphicSettingsMenuNowLine++;
			if(graphicSettingsMenuNowLine > graphicSettingsMenuSelectionCount - 1)
			{
				graphicSettingsMenuNowLine = 0;
			}
			break;
		case 5:
			_GUISFX.PlaySelectSound();
			audioSettingsMenuNowLine++;
			if(audioSettingsMenuNowLine > audioSettingsMenuSelectionCount - 1)
			{
				audioSettingsMenuNowLine = 0;
			}
			break;
		}
	}

	public void PlayerPressedRight(int layer)
	{
		switch(layer)
		{
		case 4:
			switch(graphicSettingsMenuNowLine)
			{
			case 0:
				_GUISFX.PlaySelectSound();
				resolutionPick ++;
				if(resolutionPick>1)
					resolutionPick = 0;
				SetupGraphicSettings();
				break;
			case 1:
				_GUISFX.PlaySelectSound();
				aePick ++;
				if(aePick>1)
					aePick = 0;
				SetupAE(aePick);
				SetupGraphicSettings();
				break;
			case 2:
				_GUISFX.PlaySelectSound();
				displayLang ++;
				if(displayLang>2)
					displayLang = 0;
				SetupGraphicSettings();
				break;
			}
			break;
		case 5:
			switch(audioSettingsMenuNowLine)
			{
			case 0:
				_GUISFX.PlaySelectSound();
				sfxActive ++;
				if(sfxActive>1)
					sfxActive = 0;
				SetupAudioSettings();
				SetupSFX(sfxActive);
				Debug.Log(sfxActive);
				break;
			case 1:
				_GUISFX.PlaySelectSound();
				bgmActive ++;
				if(bgmActive>1)
					bgmActive = 0;
				SetupAudioSettings();
				SetupBGM(bgmActive);
				break;
			}
			break;
		}
	}

	public void PlayerPressedLeft(int layer)
	{
		switch(layer)
		{
		case 4:
			switch(graphicSettingsMenuNowLine)
			{
			case 0:
				_GUISFX.PlaySelectSound();
				resolutionPick --;
				if(resolutionPick<0)
					resolutionPick = 1;
				SetupGraphicSettings();
				break;
			case 1:
				_GUISFX.PlaySelectSound();
				aePick --;
				if(aePick<0)
					aePick = 1;
				SetupAE(aePick);
				SetupGraphicSettings();
				break;
			case 2:
				_GUISFX.PlaySelectSound();
				displayLang --;
				if(displayLang<0)
					displayLang = 2;
				SetupGraphicSettings();
				break;
			}
			break;
		case 5:
			switch(audioSettingsMenuNowLine)
			{
			case 0:
				_GUISFX.PlaySelectSound();
				sfxActive --;
				if(sfxActive<0)
					sfxActive = 1;
				SetupAudioSettings();
				SetupSFX(sfxActive);
				Debug.Log(sfxActive);
				break;
			case 1:
				_GUISFX.PlaySelectSound();
				bgmActive --;
				if(bgmActive<0)
					bgmActive = 1;
				SetupAudioSettings();
				SetupBGM(bgmActive);
				break;
			}
			break;
		}
	}

	#endregion 
	#region 玩家按下了Enter键

	public void PlayerPressedEnter(int layer)
	{
		switch(layer)
		{
		case 0:

			if(mainMenuNowLine == 0)
			{
				_GUISFX.PlayPickSound();
				_GamePause.ResumeGame();
			}
			else if(mainMenuNowLine == 1)
			{
				_GUISFX.PlayPickSound();
				nowLayer = 3;
				showMenu = false;
				ShowRestartDiaBox();
			}
			else if(mainMenuNowLine == 2)
			{
				_GUISFX.PlayPickSound();
				nowLayer = 2;
				showMenu = false;
				ShowGameSettings();
			}
			else if(mainMenuNowLine == 3)
			{
				_GUISFX.PlayPickSound();
				nowLayer = 1;
				showMenu = false;
				ShowQuitDiaBox();
			}
			break;

		case 1:
			if(quitDiaBoxNowLine == 0)
			{
				_GUISFX.PlayPickSound();
				QuitGame();
			}
			else
			{
				HideQuitDiaBox();
				BackToMainMenu();
				nowLayer = 0;
			}
			break;

		case 2:
			if(gameSettingsMenuNowLine == 0)
			{
				_GUISFX.PlayPickSound();
				HideGameSettings();
				ShowGraphicSettings();
				nowLayer = 4;
			}
			if(gameSettingsMenuNowLine == 1)
			{
				_GUISFX.PlayPickSound();
				HideGameSettings();
				ShowAudioSettings();
				nowLayer = 5;
			}
			else if(gameSettingsMenuNowLine == 4)
			{
				HideGameSettings();
				BackToMainMenu();
				nowLayer = 0;
			}
			break;

		case 3:
			if(restartDiaBoxNowLine == 0)
			{
				RestartGameFromLastCheckPoint();
			}
			else
			{
				HideRestartDiaBox();
				BackToMainMenu();
				nowLayer = 0;
			}
			break;

		case 4:
			if(graphicSettingsMenuNowLine == 0)
			{
				_GUISFX.PlayPickSound();
				SetupResolution(resolutionPick);
				ResetGUI();
			}
			else if(graphicSettingsMenuNowLine == 1)
			{
				_GUISFX.PlayPickSound();
				SetupAE(aePick);
			}
			else if(graphicSettingsMenuNowLine == 2)
			{
				_GUISFX.PlayPickSound();
				SetupLanguage(displayLang);
			}
			else
			{
				_GUISFX.PlayQuitMenuSound();
				HideGraphicSettings();
				nowLayer = 2;
				showMenu = false;
				ShowGameSettings();
			}
			break;
		case 5:
			if(audioSettingsMenuNowLine == 0)
			{
				_GUISFX.PlayPickSound();
				SetupSFX(sfxActive);
			}
			else if(audioSettingsMenuNowLine == 1)
			{
				_GUISFX.PlayPickSound();
				SetupBGM(bgmActive);
			}
			else
			{
				_GUISFX.PlayQuitMenuSound();
				HideAudioSettings();
				nowLayer = 2;
				showMenu = false;
				ShowGameSettings();
			}
			break;
		}
	}

	#endregion

	//玩家操作

	#region 额外功能
	public void ResetGUI()
	{
		mainMenuTitleRect = new Rect(_GameData.ResolutionX/2 - (float)mainMenuWidth/2, _GameData.ResolutionY/2 - (float)mainMenuHeight/2 - 50, mainMenuWidth, 50);
		mainMenuRect = new Rect(_GameData.ResolutionX/2 - (float)mainMenuWidth/2, _GameData.ResolutionY/2 - (float)mainMenuHeight/2, mainMenuWidth, mainMenuHeight);
		
		gameSettingsRect = new Rect(_GameData.ResolutionX/2 - (float)gameSettingsMenuWidth/2, 
		                            _GameData.ResolutionY/2 - (float)gameSettingsMenuHeight/2, 
		                            gameSettingsMenuWidth, 
		                            gameSettingsMenuHeight);
		gameSettingsTitleRect = new Rect(_GameData.ResolutionX/2 - (float)gameSettingsMenuWidth/2, 
		                                 _GameData.ResolutionY/2 - (float)gameSettingsMenuHeight/2 - 50, 
		                                 gameSettingsMenuWidth, 
		                                 50);
		
		quitDiaBoxRect = new Rect(_GameData.ResolutionX/2 - (float)quitDiaBoxWidth/2, 
		                          _GameData.ResolutionY/2 - (float)quitDiaBoxHeight/2, 
		                          quitDiaBoxWidth, 
		                          quitDiaBoxHeight);
		quitDiaBoxTitleRect = new Rect(_GameData.ResolutionX/2 - (float)quitDiaBoxWidth/2 -100, 
		                               _GameData.ResolutionY/2 - (float)quitDiaBoxHeight/2 - 100, 
		                               quitDiaBoxWidth + 200, 
		                               100);
		
		restartDiaBoxRect = new Rect(_GameData.ResolutionX/2 - (float)restartDiaBoxWidth/2, 
		                             _GameData.ResolutionY/2 - (float)restartDiaBoxHeight/2, 
		                             restartDiaBoxWidth, 
		                             restartDiaBoxHeight);
		restartDiaBoxTitleRect = new Rect(_GameData.ResolutionX/2 - (float)restartDiaBoxWidth/2 - 100, 
		                                  _GameData.ResolutionY/2 - (float)restartDiaBoxHeight/2 - 100, 
		                                  restartDiaBoxWidth + 200, 
		                                  100);
		
		graphicSettingsRect = new Rect(_GameData.ResolutionX/2 - (float)graphicSettingsMenuWidth/2, 
		                               _GameData.ResolutionY/2 - (float)graphicSettingsMenuHeight/2, 
		                               graphicSettingsMenuWidth, 
		                               graphicSettingsMenuHeight);
		graphicSettingsTitleRect = new Rect(_GameData.ResolutionX/2 - (float)graphicSettingsMenuWidth/2, 
		                                    _GameData.ResolutionY/2 - (float)graphicSettingsMenuHeight/2 - 50, 
		                                    graphicSettingsMenuWidth, 
		                                    50);
		
		audioSettingsRect = new Rect(_GameData.ResolutionX/2 - (float)audioSettingsMenuWidth/2, 
		                             _GameData.ResolutionY/2 - (float)audioSettingsMenuHeight/2, 
		                             audioSettingsMenuWidth, 
		                             audioSettingsMenuHeight);
		audioSettingsTitleRect = new Rect(_GameData.ResolutionX/2 - (float)audioSettingsMenuWidth/2, 
		                                  _GameData.ResolutionY/2 - (float)audioSettingsMenuHeight/2 - 50, 
		                                  audioSettingsMenuWidth, 
		                                  50);
	}

	public void SetupAE(int index)
	{
		Camera.main.GetComponent<SmoothFollow2>().Effects(index);
	}
	
	public void SetupLanguage(int index)
	{
		_GameData.Lang = index;
		GameText.RefreshLanguage();
	}
	
	public void SetupResolution(int index)
	{
		if(index == 0)
		{
			_GameData.ResolutionX = 768;
			_GameData.ResolutionY = 576;
		}
		else if(index == 1)
		{
			_GameData.ResolutionX = 1024;
			_GameData.ResolutionY = 768;
		}
		else if(index == 2)
		{
			_GameData.ResolutionX = 1280;
			_GameData.ResolutionY = 960;
		}
		Screen.SetResolution(_GameData.ResolutionX, _GameData.ResolutionY, false);
	}
	
	public void SetupSFX(int index)
	{
		
		_GameData.SFXActive = index;
		_AudioEffects.GetSFX();
		_GUISFX.GetSFX();
		_ProDialog.GetSFX();
		Debug.Log(_GameData.SFXActive);
	}
	
	public void SetupBGM(int index)
	{
		_GameData.BGMActive = index;
		_BGM.GetBGM();
	}
	
	public void BackToMainMenu()
	{
		_GUISFX.PlayQuitMenuSound();
		_MainMenu.showMenu = true;
	}
	
	void QuitGame()
	{
		Application.Quit();
	}
	
	void RestartGameFromLastCheckPoint()
	{
		Application.LoadLevel(0);
	}
	
	void SetupGraphicSettings()
	{
		
		if(resolutionPick == 0)
		{
			resolutionString = "768x576";
		}
		else if(resolutionPick == 1)
		{
			resolutionString = "1024x768";
		}

		if(aePick == 0)
		{
			aeString = GameText.Text("On");
		}
		else if(aePick == 1)
		{
			aeString = GameText.Text("Off");
		}
//		else if(resolutionPick == 2)
//		{
//			resolutionString = "1280x1024";
//		}
		
		if(displayLang == 0)
		{
			displayLangString = "English";
		}
		else if(displayLang == 1)
		{
			displayLangString = "简体中文";
		}
		else if(displayLang == 2)
		{
			displayLangString = "日本語";
		}
	}
	
	void EnterSetupGraphicSettings()
	{
		if(_GameData.ResolutionX == 768)
		{
			resolutionPick = 0;
			resolutionString = "768x576";
		}
		else if(_GameData.ResolutionX == 1024)
		{
			resolutionPick = 1;
			resolutionString = "1024x768";
		}
//		else if(_GameData.ResolutionX == 1280)
//		{
//			resolutionPick = 2;
//			resolutionString = "1280x1024";
//		}
		if(aePick == 0)
		{
			aeString = GameText.Text("On");
		}
		else if(aePick == 1)
		{
			aeString = GameText.Text("Off");
		}

		if(displayLang == 0)
		{
			displayLangString = "English";
		}
		else if(displayLang == 1)
		{
			displayLangString = "简体中文";
		}
		else if(displayLang == 2)
		{
			displayLangString = "日本語";
		}
	}
	void SetupAudioSettings()
	{
		if(sfxActive == 1)
		{
			SFXSwitchString = GameText.Text("On");
		}
		else if(sfxActive == 0)
		{
			SFXSwitchString = GameText.Text("Off");
		}
		
		if(bgmActive == 1)
		{
			BGMSwitchString = GameText.Text("On");
		}
		else if(bgmActive == 0)
		{
			BGMSwitchString = GameText.Text("Off");
		}
	}
	
	void EnterSetupAudioSettings()
	{
		if(_GameData.SFXActive == 1)
		{
			sfxActive = 1;
			SFXSwitchString = GameText.Text("On");
		}
		else if(_GameData.SFXActive == 0)
		{
			sfxActive = 0;
			SFXSwitchString = GameText.Text("Off");
		}
		
		if(_GameData.BGMActive == 1)
		{
			bgmActive = 1;
			BGMSwitchString = GameText.Text("On");
		}
		else if(_GameData.BGMActive == 0)
		{
			bgmActive = 0;
			BGMSwitchString = GameText.Text("Off");
		}
	}
	#endregion

	//扩展功能

	#region 主菜单窗口方法
	
	public void OpenMainMenu()
	{
		if(_GamePause.paused && !showMenu)
		{
			mainMenuTitleRect = new Rect(Screen.width/2 - (float)mainMenuWidth/2, Screen.height/2 - (float)mainMenuHeight/2 - 50, mainMenuWidth, 50);
			mainMenuRect = new Rect(Screen.width/2 - (float)mainMenuWidth/2, Screen.height/2 - (float)mainMenuHeight/2, mainMenuWidth, mainMenuHeight);
			_GUISFX.PlayEnterMenuSound();
			showMenu = true;
			nowLayer = 0;
		}
	}
	
	public void CloseMainMenu()
	{
		_GUISFX.PlayQuitMenuSound();
		showMenu = false;
	}

	#endregion 
	#region 游戏选项窗口方法

	public void ShowGameSettings()
	{
		if(!showGameSettings)
		{
			gameSettingsRect = new Rect(Screen.width/2 - (float)gameSettingsMenuWidth/2, 
			                            Screen.height/2 - (float)gameSettingsMenuHeight/2, 
			                            gameSettingsMenuWidth, 
			                            gameSettingsMenuHeight);
			gameSettingsTitleRect = new Rect(Screen.width/2 - (float)gameSettingsMenuWidth/2, 
			                                 Screen.height/2 - (float)gameSettingsMenuHeight/2 - 50, 
			                                 gameSettingsMenuWidth, 
			                                 50);
			showGameSettings = true;
		}
	}
	
	public void HideGameSettings()
	{
		if(showGameSettings)
		{
			showGameSettings = false;
		}
	}

	#endregion 
	#region 退出确认窗口方法

	void ShowQuitDiaBox()
	{
		quitDiaBoxNowLine = 1;
		quitDiaBoxRect = new Rect(Screen.width/2 - (float)quitDiaBoxWidth/2, 
		                          Screen.height/2 - (float)quitDiaBoxHeight/2, 
		                          quitDiaBoxWidth, 
		                          quitDiaBoxHeight);
		quitDiaBoxTitleRect = new Rect(Screen.width/2 - (float)quitDiaBoxWidth/2 -100, 
		                               Screen.height/2 - (float)quitDiaBoxHeight/2 - 100, 
		                               quitDiaBoxWidth + 200, 
		                               100);
		showQuitDiaBox = true;

	}

	void HideQuitDiaBox()
	{
		showQuitDiaBox = false;
	}

	#endregion
	#region 重新开始确认窗口方法
	void ShowRestartDiaBox()
	{
		restartDiaBoxNowLine = 1;
		restartDiaBoxRect = new Rect(Screen.width/2 - (float)restartDiaBoxWidth/2, 
		                             Screen.height/2 - (float)restartDiaBoxHeight/2, 
		                             restartDiaBoxWidth, 
		                             restartDiaBoxHeight);
		restartDiaBoxTitleRect = new Rect(Screen.width/2 - (float)restartDiaBoxWidth/2 - 100, 
		                                  Screen.height/2 - (float)restartDiaBoxHeight/2 - 100, 
		                                  restartDiaBoxWidth + 200, 
		                                  100);
		showRestartDiaBox = true;
	}

	void HideRestartDiaBox()
	{
		showRestartDiaBox = false;
	}

	#endregion
	#region 视频选项窗口方法
	void ShowGraphicSettings()
	{
		if(!showGraphicSettings)
		{
			EnterSetupGraphicSettings();
			graphicSettingsRect = new Rect(Screen.width/2 - (float)graphicSettingsMenuWidth/2, 
			                               Screen.height/2 - (float)graphicSettingsMenuHeight/2, 
			                               graphicSettingsMenuWidth, 
			                               graphicSettingsMenuHeight);
			graphicSettingsTitleRect = new Rect(Screen.width/2 - (float)graphicSettingsMenuWidth/2, 
			                                    Screen.height/2 - (float)graphicSettingsMenuHeight/2 - 50, 
			                                    graphicSettingsMenuWidth, 
			                                 50);
			showGraphicSettings = true;
		}
	}

	public void HideGraphicSettings()
	{
		if(showGraphicSettings)
		{
			showGraphicSettings = false;
		}
	}
	#endregion 
	#region 音频选项窗口方法
	void ShowAudioSettings()
	{
		if(!showAudioSettings)
		{
			EnterSetupAudioSettings();
			audioSettingsRect = new Rect(_GameData.ResolutionX/2 - (float)audioSettingsMenuWidth/2, 
			                             _GameData.ResolutionY/2 - (float)audioSettingsMenuHeight/2, 
			                             audioSettingsMenuWidth, 
			                             audioSettingsMenuHeight);
			audioSettingsTitleRect = new Rect(_GameData.ResolutionX/2 - (float)audioSettingsMenuWidth/2, 
			                                  _GameData.ResolutionY/2 - (float)audioSettingsMenuHeight/2 - 50, 
			                                  audioSettingsMenuWidth, 
			                                  50);
			showAudioSettings = true;
		}
	}

	void HideAudioSettings()
	{
		if(showAudioSettings)
		{
			showAudioSettings = false;
		}
	}
	#endregion

	//窗口方法

	#region GUI显示内容

	void OnGUI()
	{
		if(showMenu)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;
			GUI.Box(mainMenuRect,"");
			GUI.Box(mainMenuTitleRect,GameText.Text("GameMenuTitle"));

			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)mainMenuWidth/2 + 8,
			                 Screen.height/2 - (float)mainMenuHeight/2 + globleOffset + textOffset * mainMenuNowLine,
			                 mainMenuWidth - 17,
			                 35),
			        "");
			GUI.Label(mainMenuRect,GameText.Text("GameMenu"));
		}

		if(showGameSettings)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;
			GUI.Box(gameSettingsRect,"");
			GUI.Box(gameSettingsTitleRect,GameText.Text("GameSettingsMenuTitle"));
			
			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)gameSettingsMenuWidth/2 + 8,
			                 Screen.height/2 - (float)gameSettingsMenuHeight/2 + gameSettingsGlobleOffset + gameSettingsTextOffset * gameSettingsMenuNowLine,
			                 gameSettingsMenuWidth - 17,
			                 35),
			        "");
			GUI.Label(gameSettingsRect,GameText.Text("GameSettingsMenu"));
			
		}

		if(showQuitDiaBox)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;
			GUI.Box(quitDiaBoxRect,"");
			GUI.Box(quitDiaBoxTitleRect,GameText.Text("ConfirmQuitMenuTitle"));
			
			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)quitDiaBoxWidth/2 + 8,
			                 Screen.height/2 - (float)quitDiaBoxHeight/2 + quitDiaBoxGlobleOffset + quitDiaBoxTextOffset * quitDiaBoxNowLine,
			                 quitDiaBoxWidth - 17,
			                 35),
			        "");
			GUI.Label(quitDiaBoxRect,GameText.Text("ConfirmQuitMenu"));
		}

		if(showRestartDiaBox)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;
			GUI.Box(restartDiaBoxRect,"");
			GUI.Box(restartDiaBoxTitleRect,GameText.Text("ConfirmRestartMenuTitle"));
			
			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)restartDiaBoxWidth/2 + 8,
			                 Screen.height/2 - (float)restartDiaBoxHeight/2 + restartDiaBoxGlobleOffset + restartDiaBoxTextOffset * restartDiaBoxNowLine,
			                 restartDiaBoxWidth - 17,
			                 35),
			        "");
			GUI.Label(restartDiaBoxRect,GameText.Text("ConfirmRestartMenu"));
			
		}

		if(showGraphicSettings)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;
			GUI.Box(graphicSettingsRect,"");
			GUI.Box(graphicSettingsTitleRect,GameText.Text("GraphicSettingsMenuTitle"));
			
			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)graphicSettingsMenuWidth/2 + 8,
			                 Screen.height/2 - (float)graphicSettingsMenuHeight/2 + graphicSettingsGlobleOffset + graphicSettingsTextOffset * graphicSettingsMenuNowLine,
			                 graphicSettingsMenuWidth - 17,
			                 35),
			        "");
			GUI.Label(graphicSettingsRect,GameText.Text("Resolution") + "\n" + resolutionString + "\n\n"+ GameText.Text("AE")+ "\n" + aeString + "\n\n"+ GameText.Text("Language") + "\n" + displayLangString + "\n\n" + GameText.Text("Return"));
		}

		if(showAudioSettings)
		{
			if(_GameData.Lang != 0)
				GUI.skin = backgroundSkinCH;
			else
				GUI.skin = backgroundSkinEN;

			GUI.Box(audioSettingsRect,"");
			GUI.Box(audioSettingsTitleRect,GameText.Text("AudioSettingsMenuTitle"));
			
			if(_GameData.Lang != 0)
				GUI.skin = foregroundSkinCH;
			else
				GUI.skin = foregroundSkinEN;
			GUI.Box(new Rect(Screen.width/2 - (float)audioSettingsMenuWidth/2 + 8,
			                 Screen.height/2 - (float)audioSettingsMenuHeight/2 + audioSettingsGlobleOffset + audioSettingsTextOffset * audioSettingsMenuNowLine,
			                 audioSettingsMenuWidth - 17,
			                 35),
			        "");
			GUI.Label(audioSettingsRect,GameText.Text("Sound FX") + "\n" + SFXSwitchString + "\n\n"+ GameText.Text("BGM") + "\n" + BGMSwitchString + "\n\n" + GameText.Text("Return"));
		}
	}

	#endregion

	//GUI内容
}
