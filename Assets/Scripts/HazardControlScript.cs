using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardControlScript : MonoBehaviour {

	/// <summary>
	/// The reference to the Player.
	/// </summary>
	private GameObject Player;
	private PolygonGenerator terrain;

	private List<GameObject> hazards;
	private List<GameObject> deadHazards;

	private ApocalypseType currentApocalypse;
	private int difficulty;

	private int maxHazards = 0;
	private int currHazards;

	private float hazardSpawnRate;
	private float timeSinceLastHazard;

	public ParticleSystem rockfall;
	
	// Use this for initialization
	void Start () {
//		Debug.Log ("Hazard Generator Activating");
		Player = GameObject.Find("Player");
		terrain = (PolygonGenerator) GameObject.Find("GameController").GetComponent<GameFlowController>().currentApocalypse.GetComponent<PolygonGenerator>();
		SetDifficulty();
		timeSinceLastHazard = 0;

//		Debug.Log("MaxHazards: " + maxHazards.ToString());

		hazards = new List<GameObject>();
		deadHazards = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastHazard++;

		//find and destroy all spent particle systems
        //Debug.Log("Count: " + hazards.Count);
		foreach (GameObject g in hazards){
			HazardScript hazardScript = g.GetComponent<HazardScript>();
			//Debug.Log("isAlive? " + hazardScript.isAlive);
			if (!hazardScript.isAlive){
				Destroy (g);
				deadHazards.Add(g);
				//Debug.Log ("Hazard Removed");
			}
		}
		foreach (GameObject g in deadHazards){
			hazards.Remove(g);
		}
		deadHazards.Clear();

		if (hazards.Count < maxHazards){
			//check a random chance against the spawn rate
			//this makes it more likely for a hazard to spawn as time goes on
			float result = Random.Range(0, hazardSpawnRate - timeSinceLastHazard);
			if (result < 1){
				SpawnHazard();
			}
		}
		//		Debug.Log("Hazard Count: " + hazards.Count.ToString());
	}

	private void SpawnHazard(){
		//spawn a hazard based on apocalypse
		Vector3 location;

		//check for a proper location - ie a whirlpool is actually on a water tile
		do{
			float theta, radius;
			theta = Random.Range (0, 360);
			radius = Random.Range (0, 10);
			location = new Vector3(radius * Mathf.Cos(theta * Mathf.Deg2Rad) + Player.transform.position.x, radius * Mathf.Sin(theta * Mathf.Deg2Rad) + Player.transform.position.y, -5);
		} while (!isValidLocation(location));

		switch (currentApocalypse){
		case ApocalypseType.Desert:
			//spawn a dust devil
			hazards.Add(Instantiate(rockfall, location, Quaternion.Euler(0, 0, 0)) as GameObject);
			break;
		case ApocalypseType.Flood:
			//spawn a whirlpool
			hazards.Add(Instantiate(rockfall, location, Quaternion.Euler(0, 0, 0)) as GameObject);
			break;
		case ApocalypseType.Volcano:
			//spawn a rockfall
			hazards.Add(Instantiate(rockfall, location, Quaternion.Euler(0, 0, 0)) as GameObject);
			break;
		case ApocalypseType.Default:
			hazards.Add(Instantiate(rockfall, location, Quaternion.Euler(0, 0, 0)) as GameObject);
			break;
		default:
			break;
		}

		//Debug.Log("Hazard Spawned");
		timeSinceLastHazard = 0;
		//currHazards++;
	}

	public bool isValidLocation(Vector3 location){
		bool returnVal;
		byte currTile;

//		Debug.Log ("Location: " + new Vector3(Mathf.CeilToInt(location.x / PolygonGenerator.worldScale - 1),
//		                                      Mathf.CeilToInt(location.y / PolygonGenerator.worldScale), 0).ToString());
//		Debug.Log ("Player Location: " + new Vector3(Mathf.CeilToInt(Player.transform.position.x / PolygonGenerator.worldScale - 1),
//		                                      Mathf.CeilToInt(Player.transform.position.y / PolygonGenerator.worldScale), 0).ToString());
		try {
			currTile = terrain.blocks[Mathf.CeilToInt(location.x / PolygonGenerator.worldScale - 1),
			                               Mathf.CeilToInt(location.y / PolygonGenerator.worldScale)];
		} catch(UnityException e){
			return false;
		}

		switch (currentApocalypse){
		case ApocalypseType.Default:
			returnVal = true;
			break;
		case ApocalypseType.Desert:
			returnVal = (currTile != (byte)PolygonGenerator.TileCodes.Water);
			break;
		case ApocalypseType.Flood:
			returnVal = (currTile == (byte)PolygonGenerator.TileCodes.Water);
			break;
		case ApocalypseType.Volcano:
			returnVal = true;
			break;
		default:
			returnVal = true;
			break;
		}

		return returnVal;
	}
	
	public void SetDifficulty(){
		currentApocalypse = terrain.apocalypseType;
		difficulty  = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().difficulty;
//		Debug.Log ("Difficulty: " + difficulty.ToString());
		maxHazards = difficulty * 2 + 5;
		hazardSpawnRate = 60 * (5 - difficulty);
	}
}
