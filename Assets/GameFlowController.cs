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

	// Use this for initialization
	void Start () {
		movement = player.GetComponent<PlayerMovementScript> ();
		NewApocalypse ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("o")) {
			NewApocalypse ();
		}
	}

	public void NewApocalypse(){
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

	}
}
