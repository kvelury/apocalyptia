using UnityEngine;
using System.Collections;

public class GameplayEvents : MonoBehaviour {

	GameObject Player;

	private PlayerResources resources;
	private PlayerStats stats;
	public GameObject woodenHouse;
	public GameObject resourceHouse;
	public GameObject tower;

	/// <summary>
	/// Boolean representing whether the game is paused or not
	/// </summary>
	private bool isPaused;

	/// <summary>
	/// The state of the pause menu.
	/// 0 - Base Pause Menu
	/// 1 - Inventory Screen
	/// 2 - Crafting Screen
	/// 3 - Building Screen
	/// </summary>
	private int pauseMenuState;


	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		stats = Player.GetComponent<PlayerStats> ();
		resources = Player.GetComponent<PlayerResources>();
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){
			Pause ();
		}
	}

	//GUI
	void OnGUI(){
		if (isPaused){
			switch (pauseMenuState){
			case 0:
				PauseMenuEvents();
				break;
			case 1:
				InventoryEvents();
				break;
			case 2:
				CraftingEvents();
				break;
			case 3:
				BuildingEvents();
				break;
			default:
				break;
			}
			
		}else{
			// Make a background box
			Player = GameObject.Find ("Player");
			GUI.Box (new Rect (10, 10, 100, 165), "");
			//Once combat is implemented, remove If Statement from here, and change Button to Box.
			
			//Once resource collection is implemented use the section below to just display resources rather than add them.
			if(GUI.Button (new Rect (20, 20, 80, 25), "Health: " + resources.healthCount.ToString ()))
				resources.healthCount -= 10;
			
			if(GUI.Button (new Rect (20, 50, 80, 25), "Wood: " + resources.woodCount.ToString ()))
				resources.woodCount += 10;
			
			if(GUI.Button (new Rect (20, 80, 80, 25), "Stone: " + resources.stoneCount.ToString ()))
				resources.stoneCount += 10;
			
			if(GUI.Button (new Rect (20, 110, 80, 25), "Iron: " + resources.ironCount.ToString ()))
				resources.ironCount += 10;
			
			/* Use for later when resource collection is implemented
			GUI.Box (new Rect (20, 50, 80, 25), "Wood: " + resources.woodCount.ToString ());
			GUI.Box (new Rect (20, 80, 80, 25), "Stone: " + resources.stoneCount.ToString ());
			GUI.Box (new Rect (20, 110, 80, 25), "Iron: " + resources.ironCount.ToString ());
			*/
			GUI.Box (new Rect (20, 140, 80, 25), "Fame: " + resources.fameCount.ToString ());
		}
	}

	void Pause(){
		isPaused = true;
		Time.timeScale = 0.0f;
	}

	void UnPause(){
		isPaused = false;
		Time.timeScale = 1.0f;
	}

	void PauseMenuEvents(){
		GUI.Label(new Rect(Screen.width/2 - 50, 0, 100, 50), "GAME PAUSED");

		if (GUI.Button (new Rect(Screen.width/2 - 100, Screen.height * 1/4 - 50, 200, 50), "Inventory")){
			pauseMenuState = 1;
		}
		if (GUI.Button (new Rect(Screen.width/2 - 100, Screen.height * 2/4 - 50, 200, 50), "Crafting")){
			pauseMenuState = 2;
		}
		if (GUI.Button (new Rect(Screen.width/2 - 100, Screen.height * 3/4 - 50, 200, 50), "Building")){
			pauseMenuState = 3;
		}
		if (GUI.Button (new Rect(Screen.width - 50, 0, 50, 50), "X")){
			UnPause();
		}
	}

	void InventoryEvents(){
		GUI.Label (new Rect(Screen.width/2 - 50, 0, 100, 50), "INVENTORY");
		if (GUI.Button (new Rect(Screen.width - 50, 0, 50, 50), "X")){
			pauseMenuState = 0;
		}
		for (int i = 0; i < 5; i++) {
			if(stats.inv[i] == 0){
				GUI.Box (new Rect(i*200, 25, 100, 100), "Empty");
			}
			else if(stats.inv[i] == 1){
				GUI.Box (new Rect(i*200, 25, 100, 100), "Wood Spear\n+10 Damage");
			}
			else if(stats.inv[i] == 2){
				GUI.Box (new Rect(i*200, 25, 100, 100), "Stone Spear\n+30 Damage");
			}
		}
		for (int i = 5; i < 10; i++) {
			if(stats.inv[i] == 0){
				GUI.Box (new Rect((i-5)*200, 150, 100, 100), "Empty");
			}
			else if(stats.inv[i] == 1){
				GUI.Box (new Rect((i-5)*200, 150, 100, 100), "Wood Spear\n+10 Damage");
			}
			else if(stats.inv[i] == 2){
				GUI.Box (new Rect((i-5)*200, 150, 100, 100), "Stone Spear\n+30 Damage");
			}
		}
		for (int i = 10; i < 15; i++) {
			if(stats.inv[i] == 0){
				GUI.Box (new Rect((i-10)*200, 275, 100, 100), "Empty");
			}
			else if(stats.inv[i] == 1){
				GUI.Box (new Rect((i-10)*200, 275, 100, 100), "Wood Spear\n+10 Damage");
			}
			else if(stats.inv[i] == 2){
				GUI.Box (new Rect((i-10)*200, 275, 100, 100), "Wood Spear\n+30 Damage");
			}
		}
	}

	void CraftingEvents(){
		GUI.Label (new Rect(Screen.width/2 - 50, 0, 100, 50), "CRAFTING");
		if (GUI.Button (new Rect(Screen.width - 50, 0, 50, 50), "X")){
			pauseMenuState = 0;
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 100), "Wood Spear\n20 Wood")) {
						if (resources.woodCount >= 20) {
								int insert = 15;
								for (int i = 0; i < 15; i++) {
										if (stats.inv [i] == 0) {
												insert = i;
												break;
										}
								}
								if (insert < 15) {
										stats.inv [insert] = 1;
										resources.woodCount -=20;
										Debug.Log ("You successfully craft a wood spear!");
								}
				else
					Debug.Log("Your inventory is full!");
						} else
								Debug.Log ("You need 20 wood to craft that!");
				}
		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 100), "Stone Spear\n40 Stone")) {
			if (resources.stoneCount >= 40) {
				int insert = 15;
				for (int i = 0; i < 15; i++) {
					if (stats.inv [i] == 0) {
						insert = i;
						break;
					}
				}
				if (insert < 15) {
					stats.inv [insert] = 2;
					resources.woodCount -=40;
					Debug.Log ("You successfully craft a stone spear!");
				}
				else
					Debug.Log("Your inventory is full!");
			} else
				Debug.Log ("You need 40 stone to craft that!");
		}
		//Add crafting logic here
	}

	void BuildingEvents(){
		GUI.Label (new Rect(Screen.width/2 - 50, 0, 100, 50), "BUILDING");
		if (GUI.Button (new Rect(Screen.width - 50, 0, 50, 50), "X")){
			pauseMenuState = 0;
		}
		if (GUI.Button (new Rect(Screen.width/2 - 50, 25, 100, 75), "Healing Home\n\n25 Wood")) {
			if (resources.woodCount >= WoodenHouseScript.cost) {
				resources.woodCount -= WoodenHouseScript.cost;
				Vector3 v = new Vector3(this.transform.localPosition.x,
				                        this.transform.localPosition.y,
				                        0);
				GameObject newWoodenHouse = Instantiate (woodenHouse, v, new Quaternion(0, 0, 0, 0)) as GameObject;
			}
			
		}
		if (GUI.Button (new Rect(Screen.width/2 - 75, 125, 150, 75), "Resource Generator \n\n50 Wood\n50 Stone")) {
			if (resources.woodCount >= ResourceHouseScript.cost && resources.stoneCount >= ResourceHouseScript.cost) {
				resources.woodCount -= ResourceHouseScript.cost;
				resources.stoneCount -= ResourceHouseScript.cost;
				Vector3 v = new Vector3(this.transform.localPosition.x,
				                        this.transform.localPosition.y,
				                        0);
				GameObject newResourceHouse = Instantiate (resourceHouse, v, new Quaternion(0, 0, 0, 0)) as GameObject;
			}
			
		}
		
		if (GUI.Button (new Rect(Screen.width/2 - 75, 225, 150, 75), "Attack Tower \n\n25 Wood\n25 Stone \n25 Iron")) {
			if (resources.woodCount >= TowerScript.cost && resources.stoneCount >= TowerScript.cost
			    && resources.ironCount >= TowerScript.cost) {
				resources.woodCount -= TowerScript.cost;
				resources.stoneCount -= TowerScript.cost;
				resources.ironCount -= TowerScript.cost;
				Vector3 v = new Vector3(this.transform.localPosition.x,
				                        this.transform.localPosition.y,
				                        0);
				GameObject newTower = Instantiate (tower, v, new Quaternion(0, 0, 0, 0)) as GameObject;
			}
			
		}
	}

	void OnDestroy(){
	}
}
