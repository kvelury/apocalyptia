using UnityEngine;
using System.Collections;

public class WoodenHouseScript : MonoBehaviour {
	
	GameObject Player;
	
	private PlayerResources resources;
	
	public const int cost = 25;
	
	//timer to enable healing at certain intervals
	public float timer = 0;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		resources = Player.GetComponent<PlayerResources>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//subtract amount of it time it took to run previous frame (in seconds)
		timer -= Time.deltaTime;
	}
	
	void OnTriggerStay2D(Collider2D col){
		//get heals from the house
		if (timer <= 0 && col.gameObject.tag == "Player") {
			//set time interval (in seconds)
			timer = 30;
			
			while (resources.healthCount < 100)
				resources.healthCount += 1;
		}
	}
}
