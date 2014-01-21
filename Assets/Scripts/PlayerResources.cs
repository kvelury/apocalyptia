using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
	public float woodCount;
	public float stoneCount;
	public float ironCount;
	//May end up moving fame and health to their own scripts.
	public float fameCount;
	public float healthCount;

	/*****************************************************************************
	 * boolean variable when this class wants to interact with the tile it is on.  
	 * The PolygonGenerator class actually handles the interaction
	 * **************************************************************************/
	public bool interactWithCurrentTile;
	
	public void giveIron(float amount){
		ironCount += amount;
	}

	public void giveWood(float amount){
		woodCount += amount;
	}

	public void giveStone(float amount){
		stoneCount += amount;
	}

	// Use this for initialization
	void Start () {
		healthCount = 100;
		woodCount = fameCount = stoneCount = ironCount = 0;
		interactWithCurrentTile = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthCount <= 0)
						Application.LoadLevel ("DeathScene");
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "Enemy") {
			healthCount -= 5;
		}
	}
}
