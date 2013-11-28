using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour 
{
	public float yOffset;
	private Vector2 offset;
	private Vector3 height;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		offset.x = Time.time;
		height = new Vector3(gameObject.transform.localPosition.x, Mathf.Sin(Time.time)/10 + yOffset, gameObject.transform.localPosition.z);
		renderer.material.SetTextureOffset ("_MainTex", offset);
		gameObject.transform.localPosition = height;
	}
}
