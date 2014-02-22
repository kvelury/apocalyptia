using UnityEngine;
using System.Collections;
using System;

public class PlayerMovementScript : MonoBehaviour {
	
	public PlayerResources pr;
	private PlayerStats stats;
	public GameObject Weapon;
	public GameObject Weapon2;
	public GameObject DodgeParticle;
	private GameObject swingInstance;
	public AudioClip slash;
	public PolygonGenerator terrain;
	private SpriteRenderer spriteRender;

	protected Animator animator; // for sprite animation
	
	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public float velocity = 20;
	
	private float weaponCoolTimer = 15;
	/// <summary>
	/// The weapon cool down, in frames.
	/// </summary>
	public static float weaponCoolDown = 30;
	
	/// <summary>
	/// Checking whether the character is in the process of dodging
	/// </summary>
	public bool isDodging = false;
	public int dodgeCount = 0;
	Vector3 dodgeDirection = new Vector3 (0,0,0);

	private ParticleSystem particleSystem;
	
	public float knockBackTimer;
	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector3 movement;
	
	private float colorChangeTimer;
	private float yellowTimer;
	private bool isRed;
	private bool isGreen;
	private bool isYellow;
	private int lastDir;
	
	// Use this for initialization
	void Start () {

		pr = gameObject.GetComponent<PlayerResources> ();
		spriteRender = GetComponent<SpriteRenderer>();
		stats = GameObject.Find ("Player").GetComponent<PlayerStats> ();
		//		terrain = GameObject.Find ("Terrain").GetComponent<PolygonGenerator>();

		animator = GetComponent<Animator>(); //for sprite animation
		animator.SetTrigger ("snot");
		lastDir = 2;
		knockBackTimer = 11;
		colorChangeTimer = 0;
		yellowTimer = 0;
		isRed = false;
		isYellow = false;
		isGreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		//magic
		if (Input.GetKeyDown ("w")) {
			animator.SetTrigger ("wpressed");
		}
		if(Input.GetKeyUp("w")){
			animator.SetTrigger ("snot");
		}
		//
		if (Input.GetKeyDown ("s")) {
			animator.SetTrigger ("spressed");
		}
		if(Input.GetKeyUp("s")){
			animator.SetTrigger ("snot");
		}
		//
		if (Input.GetKeyDown ("a")) {
			animator.SetTrigger ("apressed");
		}
		if(Input.GetKeyUp("a")){
			animator.SetTrigger ("snot");
		}
		//
		if (Input.GetKeyDown ("d")) {
			animator.SetTrigger ("dpressed");
		}
		if(Input.GetKeyUp("d")){
			animator.SetTrigger ("snot");
		}
*/




		if (knockBackTimer > 30) {
			
			if (Input.GetKey ("space") && dodgeCount > 50) isDodging = true;
			
			Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);

			//Debug.Log ((Quaternion.Euler (0, 0, -45) * (velocity * direction)).ToString ());
			movement = Quaternion.Euler (0, 0, -45) * (velocity * direction);
			
			//magic
			//lastDir
			//1: up
			//2: down
			//3: left
			//4: right
			if (direction.y > 0) {
				animator.Play ("WalkUp");
				lastDir = 1;
			}
			else if (direction.y < 0) {
				animator.Play ("WalkDown");
				lastDir = 2;
			}
			else if (direction.x < 0) {
				animator.Play ("WalkLeft");
				lastDir = 3;
			}
			else if (direction.x > 0) {
				animator.Play ("WalkRight");
				lastDir = 4;
			}
			if(direction.y == 0 && direction.x == 0){
				switch(lastDir){
					case 1:
						animator.Play ("StandUp");
						break;
					case 2:
						animator.Play ("StandDown");
						break;
					case 3:
						animator.Play ("StandLeft");
						break;
					case 4:
						animator.Play ("StandRight");
						break;
				}

			}
		} else {
			knockBackTimer ++;
		}
		
		if (isRed) {
			colorChangeTimer--;
			if (colorChangeTimer <= 0) {
				isRed = false;
			}
		} else if (isYellow) {
			yellowTimer--;
			if (yellowTimer <= 0) {
				isYellow = false;
			}
		} else if (isGreen) {
			spriteRender.color = new Color (.2f, 1.0f, 0f);
		} else {
			spriteRender.color = Color.white;
		}
		
		//		if (Input.GetKey (KeyCode.Space)) {
		//			//Debug.Log("Precision Guns: " + stats.precisionGuns.ToString());
		//		}
		
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemy") {
			Vector3 direction = (transform.position - col.gameObject.transform.position).normalized;
			movement = Quaternion.Euler (0,0,0/*-45*/) * (2.5F * velocity*direction);
			knockBackTimer = 20;
			ChangeToRed();
		} else if (col.gameObject.tag == "DamagingHazard"){
			GetComponent<PlayerResources>().healthCount -= 10;
			ChangeToRed();
		}
	}
	
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Water") {
			velocity = 0.05f + stats.currSpeed;
		}

		if(col.gameObject.name == "Shield(Clone)"){
			FlashYellow();
		}
	}
	
	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){

		if (Input.GetKeyDown ("q") && pr.useItemTimer == 300) {
			pr.UseItem ();
		}
		if (swingInstance != null) {
			swingInstance.transform.position += movement;
		} 
		if (isDodging == true && dodgeCount > 50){
			dodgeDirection = movement*5;
			isDodging = false;
			dodgeCount = 0;
		}
		if (dodgeCount < 7){
			transform.position += dodgeDirection;
			particleSystem = Instantiate(DodgeParticle, transform.position, Quaternion.LookRotation(transform.position)) as ParticleSystem;
		}
		if (dodgeCount < 25) {
			dodgeCount++;
		}
		else if (dodgeCount < 51) {
			transform.position += movement;
			dodgeCount++;
		}
		else{
			transform.position += movement;
		}
		
		if(Input.GetMouseButton (0) && weaponCoolTimer >= weaponCoolDown && 
		   //This added check is so the player doesn't swing when clicking Big Red Button.
		   !(Input.mousePosition.x > (Screen.width - 220) && Input.mousePosition.x < (Screen.width - 20)
		  && Input.mousePosition.y > (20) && Input.mousePosition.y < (220))){
			
			Vector3 swingDir = new Vector3(transform.position.x, transform.position.y, 0);
			Quaternion swingRot = new Quaternion(0,0,0,0);
			audio.clip = slash;
			audio.PlayOneShot(audio.clip);
			
			Vector3 diff = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2, 0);
			diff.Normalize();
			diff = Quaternion.Euler (0, 0, -45) * diff;
			swingDir = transform.position + diff;
			
			if (Input.mousePosition.x > Screen.width/2 + 16){
				swingDir.y = swingDir.y - 0.1F;
			}
			if (Input.mousePosition.x < Screen.width/2 - 16){
				swingDir.y = swingDir.y + 0.1F;
			}
			if (Input.mousePosition.y > Screen.height/2 + 16){
				swingDir.x = swingDir.x + 0.1F;
			}
			if (Input.mousePosition.y < Screen.height/2 - 16){
				swingDir.x = swingDir.x - 0.1F;
			}
			if (Input.mousePosition.x > Screen.width/2 - 16 && Input.mousePosition.x < Screen.width/2 + 16
			    && Input.mousePosition.y > Screen.height/2 - 16 && Input.mousePosition.y < Screen.height/2 + 16){
				swingDir.y = swingDir.y + 0.1F;
			}
			
			swingRot = Quaternion.LookRotation (swingDir - transform.position);
			swingRot.z = swingRot.w = 0;
			if(swingDir.x == transform.position.x){
				swingRot.y = swingRot.x;
			}
			swingRot *= Quaternion.Euler (0, 0, 90);				
			//we need account for the isometric rotation
			swingInstance = Instantiate (Weapon, swingDir, swingRot) as GameObject;
			weaponCoolTimer = 0;
		} else {
			weaponCoolTimer++;
		}
		
		if(Input.GetMouseButton (1) && weaponCoolTimer >= weaponCoolDown && 
		   //This added check is so the player doesn't swing when clicking Big Red Button.
		   !(Input.mousePosition.x > (Screen.width - 220) && Input.mousePosition.x < (Screen.width - 20)
		  && Input.mousePosition.y > (20) && Input.mousePosition.y < (220))){
			
			Vector3 swingDir = new Vector3(transform.position.x, transform.position.y, 0);
			Quaternion swingRot = new Quaternion(0,0,0,0);
			audio.clip = slash;
			audio.PlayOneShot(audio.clip);
			if (Input.mousePosition.x > Screen.width/2 + 16){
				swingDir.y = transform.position.y - 0.70F;
			}
			if (Input.mousePosition.x < Screen.width/2 - 16){
				swingDir.y = transform.position.y + 0.70F;
			}
			if (Input.mousePosition.y > Screen.height/2 + 16){
				swingDir.x = transform.position.x + 0.70F;
			}
			if (Input.mousePosition.y < Screen.height/2 - 16){
				swingDir.x = transform.position.x - 0.70F;
			}
			if (Input.mousePosition.x > Screen.width/2 - 16 && Input.mousePosition.x < Screen.width/2 + 16
			    && Input.mousePosition.y > Screen.height/2 - 16 && Input.mousePosition.y < Screen.height/2 + 16){
				swingDir.y = transform.position.y + 0.70F;
			}
			swingRot = Quaternion.LookRotation (swingDir - transform.position);
			swingRot.z = swingRot.w = 0;
			if(swingDir.x == transform.position.x){
				swingRot.y = swingRot.x;
			}
			swingRot *= Quaternion.Euler (0, 0, 90);				
			//we need account for the isometric rotation
			swingInstance = Instantiate (Weapon2, swingDir, swingRot) as GameObject;
			weaponCoolTimer = 0;
		} else {
			weaponCoolTimer++;
		}
		
		if (knockBackTimer > 30) {
			try{
				int x = Mathf.CeilToInt (transform.position.x / PolygonGenerator.worldScale - 1);
				int y = Mathf.CeilToInt (transform.position.y / PolygonGenerator.worldScale);
				if (terrain.blocks [x, y] == (byte)PolygonGenerator.TileCodes.Water) {
					velocity = 0.05f + stats.currSpeed;
				} else {
					velocity = 0.1f + stats.currSpeed;
				}
				if(terrain.blocks[x, y] == (byte)PolygonGenerator.TileCodes.Radiation){
					pr.healthCount -= 0.05f;
					isGreen = true;
				} else {
					isGreen = false;
				}
				if(terrain.blocks[x, y] == (byte)PolygonGenerator.TileCodes.Lava){
					pr.healthCount -= 0.5f;
				}
			}catch (IndexOutOfRangeException e){//player has gone through the smoke
				pr.healthCount -= 9999999999;
			}
		}
	}
	
	private void ChangeToRed(){
		isRed = true;
		colorChangeTimer = 5;
		spriteRender.color = Color.red;
	}

	private void FlashYellow() {
		isYellow = true;
		yellowTimer = 75;
		spriteRender.color = Color.yellow;
	}
}
