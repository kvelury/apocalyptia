using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	/// <summary>
	/// The velocity of the camera's free movement
	/// </summary>
	public float velocity = 10;

	/// <summary>
	/// The size of the screen buffer, within which the mouse will cause the camera to move.
	/// </summary>
	public float screenBufferSize = 25;

	/// <summary>
	/// The target that the camera follows.
	/// </summary>
	public Transform target;

	/// <summary>
	/// followDistance determines the camera's position relative to the target
	/// </summary>
	public Vector3 followDistance = new Vector3 (10, 0, 10);

	/// <summary>
	/// A boolean to represent the camera's movement mode.
	/// True represents the camera is attached to the player.
	/// False represents free-roaming camera.
	/// </summary>
	private bool followingTarget = true;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.C)) {
			followingTarget = false;
		} else {
			followingTarget = true;
		}
		if (followingTarget) {
			transform.position = target.position - followDistance;
			transform.LookAt (target);
		} else {
			//Debug.Log ("currently free roaming");
			if (Input.mousePosition.x > Screen.width - screenBufferSize) {
				transform.position += velocity * new Vector3(1, 0, 0);
				//Debug.Log ("Mouse Right");
			}
			if (Input.mousePosition.x < screenBufferSize) {
				transform.position += velocity * new Vector3 (-1, 0, 0);
				//Debug.Log ("Mouse Left");
			}
			if (Input.mousePosition.y > Screen.height - screenBufferSize) {
				transform.position += velocity * new Vector3 (0, 1, 0);
				//Debug.Log ("Mouse Up");
			}
			if (Input.mousePosition.y < screenBufferSize) {
				transform.position += velocity * new Vector3 (0, -1, 0);
				//Debug.Log ("Mouse Down");
			}
		}
	}
}
