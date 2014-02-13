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

	private ApocalypseType currentApocalypse;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		SetDifficulty();
		timeSinceLastHazard = 0;
		currentApocalypse = GameObject.Find("GameController").GetComponent<PolygonGenerator>().apocalypseType;
		//generate hazards based on difficulty
		for (int i = 0; i<maxHazards; i++){
			SpawnHazard();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currHazards < maxHazards){
			//check a random chance against the spawn rate
			//this makes it more likely for a hazard to spawn as time goes on
			float result = Random.Range(0, hazardSpawnRate - timeSinceLastHazard);
			if (result > 1){
				SpawnHazard();
			}
		}
	}

	private void SpawnHazard(){
		//spawn a hazard based on apocalypse

		//check for a proper location - ie a whirlpool is actually on a water tile
		//do{
		float theta, radius;
		theta = Random.Range (0, 360);
		radius = Random.Range (0, 1000) + 500;
		Vector3 location = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
		//} while (location is valid)

		switch (currentApocalypse){
		case ApocalypseType.Desert:
			//spawn a dust devil
			break;
		case ApocalypseType.Flood:
			//spawn a whirlpool
			break;
		case ApocalypseType.Volcano:
			//spawn a rockfall
			break;
		case ApocalypseType.Default:
			break;
		default:
			break;
		}

		currHazards++;
	}
	
	public void SetDifficulty(){
		difficulty  = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().difficulty;
		maxHazards = difficulty * 5;
		hazardSpawnRate = 60 * (6 - difficulty);
	}
}
