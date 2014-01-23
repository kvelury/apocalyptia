using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public GameObject Player;
	public GameObject EnemySpawner;
	public Vector2 velocity = new Vector3(10F, 10F);
	public int timer = 0;
	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector2 movement;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		EnemySpawner = GameObject.Find ("EnemySpawner");
		if (Vector3.Distance (Player.transform.position, transform.position) < 20) {
			movement = (Player.transform.position - transform.position).normalized;
			movement.x = movement.x * velocity.x;
			movement.y = movement.y * velocity.y;
		}
			
			//movement = new Vector2 (velocity.x * (Player.transform.position.x - transform.position.x), velocity.y * (Player.transform.position.y - transform.position.y));
	}
	
	// Update is called once per frame
	void Update () {
		timer += 1;
		if (timer > 50) {
				timer = 0;
			Vector2 direction;
			if(Vector3.Distance (Player.transform.position,transform.position) < 10)
				direction = (Player.transform.position - transform.position).normalized;
			else
				direction = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2));
			movement = new Vector2 (velocity.x * direction.x, velocity.y * direction.y);
		}
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Weapon") {
			movement.x = -2F*movement.x;
			movement.y = -2F*movement.y;
			timer = 40;
		}
	}

	void OnDestroy(){
		if (Player != null) {
						PlayerResources pr = Player.GetComponent<PlayerResources> ();
						pr.fameCount += 10;
				}
		if (EnemySpawner != null) {
						EnemySpawn es = EnemySpawner.GetComponent<EnemySpawn> ();
						es.enemyCount -= 1;
				}
	}
	
	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		rigidbody2D.velocity = movement;
	}
}