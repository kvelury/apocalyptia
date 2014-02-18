using UnityEngine;
using System.Collections;

public class RockfallScript : HazardScript {

	private ParticleSystem particleSystem;
	private SpriteRenderer spriteRender;

	/// <summary>
	/// The state of the rockfall.
	/// 0 - shadow
	/// 1 - falling
	/// 2 - particles
	/// </summary>
	private int rockfallState;
	private Vector3 gravity = new Vector3(0, 0, .245f);
	private Vector3 velocity;

	private int timeElapsed;
	public int shadowStageLifetime = 60;

	// Use this for initialization
	void Start () {
		//this returns the sibling particle system
		particleSystem = GetComponent<ParticleSystem>();
		spriteRender = GetComponent<SpriteRenderer>();
		particleSystem.Pause();
		spriteRender.enabled = false;
		rockfallState = 0;
		timeElapsed = 0;
		velocity = new Vector3(0, 0, 0);
		tag = "DamagingHazard";

		//run initializations for the base class
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Updating Rockfall " + rockfallState.ToString ());
		switch (rockfallState){
		case 0:
			shadowUpdate ();
			break;
		case 1:
			fallingUpdate ();
			break;
		case 2:
			particleUpdate();
			break;
		default:
			break;
		}
	}

	private void shadowUpdate(){
		//make the shadow grow
		timeElapsed++;
		if (timeElapsed > shadowStageLifetime){
			rockfallState = 1;
			spriteRender.enabled = true;
		}
	}

	private void fallingUpdate(){
		//??
		if (transform.position.z > 0){
			particleSystem.Play();
			rockfallState = 2;
			spriteRender.enabled = false;
		}
	}

	private void particleUpdate(){
		//Debug.Log("Playing? " + particleSystem.isPlaying);
		if (!particleSystem.isPlaying){
			isAlive = false;
		}
	}

	void FixedUpdate(){
		if (rockfallState == 1){
			velocity += gravity;
			transform.position += velocity;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		collider2D.enabled = false;
	}
}
