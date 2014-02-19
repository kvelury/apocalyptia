using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {
	public GameObject player;
	public EnemySpawn es;
	public EndGameHUD egh;
	public float timer;
	public float time;
	public float finaleTimer;
	public bool isEndGame;
	public bool winCond;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		es = GameObject.Find ("EnemySpawner").GetComponent<EnemySpawn> ();
		egh = Camera.main.GetComponent<EndGameHUD> ();
		egh.endgameobj = gameObject;
		egh.display = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("g") && Vector3.Distance (player.transform.position, transform.position) < 20) {
			if(!isEndGame)
				Debug.Log ("Endgame started. Brace yourself!");
			isEndGame = true;
		}
		if (winCond) {
			timer = 60;
			Camera.main.BroadcastMessage ("fadeOut");			
			finaleTimer++;
			if(finaleTimer > 120){
				Application.LoadLevel ("WinScene");
			}
		}
	}

	void FixedUpdate(){
		if (Input.GetKeyDown ("e")) {
			isEndGame = true;
			timer = 60;
				}
		if (isEndGame) {
			es.doomEvent = true;
			time+=Time.deltaTime;
			while(time > 1){
				//This block is if we want to adjust the rate of the clock.
				time -= 1;
				timer += 1;
				Debug.Log (timer);
			}
			if(timer >= 60){
				winCond = true;
				//Time.timeScale = 0.0f;
				player.GetComponent<PlayerResources>().mercyTimer = 0;
				//Debug.Log ("Conglaturation!");
				Camera.main.SendMessage ("fadeOut");
			}
		}
	}
}
