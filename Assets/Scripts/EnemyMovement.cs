using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public GameObject Player;
	public Vector2 velocity = new Vector3(10F, 10F);
	public int timer = 0;
	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector2 movement;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		movement = new Vector2 (velocity.x * (Player.transform.position.x - transform.position.x), velocity.y * (Player.transform.position.y - transform.position.y));
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1;
		if (timer > 50) {
				timer = 0;
				Vector2 direction = new Vector3 (Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);//Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
				movement = new Vector2 (velocity.x * direction.x, velocity.y * direction.y);
		}
	}
	
	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		rigidbody2D.velocity = movement;
	}
}