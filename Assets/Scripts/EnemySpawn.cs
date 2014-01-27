using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject Enemy;
	public int maxEnemies;
	public int enemyCount;
	public int timer;
	// Use this for initialization
	void Start () {
		maxEnemies = 5;
		for (int i = 0; i < maxEnemies; i++){
			SpawnEnemy();
			enemyCount++;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyCount < maxEnemies) {
			timer++;
			if(timer > 80){
				timer = 0;
				if(Random.Range (0, 10) > 7)
					SpawnEnemy();
			}
		}
		
	}
	
	void SpawnEnemy () {
		Vector3 spawnLoc = new Vector3 (0, 0, 0);
		spawnLoc.x = Random.Range (-25, 24);
		spawnLoc.y = Random.Range (-30, 18);
		
		GameObject enemyInstance = Instantiate (Enemy, spawnLoc, new Quaternion(0,0,0,0)) as GameObject;
		enemyInstance.GetComponent<LookAtScript>().target = GameObject.Find("Main Camera").transform;
	}
}
