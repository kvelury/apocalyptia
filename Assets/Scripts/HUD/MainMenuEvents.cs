using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour {

	public Texture Button;
	public Texture SideA;
	public Texture SideB;

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
		GUI.DrawTexture(new Rect(0, 0, SideA.width, Screen.height), SideA);
		GUI.DrawTexture (new Rect(Screen.width-SideB.width, 0, SideB.width, Screen.height), SideB);

		if (GUI.Button (new Rect((Screen.width-Button.width) * 1/2, (Screen.height-Button.height) * 1/2, Button.width, Button.height), Button)) {
			Application.LoadLevel("Scene1");
		}
		//GUI.TextField(new Rect(Screen.width * 7/16, Screen.height * 7/16, Screen.width * 1/8, Screen.height * 1/8), "Press to end\nthe world");
	}
}
