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

	public int currentBaseDifficulty = 0;

	// Use this for initialization
	void Start () {
		movement = player.GetComponent<PlayerMovementScript> ();
		NewApocalypse ();
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

	}

	public void NewApocalypse(){
		//increase difficulty in preparation
		++currentBaseDifficulty;
		if (currentBaseDifficulty > 5) {
			currentBaseDifficulty = 5;
		}
		//clean up the current
		if (currentApocalypse != null) {
			GameObject.Destroy (currentApocalypse);
		}
		//pick the new one
		int rand = Mathf.RoundToInt(Random.Range (0, 100)) % 2;
		Debug.Log (rand);
		//create the proper new apocalypse
		switch (rand) {
			case 0:
				currentApocalypse = (GameObject)Instantiate (desertApocalypse, new Vector3 (0, 0, 1), new Quaternion (0, 0, 0, 0));
				break;
			case 1:
				currentApocalypse = (GameObject)Instantiate (floodApocalypse, new Vector3 (0, 0, 1), new Quaternion (0, 0, 0, 0));
				break;
		}
		//tell the player where to find the terrain for purposes of water, lava, etc.
		movement.terrain = currentApocalypse.GetComponent<PolygonGenerator>();
		//make sure it generates at proper difficulty
		Debug.Log ("before loop");
		while(currentApocalypse.GetComponent<PolygonGenerator>().GetDifficulty () < currentBaseDifficulty) {
			Debug.Log ("Difficulty increased");
			currentApocalypse.GetComponent<PolygonGenerator> ().IncreaseDifficulty ();
		}
		Debug.Log ("after loop");

	}
}
