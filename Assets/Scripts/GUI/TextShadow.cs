using UnityEngine;
using System.Collections;

public class TextShadow : MonoBehaviour 
{
	private GUIText parentGUI;
	void Start ()
	{
		gameObject.GetComponent<GUIText>().text = "";
	}

	void Update () 
	{
		if(parentGUI == null)
			parentGUI = transform.root.gameObject.GetComponent<GUIText>();
		if(gameObject.GetComponent<GUIText>().text != parentGUI.text)
			gameObject.GetComponent<GUIText>().text = parentGUI.text;
		gameObject.GetComponent<GUIText>().color = new Color(
			gameObject.GetComponent<GUIText>().color.r,
			gameObject.GetComponent<GUIText>().color.g,
			gameObject.GetComponent<GUIText>().color.b,
			parentGUI.color.a);
	}
}
