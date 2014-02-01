using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	public float maxHP;
	public float HP;
	public float damage;
	public GameObject Player;
	public PlayerStats ps;
	// Use this for initialization
	void Start () {
		maxHP = HP = 40;
		damage = 5;
		Player = GameObject.Find ("Player");
		ps = Player.GetComponent<PlayerStats> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (HP <= 0){
			if (Player != null) {
				PlayerResources pr = Player.GetComponent<PlayerResources> ();
				pr.fameCount += 10;
				//Debug.Log ("Fame++");
				if(GameController.questAccepted == true){
					GameController.killCount += 1;
				}
			}
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Weapon") {
			HP -= ps.currDam;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Bullet") {
			HP -= 10;
			Destroy (col.gameObject);
		}
	}
}
