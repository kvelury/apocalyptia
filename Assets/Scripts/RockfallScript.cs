using UnityEngine;
using System.Collections;

public class RockfallScript : HazardScript {

	private ParticleSystem particleSystem;

	/// <summary>
	/// The state of the rockfall.
	/// 0 - shadow
	/// 1 - falling
	/// 2 - particles
	/// </summary>
	private int rockfallState;

	// Use this for initialization
	void Start () {
		//this returns the sibling particle system
		particleSystem = GetComponent<ParticleSystem>();
		//particleSystem.Pause();
		rockfallState = 2;

		//run initializations for the base class
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
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
	}

	private void fallingUpdate(){
		//??
	}

	private void particleUpdate(){
		if (particleSystem.time > particleSystem.startLifetime){
			Debug.Log("Time :" + particleSystem.time.ToString() + "Lifetime: " + particleSystem.startLifetime.ToString());
			isAlive = false;
		}
	}
}
