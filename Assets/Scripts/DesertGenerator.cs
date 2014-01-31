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
	private float waterThreshold = 0.07f;
	private float dirtThreshold = 0.09f;
	private float dryGrassThreshold = 0.11f;
	private float mountainThreshold = 0.85f;

	/*
	// Use this for initialization
	public override void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}
	*/

	/**************************************************************************
	 * The perlin noise function always returns the same value for same inputs,
	 * i.e. Mathf.PerlinNoise(5, 10) always has the same return value.
	 * 
	 * This function starts by assigning a random value to start at.  The 
	 * Perlin Noise plane is infinite, so we will just sample a random portion
	 * of it.  Loop through and extract the correct value at that point.
	 * 
	 * From there, assign ranges.  Low elevations will have water, high will 
	 * be stone for mountains.  The exact value that constitutes low or high
	 * will vary based on the apocalypse.  For desert, the threshold for water
	 * will be super low, but for flood it will be very high
	 * ***********************************************************************/
	protected override void GenTerrain(){
		blocks = new byte[gridWidth, gridHeight];
		float xStart = Random.Range (-1000, 1000);
		float yStart = Random.Range (-1000, 1000);
		Debug.Log ("Seeds used: " + xStart.ToString () + " " + yStart.ToString ());
		float[,] elevationMap = GetElevationMap (xStart, yStart);
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//assign values based on this number
				
				float elevation = elevationMap[px, py];
				if(elevation < waterThreshold){//low elevation
					blocks[px, py] = (byte)TileCodes.Water;
				}else if(elevation >= waterThreshold && elevation < dirtThreshold){
					blocks[px, py] = (byte)TileCodes.Dirt;
				}else if(elevation >= dirtThreshold && elevation < dryGrassThreshold){
					blocks[px, py] = (byte)TileCodes.DryGrass;
				}else if(elevation >= dryGrassThreshold && elevation < mountainThreshold){//middle elevation
					blocks[px, py] = (byte)TileCodes.Sand;
				}else{//high elevation
					blocks[px, py] = (byte)TileCodes.Stone;
				}
			}
		}
	}
}
