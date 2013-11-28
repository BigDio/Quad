using UnityEngine;
using System.Collections;

public class VersionControl : MonoBehaviour
{
	private string version = "0.1.6a";
	void Awake () 
	{
		if(!PlayerPrefs.HasKey("ver") || PlayerPrefs.GetString("ver") != version)
		{
			PlayerPrefs.DeleteAll();
			BottomMessageSender.Instance.Send("当前游戏版本:" + version);
			BottomMessageSender.Instance.Send("未找到存档文件或存档版本不一致！");
			BottomMessageSender.Instance.Send("存档已被清空");
			PlayerPrefs.SetString("ver", version);
		}
	}

}
