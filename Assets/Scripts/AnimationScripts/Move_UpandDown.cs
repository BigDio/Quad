/// <summary>
/// 用于控制图片的上下移动
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class Move_UpandDown : MonoBehaviour 
{
	
	void Start () 
	{
	
	}
	

	void Update () 
	{
		transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time*10)/7 + 0.7f, transform.localPosition.z);
	}
}
