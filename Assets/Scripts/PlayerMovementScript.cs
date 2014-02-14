using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
	private PlayerStats stats;
	public AudioClip slash;

	/// <summary>
	/// The velocity vector holds the player's movement speed.
	/// This gets multiplied by the direction to get actual movement.
	/// </summary>
	public float velocity = 10;

	public GameObject Weapon;
	public PolygonGenerator terrain;

	private float weaponCoolTimer = 15;
	public PlayerResources pr;
	/// <summary>
	/// The weapon cool down, in frames.
	/// </summary>
	public static float weaponCoolDown = 10;

	/// <summary>
	/// Checking whether the character is in the process of dodging
	/// </summary>
	public bool isDodging = false;
	public int dodgeCount = 0;
	Vector3 dodgeDirection = new Vector3 (0,0,0);

	public float knockBackTimer;
	/// <summary>
	/// The movement vector is given to the rigidbody physics to move the object
	/// </summary>
	private Vector3 movement;

	private GameObject swingInstance;

	// Use this for initialization
	void Start () {
		pr = gameObject.GetComponent<PlayerResources> ();
		knockBackTimer = 11;
		stats = GameObject.Find ("Player").GetComponent<PlayerStats> ();
//		terrain = GameObject.Find ("Terrain").GetComponent<PolygonGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (knockBackTimer > 30) {

			if (Input.GetKey ("space") && dodgeCount > 50) isDodging = true;

				Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);

						//Debug.Log ((Quaternion.Euler (0, 0, -45) * (velocity * direction)).ToString ());
						movement = Quaternion.Euler (0, 0, -45) * (velocity * direction);
				} else {
						knockBackTimer ++;

				}
	


//		if (Input.GetKey (KeyCode.Space)) {
//			//Debug.Log("Precision Guns: " + stats.precisionGuns.ToString());
//		}

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemy") {
			Vector3 direction = (transform.position - col.gameObject.transform.position).normalized;
			movement = Quaternion.Euler (0,0,-45) * (0.5F * velocity*direction);
			knockBackTimer = 0;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Water") {
			velocity = 0.05f + stats.currSpeed;
				}
		}

	//FixedUpdate is called once per tick and should be used for physics
	void FixedUpdate(){
		if (Input.GetMouseButton (1) && pr.useItemTimer == 300) {
			pr.UseItem ();
		}
		if (swingInstance != null) {
						swingInstance.transform.position += movement;
				}
		else {
			if (isDodging == true && dodgeCount > 50){
				dodgeDirection = movement*5;
				isDodging = false;
				dodgeCount = 0;
			}
			if (dodgeCount < 7){
				transform.position += dodgeDirection;
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
		}

		if(Input.GetMouseButton (0) && weaponCoolTimer >= weaponCoolDown){
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
			swingInstance = Instantiate (Weapon, swingDir, swingRot) as GameObject;
			weaponCoolTimer = 0;
/*			//stats.InvCheck ();
			GameObject.Destroy (GameObject.Find ("Weapon(Clone)"));
			Vector3 swingDir = new Vector3(transform.position.x,transform.position.y, 0);
			Quaternion swingRot = new Quaternion(0,0,0,0);
			//Controls for combat. Will change how to enter this block
			//when HUD is fleshed out so player does not swing while
			//clicking on menu buttons.
			if(Input.mousePosition.x > Screen.width/2 + 16 && Input.mousePosition.y < Screen.height/2 - 16)
				swingDir.y = transform.position.y - 0.70F;
			else if(Input.mousePosition.x < Screen.width/2 - 16 && Input.mousePosition.y < Screen.height/2 - 16)
				swingDir.x = transform.position.x - 0.70F;
			else if(Input.mousePosition.x < Screen.width/2 - 16 && Input.mousePosition.y > Screen.height/2 + 16)
				swingDir.y = transform.position.y + 0.70F;
			else if(Input.mousePosition.x > Screen.width/2 + 16 && Input.mousePosition.y > Screen.height/2 + 16)
				swingDir.x = transform.position.x + 0.70F;
			else if(Input.mousePosition.x > Screen.width/2 + 16){
				swingDir.x = transform.position.x + 0.55F;
				swingDir.y = transform.position.y - 0.55F;
			}
			else if(Input.mousePosition.x < Screen.width/2 - 16){
				swingDir.x = transform.position.x - 0.55F;
				swingDir.y = transform.position.y + 0.55F;
			}
			else if(Input.mousePosition.y > Screen.height/2 + 16){
				swingDir.x = transform.position.x + 0.55F;
				swingDir.y = transform.position.y + 0.55F;
			}
			else if(Input.mousePosition.y < Screen.height/2 - 16){
				swingDir.x = transform.position.x - 0.55F;
				swingDir.y = transform.position.y - 0.55F;
			}
			swingRot = Quaternion.LookRotation (swingDir - transform.position);
			swingRot.z = swingRot.w = 0;
			if(swingDir.x == transform.position.x){
				swingRot.y = swingRot.x;
			}
			if(swingDir.x != transform.position.x || swingDir.y != transform.position.y){
				
				//we need account for the isometric rotation
				GameObject swingInstance = Instantiate (Weapon,swingDir, swingRot) as GameObject;
			} */
		} else {
			weaponCoolTimer++;
		}
		if (knockBackTimer > 30) {
						if (terrain.blocks [Mathf.CeilToInt (transform.position.x / PolygonGenerator.worldScale - 1), 
		                      Mathf.CeilToInt (transform.position.y / PolygonGenerator.worldScale)] 
								== (byte)PolygonGenerator.TileCodes.Water) {
								velocity = 0.05f + stats.currSpeed;
						} else {
								velocity = 0.1f + stats.currSpeed;
						}
				}
	}
}
