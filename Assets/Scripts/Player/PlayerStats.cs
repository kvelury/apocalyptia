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
	public float[] inv;
	public float currDam;
	public float currDef;
	public float currSpeed;
	public float bestWep;
	public float bestDef;
	
	public PlayerResources pr;
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
		pr = gameObject.GetComponent<PlayerResources> ();
		//Inv slots 3 -> 9 reserved for Mark.
		//Inv slots 10 -> 16 reserved for Kyle.
		inv = new float[17];
		currDam = 10;
		
		//fill the player's inventory with persistent items
		//Commenting this out for the moment, until we have the remnants of the
		//crafted items removed so as to not confuse the new inventory system.
		//This will likely return for if we have scene changes within one life.
		/*
		for (int i = 0; i<inv.Length; i++){
			inv [i] = PlayerPrefs.GetFloat("Inventory " + i.ToString());
		}*/
		currDef = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug for items
		if (Input.GetKeyDown ("1")) {
			inv [0] = 1;
			InvCheck();
			Debug.Log ("Sword added.");
		}
		if (Input.GetKeyDown ("2")) {
			inv [1] = 1;
			InvCheck ();
			Debug.Log ("Shield added.");
		}
		if (Input.GetKeyDown ("3")) {
			inv [2] = 1;
			InvCheck();
			Debug.Log ("Boots added.");
		}
		if (Input.GetKeyDown ("l")) {
			inv[10] = 1;
			InvCheck ();
			Debug.Log("HealthLeech added.");
		}
		if (Input.GetKeyDown ("m")) {
			inv[11] = 1;
			InvCheck ();
			Debug.Log ("MoreHearts added.");
		}
		if (Input.GetKeyDown ("u")) {
			inv[12] = 1;
			InvCheck ();
			Debug.Log ("OneUp added.");
		}
		if (Input.GetKeyDown ("f")) {
			inv[16] = 1;
			InvCheck ();
			Debug.Log ("ForceField added.");
		}
		
	}
	
	public void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Item") {
			if(col.gameObject.name == "Sword(Clone)"){
				inv[0] = 1;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name == "Shield(Clone)"){
				inv[1] = 1;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name == "Boots(Clone)"){
				inv[2] = 1;
				Destroy (col.gameObject);
			}
			
			else if (col.gameObject.name == "Heart(Clone)") {
				pr.healthCount += 10;
				if(pr.healthCount > 100)
					pr.healthCount = 100;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name == "HealthLeech(Clone)"){
				inv[10] = 1;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name =="MoreHearts(Clone)"){
				inv[11] = 1;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name == "OneUp(Clone)"){
				inv[12] = 1;
				Destroy (col.gameObject);
			}
			else if(col.gameObject.name == "ForceField(Clone)"){
				inv[16] = 1;
				Destroy (col.gameObject);
			}
			InvCheck ();
		}
	}
	
	public void InvCheck() {
		//Inv check: Checks inventory for strongest weapon/defense
		//Inv codes:
		//1 = Wood Spear
		//2 = Stone Spear
		//11 = Wood Shield
		//12 = Stone Shield
		/*
		bestWep = 0;
		for (int i = 0; i < 14; i++) {
			if((inv[i] == 1 || inv[i] == 2) && inv[i] > bestWep)
				bestWep = inv[i];
		}
		if (bestWep == 0)
						currDam = 10;
				else if (bestWep == 1)
						currDam = 20;
				else if (bestWep == 2)
						currDam = 40;
		bestDef = 0;
		for (int i = 0; i < 14; i++) {
			if((inv[i] == 11 || inv[i] == 12) && inv[i] > bestWep)
				bestDef = inv[i];
		}
		if (bestDef == 0)
			currDef = 0;
		else if (bestDef == 11)
			currDef = 5;
		else if (bestDef == 12)
			currDef = 15;
	}*/
		
		//Inventory works differently now. Slots correspond to whether or not the player has
		//one of that slot's items. 0 = does not, 1 = has.
		//Slots:
		//0 = Sword, + 10 damage
		//1 = Shield, + 10 damage resist
		//2 = Boots, + 0.05 speed
		//3 - 9 reserved for Mark
		//10 = Health Leech
		//11 = More Hearts dropped
		//12 = One-Up
		//13 =
		//14 =
		//15 =
		//16 -> Reserved for Use Items. Use item codes are are as follows:
		//0 = empty
		//1 = Forcefield.
		if (inv [0] == 1)
			currDam = 20;
		else
			currDam = 10;
		if (inv [1] == 1)
			currDef = 5;
		else
			currDef = 0;
		if (inv [2] == 1)
			currSpeed = 0.02f;
		else
			currSpeed = 0;
	}
	
}
