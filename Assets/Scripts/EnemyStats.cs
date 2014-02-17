using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	public float maxHP;
	public float HP;
	public float damage;
	
	public GameObject Sword;
	public GameObject Shield;
	public GameObject Boots;
	public GameObject Heart;
	public GameObject HealthLeech;
	public GameObject MoreHearts;
	private GameObject itemDrop;
	public GameObject Player;
	public GameObject EnemyParticle;
	public PlayerStats ps;
	public PlayerResources pr;

	public AudioClip hit1;

	private ParticleSystem particleSystem;
	// Use this for initialization
	void Start () {
		if (gameObject.name == "Enemy(Clone)") {
						maxHP = HP = 40;
						damage = 10;
				} 
		else if (gameObject.name == "Enemy2" /*(Clone)*/) {
			maxHP = HP = 60;
			damage = 15;
				}
		Player = GameObject.Find ("Player");
		ps = Player.GetComponent<PlayerStats> ();
		pr = Player.GetComponent<PlayerResources> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, Player.transform.position) > 50) {
			/*Debug.Log ("Enemy Too far away!: Enemy.x = " + transform.position.x + ", Player.x = "
			           + Player.transform.position.x + ", Enemy.y = " + transform.position.y +
			           ", Player.y = " + Player.transform.position.y);*/
						Destroy (gameObject);
				}
		if (HP <= 0){
			if (Player != null) {
				PlayerResources pr = Player.GetComponent<PlayerResources> ();
				if(gameObject.name == "Enemy(Clone)"){
				pr.fameCount += 10;
				//Debug.Log ("Fame++");
				if(GameController.questAccepted == true){
					GameController.killCount += 1;
				}
				}
				else if(gameObject.name == "Enemy2" /*(Clone)*/){
					pr.fameCount+=20;
				}
			}
			int i = Random.Range (1, 15);
			if (i == 1) {
				itemDrop = Instantiate (Sword, transform.position, new Quaternion(0,0,0,0)) as GameObject;
			}
			else if (i == 2) {
				itemDrop = Instantiate (Shield, transform.position, new Quaternion(0,0,0,0)) as GameObject;
			}
			else if (i == 3) {
				itemDrop = Instantiate (Boots, transform.position, new Quaternion(0,0,0,0)) as GameObject;
			}
			else if ((i == 4 || i == 5) || ((i == 6 || i == 7) && ps.inv[11] == 1)) {
				itemDrop = Instantiate (Heart, transform.position, new Quaternion(0,0,0,0)) as GameObject;
			}
			else if (i == 8){
				itemDrop = Instantiate (HealthLeech, transform.position, new Quaternion(0,0,0,0)) as GameObject;
			}
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Weapon") {
			audio.clip = hit1;
			audio.PlayOneShot(audio.clip);
			HP -= ps.currDam;
			//Destroy (col.gameObject);
			particleSystem = Instantiate(EnemyParticle, transform.position, Quaternion.LookRotation(transform.position - Player.transform.position)) as ParticleSystem;
			if(ps.inv[10] == 1)
				pr.healthCount += 1;
		}

		//Bullet code irrelevant, bullet has trigger not collision box.

		/*
		if (col.gameObject.tag == "Bullet") {
			HP -= 10;
			Destroy (col.gameObject);
		}*/
	}
}
