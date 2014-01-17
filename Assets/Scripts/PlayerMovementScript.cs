using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public Vector2 velocity = new Vector3(10, 10);

	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector2 movement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		movement = new Vector2 (velocity.x * direction.x, velocity.y * direction.y);
	}

	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		rigidbody2D.velocity = movement;
	}
}
