using UnityEngine;
using System.Collections;

public class WeaponScript2 : MonoBehaviour {
	// Use this for initialization
	
	private Transform Player;
	private PlayerStats ps;
	public Sprite weapon1;
	public Sprite weapon2;
	
	void Start () {
		Destroy (gameObject, .12F);
		Player = GameObject.Find("Player").transform;
		ps = GameObject.Find ("Player").GetComponent<PlayerStats> ();
		if (ps.inv [0] == 0)
			GetComponent<SpriteRenderer>().sprite = weapon1;
		else
			GetComponent<SpriteRenderer>().sprite = weapon2;
	}
	
	// Update is called once per frame
	void Update () {
		//rotate the sword?
		transform.RotateAround(Player.position, new Vector3(0, 0, 1), 16F);
	}
}
