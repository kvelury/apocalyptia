using UnityEngine;
using System.Collections;

public class EndGameHUD : MonoBehaviour {
	public bool display;
	public GameObject endgameobj;
	public GameObject player;
	public GameObject enemySpawner;
	public AudioClip beep;
	public bool first;
	private bool onetoggle;
	public float alpha;
	public Texture white;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		alpha = 1;
		enemySpawner = GameObject.Find ("EnemySpawner");
		first= true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (display) {
			GUI.Box(new Rect(Screen.width/2 - 60, 20, 120, 25), (60 - endgameobj.GetComponent<EndGameScript>().timer).ToString ());
			GUI.Box(new Rect(Screen.width/2 - 120, 65, 240, 25), "Distance to target: " + Mathf.Round(Vector3.Distance (endgameobj.transform.position, player.transform.position)).ToString ());
			if(first){
				if(!onetoggle){
					audio.clip = beep;
					audio.PlayOneShot (beep);
					onetoggle = true;
				}
				Time.timeScale = 0.0f;
				if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 90, 240, 180), "Your red button gives you a beep, \npointing you towards the \nworld-unender device!")){
					first = false;
					Time.timeScale = 1.0f;
				}
			}
		}
	}

}
