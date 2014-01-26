using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			PlayerPrefs.DeleteKey("Talent0");
			PlayerPrefs.DeleteKey("Talent1");
			PlayerPrefs.DeleteKey("Talent2");
			PlayerPrefs.DeleteKey("Talent3");
			PlayerPrefs.DeleteKey("Talent4");
			PlayerPrefs.DeleteKey("Talent5");
			PlayerPrefs.DeleteKey("Talent6");
			PlayerPrefs.DeleteKey("Talent7");
			PlayerPrefs.DeleteKey("Talent8");
			PlayerPrefs.DeleteKey("Talent9");
			PlayerPrefs.DeleteKey("Talent10");
			PlayerPrefs.DeleteKey("Talent11");
			PlayerPrefs.DeleteKey("Talent12");
		}
	}

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width * 3 / 8, Screen.height * 3 / 8, Screen.width * 1 / 4, Screen.height * 1 / 4), "Play")) {
			Application.LoadLevel("Scene1");
		}
	}
}
