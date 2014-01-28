using UnityEngine;
using System.Collections;

public class NPCBehavior : MonoBehaviour {
	public GameObject Player;
	public Vector2 velocity;
	public float timer;
	public bool interacting;
	private Vector2 movement;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		velocity.x = velocity.y = 1F;
		Vector2 direction = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2));
		movement = new Vector2 (velocity.x * direction.x, velocity.y * direction.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (Player.transform.position, transform.position) < 2)
						movement = new Vector2 (0, 0);
		else {
				timer++;
				if (timer >= 50) {
						timer = 0;
						Vector2 direction = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2));
						movement = new Vector2 (velocity.x * direction.x, velocity.y * direction.y);
				}
		}
	
	}

	void FixedUpdate() {
		rigidbody2D.velocity = movement;
	}
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.name == "Player") {
						movement.x = movement.y = 0F;
				}
	}
}
