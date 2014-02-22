using UnityEngine;
using System.Collections;

public class BigRedButtonScript : MonoBehaviour {
	GameFlowController gc;
	//buttonStep:
	//false: closed lid
	//true: ready to push
	public bool buttonReady;
	public Texture closeLid;
	public Texture openLid;
	public Texture currLid;
	public AudioClip flipOpen;
	public AudioClip click;
	public ParticleSystem thunder;
	public GameObject burnTile;
	public int worldJump;
	public bool isJumping;
	private Transform player;
	private GameObject playerRender;
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameFlowController>();
		player = GameObject.Find("Player").transform;
		playerRender = GameObject.Find("Player");
		currLid = closeLid;
		worldJump = 0;
		isJumping = false;
		ParticleSystem[] systems = GameObject.Find("Player").GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem ps in systems){
			if (ps.tag == "Lightning"){
				thunder = ps;
			}
		}
		thunder.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isJumping){
			worldJump++;
		}
		if (worldJump >= 9 && worldJump <10){
			thunder.enableEmission = false;
		}
		if (worldJump >= 33 && worldJump < 34){
			GameObject burnInstance = Instantiate (burnTile, player.transform.position + new Vector3(0,0,0.27f), new Quaternion (0, 0, 0, 0)) as GameObject;
			playerRender.renderer.enabled = false;
		}
		if (worldJump >= 50 && worldJump < 51){
			Time.timeScale = 0.0f;
			Camera.main.SendMessage ("fadeOut");
		}
		if (worldJump >= 110 && worldJump <111){
			gc.NewApocalypse ();
			thunder.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y+1.5f, player.transform.position.z+10f);
			thunder.transform.rotation = Quaternion.Euler ((5 * Mathf.Deg2Rad), (180 * Mathf.Deg2Rad), 0);	
			Camera.main.SendMessage("fadeIn");
			isJumping = false;
			worldJump = 0;
			playerRender.renderer.enabled = true;
			buttonReady = false;
			currLid = closeLid;
			Time.timeScale = 1.0f;
		}
	}
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width - 220, Screen.height - 220, 200, 200), currLid)) {
			if(buttonReady == false){
				buttonReady = true;
				audio.clip = flipOpen;
				audio.PlayOneShot (flipOpen);
				currLid = openLid;
			}
			else{
				Debug.Log ("BRB");
				audio.clip = click;
				audio.PlayOneShot(click);
				thunder.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z-10);
				thunder.transform.rotation = Quaternion.Euler ((17 * Mathf.Deg2Rad), 0, 0);	
				thunder.enableEmission = true;
				isJumping = true;
			}
		}
	}
}
