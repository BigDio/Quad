using UnityEngine;
using System.Collections;

public class BottomMessageSender : MonoBehaviour 
{
	public float offset;
	public static BottomMessageSender Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject("[BottomMessageSender]").AddComponent<BottomMessageSender>();
			return instance;
		}
	}
	private static BottomMessageSender instance = null;

	public GameObject BottomMessagePrefab;
	void Awake()
	{
		if(instance)
			DestroyImmediate(gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void Send(string text)
	{
		GameObject[] others = GameObject.FindGameObjectsWithTag("BottomMessage");
		foreach(GameObject en in others)
		{
			en.transform.localPosition += Vector3.up * offset;
		}
		GameObject go = Instantiate(BottomMessagePrefab) as GameObject;
		go.GetComponent<BottomMessage>().Send(text);
	}
}
