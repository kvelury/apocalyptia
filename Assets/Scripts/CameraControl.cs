using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	/// <summary>
	/// The velocity of the camera's free movement
	/// </summary>
	public Vector3 velocity = new Vector3(10, 10, 0);

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
	void Update () {
		if (followingTarget) {
			transform.position = target.position - followDistance;
			transform.LookAt(target);
		}

	}
}
