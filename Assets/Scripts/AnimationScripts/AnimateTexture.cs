/// <summary>
/// 控制贴图序列帧播放
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class AnimateTexture : GameMaster
{
	private enum state
	{
		set,
		start,
		end
	}
	private float _x;
	private float goalTime;
	private float endTime;
	private state now;
	private bool isShowing;
	private int rand;

	public Material Idle;
	public Material Walk;
	public Material Jump;
	public Material Dock;
	public Material Die;
	public Material YoooIdle;
	public Material	YoooWalk;
	public Material YoooJump;
	public Material	YoooDock;

	//vars for the whole sheet
	public int colCount = 4;
	public int rowCount = 1;
	
	//vars for animation
	public int rowNumber = 0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 4;
	public int fps = 10;
	//Maybe this should be a private var
	private Vector2 offset;
	//Update
	private bool grounded;

	private bool yoo = false;

	public bool alive = true;

	public void Setup()
	{
		_x = Mathf.Abs(gameObject.transform.localScale.x);
		now = state.set;
		isShowing = false;
		alive = true;
	}

	void Start ()
	{
		Setup ();
	}

	void Update () 
	{

		if(!alive)
		{
			if(Input.GetKeyDown(KeyCode.P))
			{
				alive = true;
			}
		}
		else
		{
			///////////////////////
			if(Input.GetKeyDown(KeyCode.Y))
			{
				BottomMessageSender.Instance.Send("换装成功");
				yoo = !yoo;
			}
			grounded = gameObject.GetComponent<CharacterController>().isGrounded;
			if(!grounded)
			{
				JumpAnimation();
			}
			else
			{
				if(Input.GetAxis ("Vertical") < 0.0f)
				{
					isShowing = false;
					now = state.set;
					///////////////////////
					if(renderer.material != Dock || renderer.material != YoooDock)
					{
						if(yoo)
							renderer.material = YoooDock;
						else
							renderer.material = Dock;
					}
					if(Input.GetAxis ("Vertical") > -0.5f)
						SetSpriteAnimation(4,rowCount,rowNumber,1,1,fps);
					else
						SetSpriteAnimation(4,rowCount,rowNumber,2,2,fps);
				}
				else if(Input.GetAxis ("Horizontal") != 0)
				{
					isShowing = false;
					now = state.set;
					///////////////////////
					if(renderer.material != Walk || renderer.material != YoooWalk)
					{
						if(yoo)
							renderer.material = YoooWalk;
						else
							renderer.material = Walk;
					}
					if(Input.GetAxis ("Horizontal") < 0)
					{
						gameObject.transform.localScale = new Vector3(-_x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
					}
					else
					{
						gameObject.transform.localScale = new Vector3(_x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
					}
					SetSpriteAnimation(colCount,rowCount,rowNumber,colNumber,totalCells,fps);
				}
				else 
				{
					if(yoo)
					{
						ShowIdle(4, 3.0f, 2.0f);
						if(!isShowing)
							if(renderer.material != YoooIdle)
								renderer.material = YoooIdle;
					}
					else
					{
						ShowIdle(4, 3.0f, 2.0f);
						if(!isShowing)
							if(renderer.material != Idle)
								renderer.material = Idle;
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.P))
			{
				alive = false;
				_AudioEffects.PlayDieSound();
				if(renderer.material != Die)
				{
					BottomMessageSender.Instance.Send("角色已死亡");
					renderer.material = Die;
				}
				
				StartCoroutine("SetSpriteAnimationOnce");
			}
		}
	}

	public void JumpAnimation()
	{
		///////////////////////
		if(renderer.material != Walk || renderer.material != YoooWalk)
		{	
			if(yoo)
				renderer.material = YoooJump;
			else
				renderer.material = Jump;
			SetSpriteAnimation(2,1,0,0,2,fps);

		}

	}
	
	//SetSpriteAnimation
	void SetSpriteAnimation(int colCount ,int rowCount ,int rowNumber ,int colNumber,int totalCells,int fps )
	{
			// Calculate index
			int index = (int)(Time.time * fps);
	
			// Repeat when exhausting all cells
			index = index % totalCells;
			// Size of every cell
			float sizeX = 1.0f / colCount;
			float sizeY = 1.0f / rowCount;
			Vector2 size =  new Vector2(sizeX,sizeY);
			
			// split into horizontal and vertical index
			int uIndex = index % colCount;
			int vIndex = index / colCount;
			
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			float offsetX = (uIndex+colNumber) * size.x;
			float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
			Vector2 offset = new Vector2(offsetX,offsetY);
			renderer.material.SetTextureOffset ("_MainTex", offset);
			renderer.material.SetTextureScale  ("_MainTex", size);

	}

	IEnumerator SetSpriteAnimationOnce()
	{
		int	tempIndex = ((int)(Time.time * fps))%4;
		while(true)
		{
			// Calculate index
			int index  = (int)(Time.time * fps) - tempIndex;
			// Repeat when exhausting all cells
			index = index % totalCells;
			if(index < totalCells)
			{
				// Size of every cell
				float sizeX = 1.0f / colCount;
				float sizeY = 1.0f / rowCount;
				Vector2 size =  new Vector2(sizeX,sizeY);
				
				// split into horizontal and vertical index
				int uIndex = index % colCount;
				int vIndex = index / colCount;
				
				// build offset
				// v coordinate is the bottom of the image in opengl so we need to invert.
				float offsetX = (uIndex+colNumber) * size.x;
				float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
				Vector2 offset = new Vector2(offsetX,offsetY);
				renderer.material.SetTextureOffset ("_MainTex", offset);
				renderer.material.SetTextureScale  ("_MainTex", size);
			}
			if(index >= totalCells-1)
			{
				StopCoroutine("SetSpriteAnimationOnce");
			}
			yield return null;
		}
	}

	private void ShowIdle(int count,float frequency, float showTime)
	{
		switch(now)
		{
		case state.set:
			goalTime = Time.time + frequency + Random.Range(0,5);
			now = state.start;
			break ;

		case state.start:
			if(Time.time >= goalTime)
			{
				rand = (int)(Random.Range(1,count));
				if(rand == 3)
				{
					showTime = 0.1f;
				}
				endTime = Time.time + showTime;
				now = state.end;
			}
			break;

		case state.end:
			if(!isShowing)
			{
				ShowIdleImage(count, rand);
			}
			else
			{
				if(Time.time > endTime)
				{
					now = state.set;
					isShowing = false;
				}
			}
			break;

		default:
			break;
		}
	}

	private void ShowIdleImage(int count, int rand)
	{
		isShowing = true;
		Vector2 offset = new Vector2((1.0f/count)*rand,0);
		Vector2 size = new Vector2( 1.0f/count, 1.0f);
		renderer.material.SetTextureOffset ("_MainTex", offset);
		renderer.material.SetTextureScale  ("_MainTex", size);
	}
}