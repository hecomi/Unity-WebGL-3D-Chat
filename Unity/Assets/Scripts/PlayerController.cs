using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public bool isMainPlayer = true;
	public Text textUi;

	private Vector3 prePosition_;
	private Quaternion preRotation_;

	public float moveSpeed = 1f;
	public float rotationSpeed = 10f;
	public float jumpForce = 3000f;

	private bool isGround_ = false;

	private Rigidbody rigidbody_;
	public Rigidbody rigidbody
	{
		get { return (rigidbody_ = rigidbody_ ?? GetComponent<Rigidbody>()); }
	}

	void Update() 
	{
		if (!isMainPlayer) return;

		var velocity = Vector3.zero;
		var angularVelocity = 0f;

		if (Input.GetKey(KeyCode.W)) {
			velocity += transform.forward;
		}
		if (Input.GetKey(KeyCode.S)) {
			velocity -= transform.forward;
		}
		if (Input.GetKey(KeyCode.A)) {
			velocity -= transform.right;
		}
		if (Input.GetKey(KeyCode.D)) {
			velocity += transform.right;
		}

		if (Input.GetKey(KeyCode.Q)) {
			angularVelocity -= 1;
		}
		if (Input.GetKey(KeyCode.E)) {
			angularVelocity += 1;
		}

		if (isGround_ && Input.GetKeyDown(KeyCode.Space)) {
			isGround_ = false;
			rigidbody.AddForce(Vector3.up * jumpForce);
		}
	
		velocity = velocity.normalized * moveSpeed;
		angularVelocity *= rotationSpeed;

		transform.position += velocity * Time.deltaTime;
		transform.Rotate(Vector3.up, angularVelocity);

		EmitPosition();
		EmitRotation();
	}

	void LateUpdate()
	{
		if (transform.position.y < 0.4f) {
			var pos = transform.position;
			pos.y = 0.55f;
			transform.position = pos;
		}
	}

	void EmitPosition()
	{
		var pos = transform.position;
		if (pos != prePosition_) {
			prePosition_ = pos;
			Application.ExternalCall("socket.emit", "move", pos.x, pos.y, pos.z); 
		}
	}

	void EmitRotation()
	{
		var rot = transform.rotation;
		if (rot != preRotation_) {
			preRotation_ = rot;
			Application.ExternalCall("socket.emit", "rotate", rot.x, rot.y, rot.z, rot.w, Time.frameCount); 
		}
	}

	void Talk(string message)
	{
		textUi.text = message;
		Application.ExternalCall("socket.emit", "talk", message); 
	}

	void OnCollisionEnter(Collision collision)
	{
		isGround_ = true;
	}
}
