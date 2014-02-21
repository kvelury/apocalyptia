using UnityEngine;
using System.Collections;

public class VolcanoGenerator : PolygonGenerator {

	/*
	// Use this for initialization
	public override void Start () {
	
	}
	*/
		
	// Update is called once per frame
	/*************************************************
	* Comment this out once game controller is in   *
	* **********************************************/
	public override void Update () {
		if (Input.GetKeyDown ("r")) {
			IncreaseDifficulty ();
		}
	}
		
		/*
	public override void IncreaseDifficulty(){
		difficulty++;
		if (difficulty > 5) {
			difficulty = 5;
		}
		Start ();
	}
	*/
		
	protected override void SetType(){
		apocalypseType = ApocalypseType.Desert;
	}
		
		
	protected override void GenTerrain(){
		float lavaThreshold = 0.20f + 0.1f * difficulty;//this value and below is lava
		float hardLavaThreshold = 0.30f + 0.1f * difficulty;//this value and below is hardened lava
		float scorchedThreshold = 0.80f;//this value and below is scorched stone, above is dirt
		blocks = new byte[gridWidth, gridHeight];
		//xStart = 913;//Random.Range (-1000, 1000);
		//yStart = -722;//Random.Range (-1000, 1000);
		Debug.Log ("Seeds used: " + xStart.ToString () + " " + yStart.ToString ());
		float[,] elevationMap = GetElevationMap (xStart, yStart);
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//assign values based on this number
					
				float elevation = elevationMap[px, py];
				if(elevation < lavaThreshold){//low elevation
					blocks[px, py] = (byte)TileCodes.Lava;
				}else if(elevation >= lavaThreshold && elevation < hardLavaThreshold){
					blocks[px, py] = (byte)TileCodes.HardLava;
				}else if(elevation >= hardLavaThreshold && elevation < scorchedThreshold){//middle elevation
					blocks[px, py] = (byte)TileCodes.Scorched;
				}else{//high elevation
					blocks[px, py] = (byte)TileCodes.Dirt;
				}
			}
		}
	}
		
	protected override void AddResources(){
		/***************************************************************/
		//THESE NUMBERS COMBINED ARE NOT TO EXCEED 50 DUE TO LAG ISSUES//
		/***************************************************************/
		float treePercentage = 15f;
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				//add trees
				if(blocks[i,j] == (byte)TileCodes.Dirt){
					if(Random.Range (1, 100) < treePercentage){
						Vector3 location = new Vector3( worldScale * i + worldScale * 0.5f,
						                               worldScale * j - worldScale * 0.5f,
						                               0);//0.49f);
						GameObject newTree = Instantiate (burntTree, location, new Quaternion(0, 0, 0, 0)) as GameObject;
					}
				}
			}
		}
	}
}