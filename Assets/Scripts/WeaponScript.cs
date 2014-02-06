using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	// Use this for initialization

	private Transform Player;

	void Start () {
		Destroy (gameObject, .12F);
		Player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//rotate the sword?
		transform.RotateAround(Player.position, new Vector3(0, 0, 1), 16F);
	}
}
