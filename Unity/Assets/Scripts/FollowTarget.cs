using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour 
{
	public Transform target;
	public bool isSmooth = false;
	public float horiozntalDistance = 5f;
	public float verticalDistance = 2f;
	public float filter = 0.3f;

	void LateUpdate() 
	{
		var targetPosition = target.position - target.forward * horiozntalDistance + target.up * verticalDistance;
		if (isSmooth) {
			transform.position += (targetPosition - transform.position) * filter; 
		} else {
			transform.position = targetPosition; 
		}
	}
}
