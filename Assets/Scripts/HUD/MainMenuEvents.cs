using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			Debug.Log("Talents Reset");
			for (int i=0; i<20; i++){
				PlayerPrefs.DeleteKey("Talent" + i.ToString());
			}
			PlayerPrefs.DeleteKey("Completion");
		}
	}

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width * 3 / 8, Screen.height * 3 / 8, Screen.width * 1 / 4, Screen.height * 1 / 4), "Play")) {
			Application.LoadLevel("Scene1");
		}
	}
}
