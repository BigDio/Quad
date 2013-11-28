/// <summary>
/// 角色移动控制
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class CharacterMovement : GameMaster 
{
	public float gravity = 0.5f;
	public float moveSpeed = 5.0f;
	public float maxMoveSpeed = 5.0f;
	public float maxFallSpeed = 10.0f;
	public float jumpHeight = 0.3f;

	public bool toggleDoubleJump = true;
	public float doubleJumpHeight = 0.2f;
	
	private bool _canDoubleJump;
	private CollisionFlags _cf;
	private Collision _collision;
	private CharacterController _controller;
	private Vector3 _moveDirection;

	public void Setup()
	{
		StartCoroutine("JumpPick");
		_controller = gameObject.GetComponent<CharacterController>();
		_moveDirection = Vector3.zero;
	}

	void Start () 
	{
		Setup();
	}

    IEnumerator JumpPick()
	{
		while(true)
		{
			while(!_GamePause.paused && _AnimateTexture.alive)
			{

				if(Input.GetButtonDown("Jump"))
				{
					if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
					{
						if(_controller.isGrounded)
							Jump();
						if(_canDoubleJump && toggleDoubleJump && !_controller.isGrounded)
							DoubleJump();
					}
				}
				yield return null;
			}
			yield return null;
		}
	}

	void FixedUpdate()
	{

			if(Input.GetAxis("Vertical") < -0.5f && _controller.isGrounded)
			{
				_moveDirection.x = 0;
			}
			else
			{
				if(_AnimateTexture.alive)
				{
					_moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
					_moveDirection.x = Mathf.Clamp(_moveDirection.x, -maxMoveSpeed, maxMoveSpeed);
				}
				else
				{
					_moveDirection.x = 0;
					_moveDirection.y = -0.5f;
				}
			}
			_moveDirection.y = Mathf.Max(-Mathf.Abs(maxFallSpeed), _moveDirection.y);
			_cf = _controller.Move(_moveDirection);

			if(!_controller.isGrounded)
			{
				_moveDirection.y -= gravity * Time.deltaTime;
			}
			else
			{
				if(_cf == CollisionFlags.CollidedBelow)
				{
					_moveDirection.y = -0.1f;
					_canDoubleJump = true;
				}
			}
			if(_cf == CollisionFlags.CollidedAbove)
			{
				//audioEffects.PlayCollideSound();
				_moveDirection.y = -0.1f;
			}
	}

	
	void Jump()
	{
		_AudioEffects.PlayJumpSound();
		_moveDirection.y += jumpHeight;
	}
	
	void DoubleJump()
	{
		_AudioEffects.PlayDoubleJumpSound();
		_moveDirection.y = doubleJumpHeight;
		_canDoubleJump = false;
	}
}