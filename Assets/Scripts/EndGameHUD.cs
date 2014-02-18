using UnityEngine;
using System.Collections;

public class EndGameHUD : MonoBehaviour {
	public bool display;
	public GameObject endgameobj;
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (display) {
			GUI.Box(new Rect(Screen.width/2 - 60, 20, 120, 25), (60 - endgameobj.GetComponent<EndGameScript>().timer).ToString ());
			GUI.Box(new Rect(Screen.width/2 - 120, 65, 240, 25), "Distance to target: " + Mathf.Round(Vector3.Distance (endgameobj.transform.position, player.transform.position)).ToString ());
		}
	}

}
