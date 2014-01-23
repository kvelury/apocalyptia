using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width * 3 / 8, Screen.height * 3 / 8, Screen.width * 1 / 4, Screen.height * 1 / 4), "Play")) {
			Application.LoadLevel("Scene1");
		}
	}
}
