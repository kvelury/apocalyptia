using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	// Use this for initialization

	private Transform Player;
	private PlayerStats ps;

	void Start () {
		Destroy (gameObject, .12F);
		Player = GameObject.Find("Player").transform;
		ps = GameObject.Find ("Player").GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		//rotate the sword?
		transform.RotateAround(Player.position, new Vector3(0, 0, 0), 16F);
	}
}
