using UnityEngine;
using System.Collections;

public class MessageArea : MonoBehaviour 
{

	public string showText = "输入字符";
	public float showTime = 3;
	public float fadeInSpeed = 3;
	public float fadeOutSpeed = 1;
	public Color textColor = Color.white;
	public int fontSize = 50;

	public GameObject messenger;

	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameObject temp = GameObject.FindGameObjectWithTag("TopMessage");
			if(temp != null)
			{
				Destroy(temp);
			}
			GameObject go = Instantiate(messenger) as GameObject;
			go.GetComponent<TopMessage>().Setup(showText,fontSize,showTime,fadeInSpeed,fadeOutSpeed,textColor);
		}
	}
}
