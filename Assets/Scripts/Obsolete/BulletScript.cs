using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Vector3 direction;
	public float speed;
	public float damage;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1.0F);
		speed = 10;
		damage = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	void FixedUpdate(){
		rigidbody2D.velocity = (direction * speed);
	}
	void OnTriggerStay2D(Collider2D col){
		if (col.tag == "Enemy") {
			EnemyStats es = col.gameObject.GetComponent<EnemyStats>();
			es.HP -= damage;
			Destroy (gameObject);
			}
	}
}
