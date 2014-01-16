using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	GameObject Player;
	private PlayerResources resources;
	float health = 88;
	//GUI
	void OnGUI(){
		// Make a background box
		Player = GameObject.Find ("Player");
		resources = Player.GetComponent<PlayerResources>();
		GUI.Box (new Rect (10, 10, 100, 165), "");
		GUI.Box (new Rect (20, 20, 80, 25), "Health: " + resources.healthCount.ToString ());
		GUI.Box (new Rect (20, 50, 80, 25), "Wood: " + resources.woodCount.ToString ());
		GUI.Box (new Rect (20, 80, 80, 25), "Stone: " + resources.stoneCount.ToString ());
		GUI.Box (new Rect (20, 110, 80, 25), "Iron: " + resources.ironCount.ToString ());
		GUI.Box (new Rect (20, 140, 80, 25), "Fame: " + resources.fameCount.ToString ());

	}
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
