using UnityEngine;
using System.Collections;

public class InvSlotScript : MonoBehaviour {
	
	public GameObject Player;
	public PlayerStats stats;
	
	public bool weapon = false;
	public bool shield = false;
	public bool boots = false;
	public bool oneUse = false;
	public bool leech = false;
	public bool health = false;
	
	private GameObject WepLabel;
	private GameObject ShieldLabel;
	private GameObject BootLabel;
	private GameObject OneUseLabel;
	private GameObject LeechLabel;
	
	// Use this for initialization
	void Start () {
		
		Player = GameObject.Find ("Player");
		stats = Player.GetComponent<PlayerStats> ();
		
		WepLabel = GameObject.Find ("WepLabel");
		ShieldLabel = GameObject.Find ("ShieldLabel");
		BootLabel = GameObject.Find ("BootLabel");
		OneUseLabel = GameObject.Find ("OneUseLabel");
		LeechLabel = GameObject.Find ("HealthLeechLabel");
	}
	
	// Update is called once per frame
	void Update () {
		
		//weapon picked up
		if (stats.inv[0] == 1) {
			weapon = true;
			Destroy (WepLabel, 1.0F);
			//if (ShieldLabel != null) Destroy (ShieldLabel);
			//if (BootLabel != null) Destroy (BootLabel);
			//if (OneUseLabel != null) Destroy (OneUseLabel);
		}
		//shield picked up
		if (stats.inv[1] == 1) {
			shield = true;
			Destroy (ShieldLabel, 1.0F);
			//if (WepLabel != null) Destroy (WepLabel);
			//if (BootLabel != null) Destroy (BootLabel);
			//if (OneUseLabel != null) Destroy (OneUseLabel);
		}
		//boots picked up
		if (stats.inv[2] == 1) {
			boots = true;
			Destroy (BootLabel, 1.0F);
			//if (ShieldLabel != null) Destroy (ShieldLabel);
			//if (WepLabel != null) Destroy (WepLabel);
			//if (OneUseLabel != null) Destroy (OneUseLabel);
		}
		if (stats.inv[10] == 1) {
			leech = true;
			Destroy (LeechLabel, 1.5F);
		}
		//one-use item picked up
		if (stats.inv[16] == 1) {
			oneUse = true;
			Destroy (OneUseLabel, 1.0F);
			//if (ShieldLabel != null) Destroy (ShieldLabel);
			//if (BootLabel != null) Destroy (BootLabel);
			//if (WepLabel != null) Destroy (WepLabel);
		}
		else{
			oneUse = false;
		}
		
		
	}
}
