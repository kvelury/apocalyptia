/*****************************************************************************************
 * Terrain generator script
 * By Cody Childers
 * Based of tutorials found at:
 * http://studentgamedev.blogspot.com/2013/08/unity-voxel-tutorial-part-1-generating.html
 * **************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PolygonGenerator : MonoBehaviour {

	//*******************************************************************************************
	//constants
	public const int gridWidth = 100;
	public const int gridHeight = 100;
	public const float worldScale = 3.0f;
	protected const int perlinSamples = 3;
	public const int maxLevel = 5;
	//*******************************************************************************************
	//references for resources
	public GameObject tree;
	public GameObject stone;
	public GameObject player;
	public GameObject water;
	public GameObject enemyspawner;
	public EnemySpawn es;
	//*******************************************************************************************

	//list of vertices to the mesh
	public List<Vector3> newVertices = new List<Vector3>();
	//triangles how to build each section of the mesh
	public List<int> newTriangles = new List<int>();
	//UV list to tell Unity how texture is aligned to polygon
	public List<Vector2> newUV = new List<Vector2>();
	//save final terrain as this mesh
	protected Mesh mesh;

	//variables to pick apart the sprite sheet
	protected const float tUnit = 0.20f; //percentage of the width of the image of one tile
    protected readonly Vector2 Grass    = new Vector2(3, 0);
	protected readonly Vector2 Stone    = new Vector2(1, 0);
	protected readonly Vector2 Water    = new Vector2(2, 0);
	protected readonly Vector2 Sand     = new Vector2(0, 0);
	protected readonly Vector2 Dirt     = new Vector2(4, 0);
	public enum TileCodes : byte {Grass, DryGrass, Stone, Water, Sand, Dirt};

	protected int squareCount = 0;

	//types of terrain elements at a given location
	public byte[,] blocks;
	//difficulty
	public enum Difficulty:int{Dumbass, Easy, Medium, Hard, Fuckyou};
	public int difficulty = 0;
	protected float xStart;
	protected float yStart;
	protected bool firstInit = true;


	// Use this for initialization
	public virtual void Start () {
		mesh = GetComponent<MeshFilter>().mesh;

		//fetch the difficulty from player preferences
		/*
		if (false) {
			difficulty = 0;
		} else {
			difficulty = 0;
		}
		*/
		//set the seed if necessary
		if (firstInit) {
			xStart = Random.Range (0, 10000);
			yStart = Random.Range (0, 10000);
			firstInit = false;
		}
		GenTerrain ();
		BuildMesh ();
		UpdateMesh ();
		AddResources ();
		SpawnPlayerSafely ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//nothing
	}

	public virtual void IncreaseDifficulty(){
		if (difficulty == maxLevel) {
			return;
		}
		enemyspawner = GameObject.Find ("EnemySpawner");
		es = enemyspawner.GetComponent<EnemySpawn> ();
		es.difficulty ++;
		difficulty++;
		//Added the next two lines to reduce lag - Kyle
		ClearWater ();
		if(mesh!= null)
			mesh.Clear ();
		Start ();
		//blocks = null;
	}

	public int GetDifficulty(){
		return this.difficulty;
	}

	protected void ClearWater(){
		GameObject[] water = GameObject.FindGameObjectsWithTag ("Water");
		for (int i = 0; i < water.Length; i++) {
			Destroy (water[i]);
				}
		}

	protected void UpdateMesh(){
		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray ();
		mesh.Optimize();
		mesh.RecalculateNormals();

		squareCount = 0;
		newVertices.Clear ();
		newTriangles.Clear ();
		newUV.Clear ();
	}

	protected void GenSquare(int x, int y, Vector2 texture, float distance = 0){
		newVertices.Add( new Vector3(worldScale * x, worldScale * y, 0));
		newVertices.Add( new Vector3(worldScale * (x + 1), worldScale * y, 0));
		newVertices.Add( new Vector3(worldScale * (x + 1), worldScale * (y - 1), 0));
		newVertices.Add( new Vector3(worldScale * x, worldScale * (y - 1), 0));
		
		//unity uses left handed triangles
		//the opposite of openGL's right handed
		//so right hand rule for poly's doesn't work here
		newTriangles.Add(squareCount*4);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+3);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+2);
		newTriangles.Add((squareCount*4)+3);
		
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));

		squareCount++;
	}

	protected virtual void GenTerrain(){
		blocks = new byte[gridWidth, gridHeight];
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//these are stupid random values.  More intelligent values come from derived classes
				float tmp = Mathf.Round(Random.Range (0, 6));
				tmp = tmp % 6;
				blocks[px, py] = (byte)tmp;
			}
		}
	}

	protected void BuildMesh(){
		for(int px = 0; px < blocks.GetLength (0); px++){
			for(int py = 0; py < blocks.GetLength (1); py++){
				switch(blocks[px, py]){
				//case (byte)TileCodes.DryGrass:
				//	GenSquare(px, py, DryGrass);
				//	break;
				case (byte)TileCodes.Grass:
					GenSquare(px, py, Grass);
					break;
				case (byte)TileCodes.Stone:
					GenSquare(px, py, Stone);
					break;
				case (byte)TileCodes.Water:
					GenSquare(px, py, Water);
					break;
				case (byte)TileCodes.Sand:
					GenSquare (px, py, Sand);
					break;
				case (byte)TileCodes.Dirt:
					GenSquare (px, py, Dirt);
					break;
				}
			}
		}
	}

	protected virtual void AddResources(){
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				//add trees
				if(blocks[i,j] == (byte)TileCodes.DryGrass ||  blocks[i,j] == (byte)TileCodes.Grass){
					Vector3 location = new Vector3(worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                               0.49f);
					GameObject newTree = Instantiate (tree, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}
				//add stone
				if(blocks[i,j] == (byte)TileCodes.Stone){
					Vector3 location = new Vector3(worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                               0.49f);
					GameObject newStone = Instantiate (stone, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}
			}
		}
	}

	
	protected float[,] GetElevationMap(float x, float y){
		float[,] elevation = new float[gridWidth, gridHeight];
		float perlinScale = 15.0f;
		for (int i = 0; i < perlinSamples; i++) {
			for (int px = 0; px < blocks.GetLength (0); px++) {
				for (int py = 0; py < blocks.GetLength (1); py++) {
					//float xSample = px + x / gridWidth * perlinScale;
					//float ySample = py + y / gridHeight * perlinScale;
					//Debug.Log (xSample.ToString () + " " + ySample.ToString ());
					elevation [px, py] += Noise ((int)x + px, (int)y + py, perlinScale, 1, 1.0f);
				}
			}
			perlinScale *= 500.0f;
		}
		Normalize (elevation);
		//printArray (elevation);
		return elevation;
	}

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
	protected float Noise(int x, int y, float scale, float mag, float exp){
		return Mathf.Pow (Mathf.PerlinNoise (x / scale, y / scale) * mag, exp);
	}

	protected void Normalize(float[,] map){
		//change all values to be between 0 and 1, since they will be a sum of [perlinSamples] passes
		for(int i = 0; i < map.GetLength (0); i++){
			for(int j = 0; j < map.GetLength (1); j++){
				map[i,j] /= (float)perlinSamples;
			}
		}
		//find the low and high points
		float low = 10;
		float high = -10;
		for(int i = 0; i < map.GetLength (0); i++){
			for(int j = 0; j < map.GetLength (1); j++){
				if(map[i,j] > high)
					high = map[i,j];
				if(map[i,j] < low)
				   low = map[i,j];
			}
		}
		//Debug.Log ("Low: " + low.ToString () + " High: " + high.ToString ());
		//rescale so low is 0 and high is 1
		for(int i = 0; i < map.GetLength (0); i++){
			for(int j = 0; j < map.GetLength (0); j++){
				map[i,j] -= low;
				map[i,j] *= 1/(high-low);
			}
		}
	}

	protected void printArray(float[,] x){
		string s = "";
		for (int i = 0; i < x.GetLength (0); i++) {
			for (int j = 0; j < x.GetLength (1); j++) {
				s += x [i, j].ToString () + " ";
			}
			s += "\n";
		}
		Debug.Log (s);
	}

	protected void SpawnPlayerSafely(){
		/*
		//starting point to check in the middle
		int midx = gridWidth/2;
		int midy = gridHeight/2;
		//an array of vectors to try moving to to find a safe spot
		//these go radially around a square
		Vector2[] locationVectors = { new Vector2(-1, -1),
									  new Vector2( 0, -1),
									  new Vector2( 1, -1),
									  new Vector2(-1,  0),
									  new Vector2( 1,  0),
									  new Vector2(-1,  1),
									  new Vector2( 0,  1),
									  new Vector2( 1,  1) };
		//multiplied factor to the vector2's above
		for(int offset = 1; true; offset++){//this is broken by the return
			for(int i = 0; i < locationVectors.Length; i++){
				if(blocks[midx + Mathf.FloorToInt(locationVectors[i].x) * offset, midy + Mathf.FloorToInt (locationVectors[i].y) * offset]
				   != (byte)TileCodes.Water){
					player.transform.Translate (0, 0, 0);//reset to make the next computation easier
					player.transform.Translate (midx + locationVectors[i].x * offset,
					                            midy + locationVectors[i].y * offset, 
					                            0);
					return;
				}
			}
		}*/
	}

}
