using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject Enemy;
	public GameObject Player;
	//private GameObject[] enemies;
	public int maxEnemies;
	public int enemyCount;
	public int timer;
	public int enemySpawnTime;
	float incrementTime = 1;
	float incrementBy = 1;
	public int difficulty;
	public float doomTimer = 0;
	public float winTimer;
	double time =0;
	public bool doomEvent;
	public bool winEvent;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		incrementTime = 1f;
		incrementBy = 1;
		maxEnemies = 20;
		doomEvent = false;
		winEvent = false;
		enemySpawnTime = 20;
		winTimer = 0;
		for (int i = 0; i < maxEnemies; i++){
			SpawnEnemy();
			//enemyCount++;
		}
		
	}
	void Update(){
		if (!doomEvent) {
						switch (difficulty) {
						case 1:
								maxEnemies = 25;
								enemySpawnTime = 20;
								break;
						case 2:
								maxEnemies = 25;
								enemySpawnTime = 15;
								break;
						case 3:
								maxEnemies = 30;
								enemySpawnTime = 15;
								break;
						case 4:
								maxEnemies = 30;
								enemySpawnTime = 10;
								break;
						case 5:
								maxEnemies = 35;
								enemySpawnTime = 10;
								break;
						}
				}
		else {
			maxEnemies = 40;
			enemySpawnTime = 5;
				}
		}
	// Update is called once per frame
	void FixedUpdate () {
/*		time+=Time.deltaTime;
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
		}*/
		//Destroy (enemies);
		if (enemyCount < maxEnemies) {
			timer++;
			if(timer > enemySpawnTime){
				timer = 0;
				if(Random.Range (0, 10) > 2 || enemyCount <= 0)
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
		//spawnLoc.x = Random.Range (-25, worldWidth);
		//spawnLoc.y = Random.Range (-30, worldHeight);
		float xDist = 0;
		while(xDist > -20 && xDist < 20)
			xDist = Random.Range (-49, 49);
		
		float yDist = 0;
		while(yDist > -20 && yDist < 20)
			yDist = Random.Range (-49, 49);
		spawnLoc.x = Player.transform.position.x + xDist;
		spawnLoc.y = Player.transform.position.y + yDist;
		//spawnLoc.x = Random.Range (Player.transform.position.x - 10, Player.transform.position.x + 10);
		//spawnLoc.x = spawnLoc.x * 4;
		//spawnLoc.y = Random.Range (Player.transform.position.y - 10, Player.transform.position.y + 10);
		//spawnLoc.y = spawnLoc.y * 4;
		enemyCount++;
		GameObject enemyInstance = Instantiate (Enemy, spawnLoc, new Quaternion(0,0,0,0)) as GameObject;
		//enemyInstance.GetComponent<LookAtScript>().target = GameObject.Find("Main Camera").transform;
	}
}
