using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour 
{
	public float speed;
	void Update () 
	{
		gameObject.transform.Rotate(Vector3.forward, speed);
	}
}
