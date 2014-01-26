using UnityEngine;
using System.Collections;

public class GameplayEvents : MonoBehaviour {

	GameObject Player;

	private PlayerResources resources;

	private bool isPaused;


	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//GUI
	void OnGUI(){
		if (isPaused){
			
		}else{
			// Make a background box
			Player = GameObject.Find ("Player");
			resources = Player.GetComponent<PlayerResources>();
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

	void OnDestroy(){
	}
}
