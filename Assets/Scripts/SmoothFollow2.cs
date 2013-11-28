/// <summary>
/// 控制摄影机的跟随
/// 制作人：未知
/// </summary>

using UnityEngine;
using System.Collections;

public class SmoothFollow2 : GameMaster {
	public Transform target;
	public float distance = 3.0f;
	public float height = 3.0f;
	public float damping = 5.0f;
	public bool smoothRotation = true;
	public bool followBehind = true;
	public float rotationDamping = 10.0f;
	public bool lockRotation;
	public bool seeFar;
	public float seeDistance = 10.0f;
	public bool limitCamrera;

	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	void Awake()
	{
//		transform.position = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);
	}
	
	void FixedUpdate () {
		Vector3 wantedPosition;

		if(seeFar)
		{
			float sdv = seeDistance * Input.GetAxis("Vertical");

			if(followBehind)
			{
				wantedPosition = target.TransformPoint(0, height, -distance);
				wantedPosition += new Vector3(0,sdv,0);
			}
			else
			{
				wantedPosition = target.TransformPoint(0, height, distance);
				wantedPosition += new Vector3(0,sdv,0);
			}
		}
		else
		{
			if(followBehind)
				wantedPosition = target.TransformPoint(0, height, -distance);
			else
				wantedPosition = target.TransformPoint(0, height, distance);
		}
		
		transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
		
		if (smoothRotation) {
			Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		}
		else transform.LookAt (target, target.up);

		if(lockRotation)
		{
			transform.localRotation = Quaternion.Euler(Vector3.zero);
		}

		if(limitCamrera)
			transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, xMin, xMax),
		                                      Mathf.Clamp(transform.localPosition.y, yMin, yMax),
		                                      transform.localPosition.z
		                                      );
	}

	public void Effects(int index)
	{
//		if(index == 0)
//			gameObject.GetComponent<>().enabled = true;
//		else
//			gameObject.GetComponent<ImageEffectBase>().enabled = false;
	}
}