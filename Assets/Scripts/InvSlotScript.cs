using UnityEngine;
using System.Collections;

public class InvSlotScript : MonoBehaviour {

	public GameObject Player;
	public PlayerStats stats;

	public bool weapon = false;
	public bool shield = false;
	public bool boots = false;
	public bool oneUse = false;

	// Use this for initialization
	void Start () {

		Player = GameObject.Find ("Player");
		stats = Player.GetComponent<PlayerStats> ();

	}
	
	// Update is called once per frame
	void Update () {
		//weapon picked up
		if (stats.inv[0] == 1) {
			weapon = true;
			//Debug.Log ("Wep");
		}
		//shield picked up
		if (stats.inv[1] == 1) {
			shield = true;
			//Debug.Log ("shield");
		}
		//boots picked up
		if (stats.inv[2] == 1) {
			boots = true;
			//Debug.Log ("boots");
		}
		//one-use item picked up
		if (stats.inv[16] == 1) {
			oneUse = true;
			//Debug.Log ("boots");
		}
		else{
			oneUse = false;
		}

	}
}
