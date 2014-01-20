using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	public float maxHP;
	public float HP;
	public float damage;
	// Use this for initialization
	void Start () {
		maxHP = HP = 40;
		damage = 5;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (HP <= 0)
						Destroy (gameObject);
	
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Weapon") {
			HP -= 10;
			Destroy (col.gameObject);
		}
	}
}
