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
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameController").GetComponent<GameFlowController>();
		currLid = closeLid;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width - 220, Screen.height - 220, 200, 200), currLid)) {
			if(buttonReady == false){
				buttonReady = true;
				currLid = openLid;
			}
			else{
				Debug.Log ("BRB");
				gc.NewApocalypse ();
				buttonReady = false;
				currLid = closeLid;
			}
		}
	}
}
