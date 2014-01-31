using UnityEngine;
using System.Collections;

public class ResourceHouseScript : MonoBehaviour {
	
	GameObject Player;
	private PlayerResources resources;
	
	public const int wCost = 75;
	public const int sCost = 50;
	public float timer = 0;
	private int resCount = 0;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		resources = Player.GetComponent<PlayerResources>();
		//intial amount of resource given
		resCount = 10;
	}
	
	// Update is called once per frame
	void Update () {
		//subtract amount of it time it took to run previous frame (in seconds)
		timer -= Time.deltaTime;
	}
	
	void OnTriggerStay2D(Collider2D col){
		//get resources from the house
		
		if (timer <= 0 && col.gameObject.tag == "Player") {
			//set time interval (in seconds)
			timer = 60;
			
			for (int i = resCount; i > 0; i--) {
				resources.stoneCount += 1;
				resources.woodCount += 1;
			}
			//increase amount of resources given every time player has collected
			resCount += 5;
		}
		
	}
}