using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject Enemy;
	public int maxEnemies;
	public int enemyCount;
	public int timer;
	public int enemySpawnTime;
	float incrementTime = 1;
	float incrementBy = 1;
	public float doomTimer = 0;
	public float winTimer;
	double time =0;
	public bool doomEvent;
	public bool winEvent;
	// Use this for initialization
	void Start () {
		incrementTime = 1f;
		incrementBy = 1;
		maxEnemies = 50;
		doomEvent = false;
		winEvent = false;
		enemySpawnTime = 80;
		winTimer = 0;
		for (int i = 0; i < maxEnemies; i++){
			SpawnEnemy();
			//enemyCount++;
		}
		
	}
	void Update(){
		}
	// Update is called once per frame
	void FixedUpdate () {
		time+=Time.deltaTime;
		while(time>incrementTime)
		{
			time-=incrementTime;
			doomTimer+=incrementBy;
			if(doomEvent == true)
				winTimer+=incrementBy;
		}
		if (doomTimer >= 120) {
			maxEnemies = 250;
			enemySpawnTime = 20;
			doomEvent = true;
		}
		if (enemyCount < maxEnemies) {
			timer++;
			if(timer > enemySpawnTime){
				timer = 0;
				if(Random.Range (0, 10) > 4)
					SpawnEnemy();
			}
		}
		if (winTimer >= 120) {
			winEvent = true;
			Debug.Log ("You have survived!");
			//Victory code goes here.
		}
		
	}
	
	void SpawnEnemy () {
		Vector3 spawnLoc = new Vector3 (0, 0, 0);
		//find the data for where the world is located
		int worldWidth = PolygonGenerator.gridWidth;
		int worldHeight = PolygonGenerator.gridHeight;
		//rescale
		worldWidth *= (int)PolygonGenerator.worldScale;
		worldHeight *= (int)PolygonGenerator.worldScale;
		spawnLoc.x = Random.Range (-25, worldWidth);
		spawnLoc.y = Random.Range (-30, worldHeight);
		enemyCount++;
		GameObject enemyInstance = Instantiate (Enemy, spawnLoc, new Quaternion(0,0,0,0)) as GameObject;
		//enemyInstance.GetComponent<LookAtScript>().target = GameObject.Find("Main Camera").transform;
	}
}
