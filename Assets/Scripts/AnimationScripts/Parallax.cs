/// <summary>
/// 平行效果，使场景更具层次感
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class Parallax : GameMaster
{
	private float xPos;
	private float yPos;
	private float _x;
	private float _y;

	public float offset;
	public float speedMultiply = 0.1f;
	public float xMin = -40.0f;
	public float xMax = 100.0f;
	public bool move;
	public bool moveToLeft;
	public int round;

	void Start () 
	{
		xPos = transform.position.x;
		yPos = transform.position.y;
		_x = Camera.main.transform.position.x;
		_y = Camera.main.transform.position.y;
		offset = Mathf.Max(1.0f,offset);
		round = 0;
	}

	void LateUpdate () 
	{


		if(move)
		{
			transform.position = new Vector3(((Camera.main.transform.position.x - _x) / offset  + xPos + (speedMultiply * -offset * Time.time)) + round*(xMax - xMin),
			                                 (Camera.main.transform.position.y - _y ) / offset + yPos, 
			                                 transform.position.z);
			if(moveToLeft && transform.position.x < xMin)
			{
				transform.position = new Vector3(xMax, (Camera.main.transform.position.y - _y ) / offset + yPos,  transform.position.z);
				round ++;
			}
			else if(!moveToLeft && transform.position.x > xMax)
			{
				transform.position = new Vector3(xMin, (Camera.main.transform.position.y - _y ) / offset + yPos,  transform.position.z);
				round --;
			}
		}
		else
		{
			transform.position = new Vector3((Camera.main.transform.position.x - _x) / offset  + xPos,
			                                  (Camera.main.transform.position.y - _y ) / offset + yPos, 
			                                  transform.position.z);
		}
	}
}
