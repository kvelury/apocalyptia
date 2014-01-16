using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	//GUI
	void OnGUI(){
		// Make a background box
		if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 100,200,200),"You Are Dead\n Try Again?"))
			Application.LoadLevel ("Scene1");
	}
	
	// Use this for initialization
	void Start () {
		//onGUI ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
