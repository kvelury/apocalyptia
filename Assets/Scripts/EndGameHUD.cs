using UnityEngine;
using System.Collections;

public class EndGameHUD : MonoBehaviour {
	public bool display;
	public GameObject endgameobj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (display) {
			GUI.Box(new Rect(Screen.width/2 - 60, 20, 100, 25), (60 - endgameobj.GetComponent<EndGameScript>().timer).ToString ());
		}
	}

}
