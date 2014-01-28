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
	double time =0;
	// Use this for initialization
	void Start () {
		incrementTime = 0.3f;
		incrementBy = 1;
		maxEnemies = 15;
		enemySpawnTime = 80;
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
		}
		if (doomTimer >= 120) {
			maxEnemies = 100;
			enemySpawnTime = 20;
		}
		if (enemyCount < maxEnemies) {
			timer++;
			if(timer > enemySpawnTime){
				timer = 0;
				if(Random.Range (0, 10) > 4)
					SpawnEnemy();
			}
		}
		
	}
	
	void SpawnEnemy () {
		Vector3 spawnLoc = new Vector3 (0, 0, 0);
		spawnLoc.x = Random.Range (-25, 124);
		spawnLoc.y = Random.Range (-30, 116);
		enemyCount++;
		GameObject enemyInstance = Instantiate (Enemy, spawnLoc, new Quaternion(0,0,0,0)) as GameObject;
		//enemyInstance.GetComponent<LookAtScript>().target = GameObject.Find("Main Camera").transform;
	}
}
