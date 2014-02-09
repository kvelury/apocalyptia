﻿/*********************************************************************
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
	private float waterThreshold;
	private float grassThreshold;
	//private float dirtThreshold;
	private float mountainThreshold;

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
	protected override void GenTerrain(){
		waterThreshold = 0.15f + 0.1f * difficulty;
		grassThreshold = 0.20f + 0.1f * difficulty;
		//dirtThreshold = 0.22f + 2 * difficulty;
		mountainThreshold = 0.80f;
		blocks = new byte[gridWidth, gridHeight];
		//xStart = 913;//Random.Range (-1000, 1000);
		//yStart = -722;//Random.Range (-1000, 1000);
		Debug.Log ("Seeds used: " + xStart.ToString () + " " + yStart.ToString ());
		float[,] elevationMap = GetElevationMap (xStart, yStart);
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//assign values based on this number
				
				float elevation = elevationMap[px, py];
				if(elevation < waterThreshold){//low elevation
					blocks[px, py] = (byte)TileCodes.Water;
				}else if(elevation >= waterThreshold && elevation < grassThreshold){
					blocks[px, py] = (byte)TileCodes.Grass;
				}else if(elevation >= grassThreshold && elevation < mountainThreshold){//middle elevation
					blocks[px, py] = (byte)TileCodes.Sand;
				}else{//high elevation
					blocks[px, py] = (byte)TileCodes.Stone;
				}
			}
		}
	}

	protected override void AddResources(){
		//float treePercentage = 10f;
		//float stonePercentage = 30f;
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				/*
				//add trees
				if(blocks[i,j] == (byte)TileCodes.DryGrass ||  blocks[i,j] == (byte)TileCodes.Grass){
					if(Random.Range (1, 100) < treePercentage){
						Vector3 location = new Vector3( worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               		worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
						                               0);//0.49f);
						GameObject newTree = Instantiate (tree, location, new Quaternion(0, 0, 0, 0)) as GameObject;
					}
				}
				//add stone
				if(blocks[i,j] == (byte)TileCodes.Stone){
					if(Random.Range (1, 100) < stonePercentage){
						Vector3 location = new Vector3( worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               		worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
						                               0);//-0.49f);
						GameObject newStone = Instantiate (stone, location, new Quaternion(0, 0, 0, 0)) as GameObject;
					}
				}
				*/
				/*if(blocks[i, j] == (byte)TileCodes.Water){
					Vector3 location = new Vector3( worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                                 worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                                 0);
					GameObject newWater = Instantiate(water, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}*/
			}
		}
	}
}