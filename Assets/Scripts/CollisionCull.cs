using UnityEngine;
using System.Collections;

public class CollisionCull : MonoBehaviour 
{
	public MeshCollider children;

	void Awake()
	{
		children = transform.FindChild("Step").GetComponent<MeshCollider>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			children.enabled = false;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			children.enabled = true;
		
		}
	}
}
