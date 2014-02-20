using UnityEngine;
using System.Collections;

public class WoodenHouseScript : MonoBehaviour {
	
	GameObject Player;
	
	private PlayerResources resources;
	
	public const int wCost = 30;
	public const int sCost = 15;
	
	//timer to enable healing at certain intervals
	public float timer = 1.5f;
	public bool heal = false;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		resources = Player.GetComponent<PlayerResources>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//subtract amount of it time it took to run previous frame (in seconds)
		timer -= Time.deltaTime;

		if (timer <= 0) {
			if (heal == true) heal = false;
			else if (heal == false) heal = true;
			timer = 1.5f;
		}
	}

	void FixedUpdate(){
		if(Vector3.Distance (Player.transform.position, transform.position) <= 2){
			if (heal == true) {
				resources.healthCount += 5;
				heal = false;
			}
			if (resources.healthCount >= 100) 
				resources.healthCount = 100;
			
		}
	}
	
	void OnTriggerStay2D(Collider2D col){
		//get heals from the house
		if (col.gameObject.tag == "Player") {
			if (heal == true) {
				resources.healthCount += 5;
				heal = false;
			}
			if (resources.healthCount >= 100) 
				resources.healthCount = 100;

		}
	}
}
