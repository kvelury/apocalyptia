using UnityEngine;
using System.Collections;

public class HazardControlScript : MonoBehaviour {

	/// <summary>
	/// The reference to the Player.
	/// </summary>
	private GameObject Player;

	private int difficulty;

	private int maxHazards;

	private int currHazards;

	private float hazardSpawnRate;

	private float timeSinceLastHazard;

	private int currentApocalypse;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		SetDifficulty();
		timeSinceLastHazard = 0;
		//generate hazards based on difficulty
	}
	
	// Update is called once per frame
	void Update () {
		if (currHazards < maxHazards){
			//check a random chance against the spawn rate
			//this makes it more likely for a hazard to spawn as time goes on
			float result = Random.Range(0, hazardSpawnRate - timeSinceLastHazard);
		}
	}

	private void SpawnHazard(int apocalypseID){
		//spawn a hazard based on apocalypse

		//do{
		float theta, radius;
		theta = Random.Range (0, 360);
		radius = Random.Range (0, 1000) + 500;
		Vector3 location = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
		//} while (location is valid)

		switch (apocalypseID){
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		default:
			break;
		}

		//check for a proper location - ie a whirlpool is actually on a water tile
	}
	
	public void SetDifficulty(){
		difficulty  = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().difficulty;
		maxHazards = difficulty * 5;
		hazardSpawnRate = 60 * (6 - difficulty);
	}
}
