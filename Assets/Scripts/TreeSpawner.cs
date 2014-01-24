using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour {
	GameObject tree;
	PolygonGenerator pg;

	// Use this for initialization
	void Start () {
		GameObject DG = GameObject.Find ("DesertGenerator");
		pg = DG.GetComponent<PolygonGenerator> ();
		byte[,] terrain = pg.blocks;
		AddTrees (terrain);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AddTrees(byte[,] land){
		for(int i = 0; i < land.GetLength (0); i++){
			for(int j = 0; j < land.GetLength (1); j++){
				if(land[i,j] == (byte)PolygonGenerator.TileCodes.DryGrass ||  land[i,j] == (byte)PolygonGenerator.TileCodes.Grass){
					Vector3 location = new Vector3(i - 25, j - 25, 0);
					GameObject newTree = Instantiate (tree, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}
			}
		}
	}
}

//GameObject enemyInstance = Instantiate (Enemy, spawnLoc, new Quaternion(0,0,0,0)) as GameObject;