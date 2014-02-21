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
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameFlowController>();
		currLid = closeLid;
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
				gc.NewApocalypse ();
				buttonReady = false;
				currLid = closeLid;
			}
		}
	}
}
