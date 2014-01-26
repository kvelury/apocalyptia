using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	//We might move Health here.
	//These are just the stat names from the prototype.
	public float precisionGuns;
	public float powerGuns;
	public float sharpWeapon;
	public float bluntWeapon;
	public float charm;
	public float intimidate;
	public float movement;
	public float medical;
	public float crafting;
	public float repairing;
	// Use this for initialization
	void Start () {
		//Change these once we set up persistant stat levels.
		precisionGuns = powerGuns = sharpWeapon = bluntWeapon
			= charm = intimidate = movement = medical = crafting
				= repairing = 20;
		if (PlayerPrefs.GetInt("Talent0") == 1) {
			precisionGuns += 10;
			powerGuns += 10;
			sharpWeapon += 10;
			bluntWeapon += 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
