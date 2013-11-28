/// <summary>
/// 用于显示角色头顶的提示操作
/// 制作人：小白
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerHint : GameMaster 
{
	public GameObject upHint;
	void Awake()
	{
		upHint = transform.FindChild("Hint_Up").gameObject;
		HideUpHint();
	}
	public void ShowUpHint()
	{
		upHint.SetActive(true);
	}

	public void HideUpHint()
	{
		upHint.SetActive(false);
	}
}
