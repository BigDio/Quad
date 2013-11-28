/// <summary>
/// 游戏中所包含的文本
/// 制作人：小白
/// </summary>

using UnityEngine;
using System.Collections;

public class GameStrings : MonoBehaviour 
{
	void Awake()
	{
		RefreshLanguage();
	}
	public int lang;
	public void RefreshLanguage()
	{
		lang = PlayerPrefs.GetInt("Language", 1);
	}
	private string returnString;
	public string Text(string index)
	{
		if(lang == 0)
		{
			switch(index)
			{
				#region 通用
			case "On":
				returnString = "ON";
				break;
			case "Off":
				returnString = "OFF";
				break;
			case "OK":
				returnString = "OK";
				break;
			case "Cancle":
				returnString = "Cancle";
				break;
			case "Yes":
				returnString = "Yes";
				break;
			case "No":
				returnString = "No";
				break;
			case "Return":
				returnString = "Return";
				break;
			case "Save":
				returnString = "Save";
				break;
				#endregion
				#region 菜单
			case "GameMenuTitle":
				returnString = "Game Menu";
				break;
			case "GameMenu":
				returnString = "Resume\nRestart\nOptions\nQuit Game";
				break;
			case "GameSettingsMenuTitle":
				returnString = "Options";
				break;
			case "GameSettingsMenu":
				returnString = "Graphic Settings\nAudio Settings\nControl Config\nAbout\nReturn";
				break;
			case "ConfirmQuitMenuTitle":
				returnString = "You really want to quit?\nAll your unsaved progress will lost!";
				break;
			case "ConfirmQuitMenu":
				returnString = "Yes\nNo";
				break;
			case "ConfirmRestartMenuTitle":
				returnString = "Restart form last checkpoint?\nAll your unsaved progress will lost!";
				break;
			case "ConfirmRestartMenu":
				returnString = "Yes\nNo";
				break;
			case "GraphicSettingsMenuTitle":
				returnString = "Graphic Settings";
				break;
			case "Resolution":
				returnString = "<- Resolution ->";
				break;
			case "Language":
				returnString = "<- Language ->";
				break;
			case "AudioSettingsMenuTitle":
				returnString = "Audio Settings";
				break;
			case "Sound FX":
				returnString = "<- Sound FX ->";
				break;
			case "BGM":
				returnString = "<- BGM ->";
				break;
			case "AE":
				returnString = "<- Visual Effects ->";
				break;
				#endregion
			case "Sign001":
				returnString = "Sign|This game demo is made by BigDio Studio.";
				break;
			case "Sign002":
				returnString = "Sign|Press X to attack(TBA), press Z to jump\nGame Version: Alpha_0.0.8\nRelease Date: 2013-11-22";
				break;
			case "Sign003":
				returnString = "Sign|Update Details: TBA";
				break;
			case "StrawMan001":
				returnString = "Scare Crow|....";
				break;
			}
		}
		else if(lang == 1 || lang == 2)
		{
			switch(index)
			{
				#region 通用
			case "On":
				returnString = "开";
				break;
			case "Off":
				returnString = "关";
				break;
			case "OK":
				returnString = "确定";
				break;
			case "Cancle":
				returnString = "取消";
				break;
			case "Yes":
				returnString = "是";
				break;
			case "No":
				returnString = "否";
				break;
			case "Return":
				returnString = "返回";
				break;
			case "Save":
				returnString = "保存";
				break;
				#endregion
				#region 菜单
			case "GameMenuTitle":
				returnString = "游戏菜单";
				break;
			case "GameMenu":
				returnString = "返回游戏\n重新开始\n游戏设置\n退出游戏";
				break;
			case "GameSettingsMenuTitle":
				returnString = "游戏设置";
				break;
			case "GameSettingsMenu":
				returnString = "视频选项\n音频选项\n控制设定\n关于\n返回";
				break;
			case "ConfirmQuitMenuTitle":
				returnString = "确认退出游戏?\n您未保存的内容将会丢失!";
				break;
			case "ConfirmQuitMenu":
				returnString = "是\n否";
				break;
			case "ConfirmRestartMenuTitle":
				returnString = "确认重新开始?\n您未保存的内容将会丢失!";
				break;
			case "ConfirmRestartMenu":
				returnString = "是\n否";
				break;
			case "GraphicSettingsMenuTitle":
				returnString = "视频选项";
				break;
			case "Resolution":
				returnString = "<- 分辨率 ->";
				break;
			case "Language":
				returnString = "<- 语言 ->";
				break;
			case "AudioSettingsMenuTitle":
				returnString = "音频选项";
				break;
			case "Sound FX":
				returnString = "<- 音效 ->";
				break;
			case "BGM":
				returnString = "<- 音乐 ->";
				break;
			case "AE":
				returnString = "<- 渲染特效 ->";
				break;
				#endregion 
			case "Sign001":
				returnString = "路牌|本游戏Demo由[大雕游戏研发工作室(BigDio Studio)]开发制作。\n起源于 ACFUN - 匿名版。";
				break;
			case "Sign002":
				returnString = "路牌|操作说明：x键攻击(暂无)，z键跳跃\n游戏版本: Alpha_0.1.0\n发布日期: 2013-11-23。";
				break;
			case "Sign003":
				returnString = "路牌|本次更新内容：1.暂时移除后期特效以及若干美术资源 2.添加H3LP制作的美术资源 3.果酱面包的彩蛋一枚";
				break;
			case "StrawMan001":
				returnString = "一只稻草人，身上似乎插着一只铁钉。";
				break;
			}
		}
		return returnString;
	}
}
