using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public float velocity = 10;

	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector3 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);

		movement = new Vector3 (velocity * direction.x, velocity * direction.y, 0);
	}

	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		transform.position += movement;
	}
}
