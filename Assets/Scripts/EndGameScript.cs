using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {
	public GameObject player;
	public EnemySpawn es;
	public float timer;
	public float time;
	public bool isEndGame;
	public bool winCond;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		es = GameObject.Find ("EnemySpawner").GetComponent<EnemySpawn> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("g") && Vector3.Distance (player.transform.position, transform.position) < 20) {
			if(!isEndGame)
				Debug.Log ("Endgame started. Brace yourself!");
			isEndGame = true;
		}
	}
	void FixedUpdate(){
		
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
				Debug.Log ("Conglaturation!");
				//Put score-saving here.
				Application.LoadLevel ("WinScene");
			}
		}
	}
}
