using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkPlayerController : MonoBehaviour 
{
	public Text textUi;

	public void Move(Vector3 position) 
	{
		transform.position = position;
	}
	
	public void Rotate(Quaternion rotation) 
	{
		transform.rotation = rotation;
	}

	public void Talk(string message)
	{
		textUi.text = message;
	}
}
