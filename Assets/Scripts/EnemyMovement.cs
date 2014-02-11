using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	
	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public GameObject Player;
	public GameObject EnemySpawner;
	public float velocity = 2.5F;
	public int timer = 0;
	public int chargeTimer;
	EnemySpawn es;
	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector2 movement;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		velocity = 2.5F;
		EnemySpawner = GameObject.Find ("EnemySpawner");
		es = EnemySpawner.GetComponent<EnemySpawn> ();
		if (Vector3.Distance (Player.transform.position, transform.position) < 20) {
			movement = (Player.transform.position - transform.position).normalized;
			movement.x = movement.x * velocity;
			movement.y = movement.y * velocity;
		}
			
			//movement = new Vector2 (velocity.x * (Player.transform.position.x - transform.position.x), velocity.y * (Player.transform.position.y - transform.position.y));
	}
	
	// Update is called once per frame
	void Update () {
		//if (/*velocity == 2.5F &&*/ es.doomEvent == true)
						//velocity = velocity * 1.5F;
		timer += 1;
		if (timer > 50) {
				timer = 0;
			Vector2 direction = new Vector2 (0,0);
			if(gameObject.name == "Enemy(Clone)"){
				if(Vector3.Distance (Player.transform.position,transform.position) < 10 || (es.doomEvent == true && Random.Range(0, 10) > 6))
					direction = (Player.transform.position - transform.position).normalized;
				else
					direction = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2));
			}


			else if(gameObject.name == "Enemy2" /*(Clone)*/){
				//TRICKSY BEHAVIOR GOES HERE!
				if(Vector3.Distance (Player.transform.position,transform.position) < 10){// || (es.doomEvent == true && Random.Range(0, 10) > 6)){
					if(chargeTimer < 2){
						direction = -(Player.transform.position - transform.position).normalized;
						chargeTimer++;
					}
					else if(chargeTimer < 4){
						direction = 2 * (Player.transform.position - transform.position).normalized;
						chargeTimer++;
					}
					else if(chargeTimer >= 4){
						chargeTimer = 0;
					}
					//Tricksy behavior when player is in view.
				}
				else
					direction = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2));
			}
			movement = new Vector2 (velocity * direction.x, velocity * direction.y);
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
		if (gameObject.name == "Enemy(Clone)") {
						if (EnemySpawner != null) {
								es.enemyCount -= 1;
						}
				}
	}
	
	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		rigidbody2D.velocity = movement;
	}
}