using UnityEngine;
using System.Collections;

public class GameFlowController : MonoBehaviour {

	//fields for terrain, set these in inspector
	public GameObject desertApocalypse;
	public GameObject floodApocalypse;
	public GameObject volcanoApocalypse;
	public GameObject frostApocalypse;
	public GameObject derpApocalypse;

	public GameObject currentApocalypse;
	
	public GameObject player;
	private PlayerMovementScript movement;
	public GameObject sword;
	public GameObject shield;
	public GameObject boots;
	public GameObject healthleech;
	public GameObject moreHearts;
	public GameObject oneUp;
	public GameObject forceField;
	public GameObject itemInstance;

	public int currentBaseDifficulty = 0;

	//creation of the object doesn't happen right when i say it, so need this
	private bool worldStillNeedsUpdate = false;

	// Use this for initialization
	void Start () {
		movement = player.GetComponent<PlayerMovementScript> ();
		NewApocalypse ();
		currentApocalypse.GetComponent<PolygonGenerator> ().player = this.player;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("o")) {
			NewApocalypse ();
			Debug.Log ("O was pressed - remove this asap\nGameFlowController.Update()");
		}
		if (Input.GetKeyDown ("p")) {
			currentApocalypse.GetComponent<PolygonGenerator> ().IncreaseDifficulty ();
			Debug.Log ("DifficultyIncreased - P was pressed\nRemove this ASAP\nGameFlowController.Update()");
		}
		if (worldStillNeedsUpdate) {
			//make sure it generates at proper difficulty
			for (int i = 0; i < currentBaseDifficulty; i++) {
				currentApocalypse.GetComponent<PolygonGenerator> ().IncreaseDifficulty ();
			}
			worldStillNeedsUpdate = false;
		}

	}

	public void NewApocalypse(){
		DespawnOldResources ();
		//increase difficulty in preparation
		++currentBaseDifficulty;
		//clean up the current
		if (currentApocalypse != null) {
			GameObject.Destroy (currentApocalypse);
			
			GameObject[] items = GameObject.FindGameObjectsWithTag ("Item");
			for (int i = 0; i < items.Length; i++) {
				Destroy (items[i]);
			}
		}
		//pick the new one
		//avoid repeat apocalypses
		int rand = Mathf.RoundToInt(Random.Range (0, 100)) % 2;
		if(currentApocalypse != null){
			int targetToAvoid = (int)currentApocalypse.GetComponent<PolygonGenerator> ().apocalypseType;
			while (rand == targetToAvoid) {
				rand = Mathf.RoundToInt(Random.Range (0, 100)) % 2;
			}
		}

		//create the proper new apocalypse
		switch (rand) {
			case 0:
				currentApocalypse = (GameObject)Instantiate (desertApocalypse, new Vector3 (0, 0, .28f), new Quaternion (0, 0, 0, 0));
				break;
			case 1:
				currentApocalypse = (GameObject)Instantiate (floodApocalypse, new Vector3 (0, 0, .28f), new Quaternion (0, 0, 0, 0));
				break;
		}
		for (int i = 0; i < 20; i++) {
			switch (Random.Range (0,8)){
			case 0:
				itemInstance = Instantiate (sword, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                                          ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                                       , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 1:
				itemInstance = Instantiate (shield, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                                          ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                                       , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 2:
				itemInstance = Instantiate (boots, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                                          ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                                       , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 3:
				itemInstance = Instantiate (healthleech, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                               ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                            , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 4:
				itemInstance = Instantiate (moreHearts, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                                    ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                            , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 5:
				itemInstance = Instantiate (oneUp, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                        ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                     , new Quaternion(0,0,0,0)) as GameObject;
				break;
			case 6:
				itemInstance = Instantiate (forceField, new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
				                                               ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale))
				                            , new Quaternion(0,0,0,0)) as GameObject;
				break;
			}

		}
		//tell the player where to find the terrain for purposes of water, lava, etc.
		movement.terrain = currentApocalypse.GetComponent<PolygonGenerator>();
		worldStillNeedsUpdate = true;
		GameObject.Find("HazardController").GetComponent<HazardControlScript>().SetDifficulty();
	}

	private void DespawnOldResources(){
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("tree");
		GameObject[] stones = GameObject.FindGameObjectsWithTag ("stone");
		
		for (int i = 0; i < trees.Length; i++) {
			Destroy (trees[i]);
		}
	
		for (int i = 0; i < stones.Length; i++) {
			Destroy (stones[i]);
		}
	}
}
