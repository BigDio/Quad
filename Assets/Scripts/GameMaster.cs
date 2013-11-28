/// <summary>
/// 游戏核心，用于取用引用
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour
{
	#region 获取引用
	private GameObject _player;
	public GameObject Player
	{
		get
		{
			if(_player == null)
				_player = GameObject.FindGameObjectWithTag("Player");
			return _player;
		}
	}

	private CharacterController _characterController;
	public CharacterController _CharacterController
	{
		get
		{
			if(_characterController == null)
				_characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
			return _characterController;
		}
	}

	private PlayerHint _playerHint;
	public PlayerHint _PlayerHint
	{
		get
		{
			if(_playerHint == null)
				_playerHint = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHint>();
			return _playerHint;
		}
	}

	private GameStrings _gameStrings;
	public GameStrings GameText
	{
		get
		{
			if(_gameStrings == null)
				_gameStrings = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameStrings>();
			return _gameStrings;
		}
	}


	private GameData _gameData;
	public GameData _GameData
	{
		get
		{
			if(_gameData == null)
				_gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
			return _gameData;
		}
	}

	private MainMenu _mainMenu;
	public MainMenu _MainMenu
	{
		get
		{
			if(_mainMenu == null)
				_mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
			return _mainMenu;
		}
	}

	private GameMaster _gameMaster;
	public GameMaster _GM
	{
		get
		{
			if(_gameMaster == null)
				_gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
			return _gameMaster;
		}
	}

	private GamePause _gamePause;
	public GamePause _GamePause
	{
		get
		{
			if(_gamePause == null)
				_gamePause = GameObject.FindGameObjectWithTag("GamePause").GetComponent<GamePause>();
			return _gamePause;
		}
	}

	private GUISFX _guiSFX;
	public GUISFX _GUISFX
	{
		get
		{
			if(_guiSFX == null)
				_guiSFX = GameObject.FindGameObjectWithTag("GUISFX").GetComponent<GUISFX>();
			return _guiSFX;
		}
	}

	private ProDialog _proDialog;
	public ProDialog _ProDialog
	{
		get
		{
			if(_proDialog == null)
				_proDialog = GameObject.FindGameObjectWithTag("ProDialog").GetComponent<ProDialog>();
			return _proDialog;
		}
	}

	private SelectionBox _selectionBox;
	public SelectionBox _SelectionBox
	{
		get
		{
			if(_selectionBox == null)
				_selectionBox = GameObject.FindGameObjectWithTag("SelectionBox").GetComponent<SelectionBox>();
			return _selectionBox;
		}
	}

	private StoryBoard _storyBoard;
	public StoryBoard _StoryBoard
	{
		get
		{
			if(_storyBoard == null)
				_storyBoard = GameObject.FindGameObjectWithTag("StoryBoard").GetComponent<StoryBoard>();
			return _storyBoard;
		}
	}

	private AudioEffects _audioEffects;
	public AudioEffects _AudioEffects
	{
		get
		{
			if(_audioEffects == null)
				_audioEffects = GameObject.FindGameObjectWithTag("AudioEffects").GetComponent<AudioEffects>();
			return _audioEffects;
		}
	}

	private AnimateTexture _animateTexture;
	public AnimateTexture _AnimateTexture
	{
		get
		{
			if(_animateTexture == null)
				_animateTexture = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimateTexture>();
			return _animateTexture;
		}
	}

	private SmoothFollow2 _smoothFollow2;
	public SmoothFollow2 _SmoothFollow2
	{
		get
		{
			if(_smoothFollow2 == null)
				_smoothFollow2 = Camera.main.GetComponent<SmoothFollow2>();
			return _smoothFollow2;
		}
	}

	private BGM _bgm;
	public BGM _BGM
	{
		get
		{
			if(_bgm == null)
			_bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGM>();
			return _bgm;
		}
	}

	private CharacterMovement _characterMovement;
	public CharacterMovement _CharacterMovement
	{
		get
		{
			if(_characterMovement == null)
				_characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
			return _characterMovement;
		}
	}
	#endregion

	#region 公共变量

	#endregion
}
