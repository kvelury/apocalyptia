using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {
	
	public const int cost = 25;
	public GameObject Bullet;

	//timer to attack in intervals
	public float timer = 0;
	public bool attack = false;

	private Transform target;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		timer++;
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Enemy") {
			if(timer > 60){
				timer = 0;
			//target enemy
			target = col.transform;
			//transform.LookAt(target);

			Vector3 startPosition = transform.position;//target.position.x,target.position.y, 0);
			Quaternion rotation = new Quaternion(0,0,0,0);
			//rotation = Quaternion.LookRotation (direction - transform.position);
				Vector3 direction = (target.position - transform.position).normalized;

			GameObject newBullet = (GameObject)Instantiate(Bullet, startPosition, rotation);
				BulletScript bs = newBullet.GetComponent<BulletScript>();
				bs.direction = direction;
			//newBullet.rigidbody2D.AddForce(direction * 5); // shoot in the target direction
			}
		}
	}

}