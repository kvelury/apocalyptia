/*********************************************************************
 * DesertGenerator.cs
 * Cody Childers
 * inherits from the polygon generator class
 * uses more intelligent design to design code specific to the desert
 * but uses many of the methods inherited from PolygonGenerator
 * ******************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesertGenerator : PolygonGenerator {
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
		float radiationThreshold = 0.15f + 0.1f * difficulty;//this value and below is radioactive
		float stoneThreshold = 0.30f + 0.1f * difficulty;//this value and below is stone
		float sandThreshold = 0.80f;//this value and below is sand, above is water
		blocks = new byte[gridWidth, gridHeight];
		//xStart = 913;//Random.Range (-1000, 1000);
		//yStart = -722;//Random.Range (-1000, 1000);
		Debug.Log ("Seeds used: " + xStart.ToString () + " " + yStart.ToString ());
		float[,] elevationMap = GetElevationMap (xStart, yStart);
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//assign values based on this number
				
				float elevation = elevationMap[px, py];
				if(elevation < radiationThreshold){//low elevation
					blocks[px, py] = (byte)TileCodes.Radiation;
				}else if(elevation >= radiationThreshold && elevation < stoneThreshold){
					blocks[px, py] = (byte)TileCodes.Stone;
				}else if(elevation >= stoneThreshold && elevation < sandThreshold){//middle elevation
					blocks[px, py] = (byte)TileCodes.Sand;
				}else{//high elevation
					blocks[px, py] = (byte)TileCodes.Water;
				}
			}
		}
	}

	protected override void AddResources(){
		/***************************************************************/
		//THESE NUMBERS COMBINED ARE NOT TO EXCEED 50 DUE TO LAG ISSUES//
		/***************************************************************/
		float treePercentage = 10f;
		float stonePercentage = 20f;
		float buildingPercentage = 1f;
		float cactusPercentage = 20f;
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				//add trees
				if(blocks[i,j] == (byte)TileCodes.Grass){
					if(Random.Range (1, 100) < treePercentage){
						Vector3 location = new Vector3( worldScale * i + worldScale * 0.5f,
					                               		worldScale * j - worldScale * 0.5f,
						                               0);//0.49f);
						Instantiate (tree, location, new Quaternion(0, 0, 0, 0));
					}
				}
				//add stone
				if(blocks[i,j] == (byte)TileCodes.Stone){
					if(Random.Range (1, 100) < stonePercentage){
						Vector3 location = new Vector3( worldScale * i + worldScale * 0.5f,
					                               		worldScale * j - worldScale * 0.5f,
						                                0);
						Instantiate (stone, location, new Quaternion(0, 0, 0, 0));
					}
				}
				//add buildings
				if(blocks[i,j] == (byte)TileCodes.Sand){
					if(Random.Range (1, 100) < buildingPercentage){
						Vector3 location = new Vector3( worldScale * i + worldScale * 0.5f,
						                               worldScale * j  - worldScale * 0.5f,
						                               0);//-0.49f);
						Instantiate (building, location, new Quaternion(0, 0, 0, 0));
					}
				}
				if(blocks[i,j] == (byte)TileCodes.Sand){
					if(Random.Range (1, 100) < treePercentage){
						Vector3 location = new Vector3( worldScale * i + worldScale * 0.5f,
						                               worldScale * j  - worldScale * 0.5f,
						                               0);//-0.49f);
						Instantiate (burntTree, location, new Quaternion(0, 0, 0, 0));
					}
				}
			}
		}
	}
}
