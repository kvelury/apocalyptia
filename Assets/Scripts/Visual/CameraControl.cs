using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

	/// <summary>
	/// The velocity of the camera's free movement
	/// </summary>
	public float velocity = 10;
	public float timer;
	public PolygonGenerator pg;
	/// <summary>
	/// The size of the screen buffer, within which the mouse will cause the camera to move.
	/// </summary>
	public float screenBufferSize = 25;

	public float difficultyTimer = 3600;
	public float currTime;

	/// <summary>
	/// The target that the camera follows.
	/// </summary>
	public Transform target;

	/// <summary>
	/// followDistance determines the camera's position relative to the target
	/// </summary>
	public Vector3 followDistance = new Vector3 (10, 0, 10);

	/// <summary>
	/// A boolean to represent the camera's movement mode.
	/// True represents the camera is attached to the player.
	/// False represents free-roaming camera.
	/// </summary>
	private bool followingTarget = true;
	private bool playAudio;
	private int boolZoom;
	public AudioClip rumble;
	private Vector3 prevPosition;

	void Start(){
		currTime = 0;
		playAudio = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (currTime > difficultyTimer){
			followingTarget = false;
			boolZoom = 1;
			prevPosition = target.position - followDistance;
			currTime = 0;
		}

		if (Input.GetKey (KeyCode.C)) {
			followingTarget = false;
			boolZoom = 1;
			prevPosition = target.position - followDistance;
		} else {
			//followingTarget = true;
		}
		if (boolZoom == 0) {
			transform.position = target.position - followDistance;
			transform.LookAt (target, new Vector3(0, 0, -1));
		} else {/*
			//Debug.Log ("currently free roaming");
			if (Input.mousePosition.x > Screen.width - screenBufferSize) {
				transform.position += Quaternion.Euler(0, 0, -45) * (velocity * new Vector3(1, 0, 0));
				//Debug.Log ("Mouse Right");
			}
			if (Input.mousePosition.x < screenBufferSize) {
				transform.position += Quaternion.Euler(0, 0, -45) * (velocity * new Vector3(-1, 0, 0));
				//Debug.Log ("Mouse Left");
			}
			if (Input.mousePosition.y > Screen.height - screenBufferSize) {
				transform.position += Quaternion.Euler(0, 0, -45) * (velocity * new Vector3(0, 1, 0));
				//Debug.Log ("Mouse Up");
			}
			if (Input.mousePosition.y < screenBufferSize) {
				transform.position += Quaternion.Euler(0, 0, -45) * (velocity * new Vector3(0, -1, 0));
				//Debug.Log ("Mouse Down");
			}*/
			Time.timeScale = 0.0f;
			if(boolZoom == 1){
				Vector3 dir = (transform.position - target.position).normalized;
				dir.x = 0;
				dir.y = 0.05f;
				
				if(playAudio){
					audio.clip = rumble;
					audio.PlayOneShot (rumble);
					playAudio = false;
				}
				if(Vector3.Distance (transform.position, target.position) < 20){
					transform.Translate (dir * .1f);
				}

				else
					boolZoom = 2;
			}
			else if(boolZoom == 2){
				Camera.main.SendMessage ("fadeOut");
				timer++;
				if(timer > 60)
					boolZoom = 3;
			}
			else if(boolZoom == 3){
				//diff increase
				pg = GameObject.Find ("GameController").GetComponent<GameFlowController>().currentApocalypse.GetComponent<PolygonGenerator>();
				pg.IncreaseDifficulty ();
				Debug.Log ("New Difficulty: " + pg.difficulty);
				boolZoom = 4;
			}
			else if(boolZoom == 4){
				//screen shake?
				//sound effect
				Camera.main.SendMessage("fadeIn");
				timer++;
				if(timer > 120)
					boolZoom = 5;
			}
			else if(boolZoom == 5){
				Vector3 dir = -(transform.position - prevPosition).normalized;
				if(Vector3.Distance (transform.position, prevPosition) > 0.2f){
					//Debug.Log ("boop");
					transform.Translate (dir * .1f);// * Time.deltaTime);
				}
				else {
					timer = 0;
					boolZoom = 0;
					//Debug.Log ("Liiiive!");
					Time.timeScale = 1.0f;
					
					playAudio = true;
				}
			}

			//Debug.Log("beep");
		}
	}

	void FixedUpdate(){
		currTime++;
	}
}
