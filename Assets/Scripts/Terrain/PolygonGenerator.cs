/*****************************************************************************************
 * Terrain generator script
 * By Cody Childers
 * Based of tutorials found at:
 * http://studentgamedev.blogspot.com/2013/08/unity-voxel-tutorial-part-1-generating.html
 * **************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ApocalypseType : int {Default, Desert, Flood, Volcano};

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
	public GameObject building;
	public GameObject player;
	public GameObject water;
	public GameObject enemyspawner;
	public GameObject endgameobj;
	public EnemySpawn es;
	//*******************************************************************************************
	//data needed externally
	public ApocalypseType apocalypseType;
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
	protected const float tUnit = 1.0f/16.0f; //percentage of the width of the image of one tile
	/*
    protected readonly Vector2 Grass    = new Vector2(3, 0);
	protected readonly Vector2 Stone    = new Vector2(1, 0);
	protected readonly Vector2 Water    = new Vector2(2, 0);
	protected readonly Vector2 Sand     = new Vector2(0, 0);
	protected readonly Vector2 Dirt     = new Vector2(4, 0);
	*/
	protected readonly Vector2 Water     = new Vector2( 1, 1);
	protected readonly Vector2 Stone     = new Vector2( 4, 1);
	protected readonly Vector2 Radiation = new Vector2( 7, 1);
	protected readonly Vector2 Sand      = new Vector2(10, 1);
	protected readonly Vector2 Grass     = new Vector2( 0, 12);//junk: replace with actua later
	protected readonly Vector2 Dirt      = new Vector2( 9, 1);//junk: replace with actua later
	public enum TileCodes : byte {Water, Stone, Radiation, Sand, Grass, Dirt};

	protected int squareCount = 0;

	//types of terrain elements at a given location
	public byte[,] blocks;
	//difficulty
	public enum Difficulty:int{Dumbass, Easy, Medium, Hard, Fuckyou};
	public int difficulty = 0;
	protected float xStart;
	protected float yStart;
	protected bool firstInit = true;


	protected readonly Vector2[] safeSpawnSeeds = { new Vector2( 8739, 5922),
	                                       new Vector2( 3342, 6371),
										   new Vector2( 5562, 9140),
	            						   new Vector2( 3526, 2741),
										   new Vector2( 8834, 5377),
										   new Vector2(  335, 5163)
	                                      };

	// Use this for initialization
	public virtual void Start () {
		SetType ();
		mesh = GetComponent<MeshFilter>().mesh;
		//set the seed if necessary, ignored if this is called by increasing difficulty instead
		if (firstInit) {
			//xStart = Random.Range (0, 10000);
			//yStart = Random.Range (0, 10000);
			int rand = Mathf.RoundToInt(Random.Range (0, 5));
			Vector2 startvec = safeSpawnSeeds[rand];
			xStart = startvec.x;
			yStart = startvec.y;
		}
		GenTerrain ();
		BuildMesh ();
		UpdateMesh ();
		AddResources ();
		if (firstInit) {
			SpawnPlayerSafely ();
			firstInit = false;
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//nothing
	}

	protected virtual void SetType(){
		apocalypseType = (int)ApocalypseType.Default;
	}

	public virtual void IncreaseDifficulty(){
		if (difficulty == maxLevel) {
			return;
		}
		enemyspawner = GameObject.Find ("EnemySpawner");
		es = enemyspawner.GetComponent<EnemySpawn> ();
		es.difficulty ++;
		difficulty++;
		if (difficulty == maxLevel) {
			GameObject endgame = Instantiate (endgameobj, 
			                                  new Vector3(player.transform.position.x + Random.Range (-30, 30), 
			            									player.transform.position.y + Random.Range (-30, 30)),
			                                  new Quaternion(0,0,0,0)) as GameObject;
			Debug.Log ("Endgame object spawned!");
				}
		//Added the next two lines to reduce lag - Kyle
		//ClearWater ();
		DespawnOldResources ();
		if(mesh!= null)
			mesh.Clear ();
		Start ();
		//blocks = null;
		GameObject.Find("HazardController").GetComponent<HazardControlScript>().SetDifficulty();
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
		CalculateMeshTangents ();

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

		//modify it by a tiny bit to stop grabbing parts of other tiles.
		float aLittleBit = 0.0005f;
		newUV.Add(new Vector2 (tUnit * texture.x + aLittleBit        , tUnit * texture.y + tUnit - aLittleBit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit - aLittleBit, tUnit * texture.y + tUnit - aLittleBit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit - aLittleBit, tUnit * texture.y + aLittleBit));
		newUV.Add(new Vector2 (tUnit * texture.x + aLittleBit        , tUnit * texture.y + aLittleBit));

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
				case (byte)TileCodes.Grass:
					//GenSquare(px, py, Grass);
					//SandOffset(px, py, Grass);
					TileOffset (px, py, Grass, TileCodes.Sand);
					break;
				case (byte)TileCodes.Stone:
					//GenSquare(px, py, Stone);
					//SandOffset(px, py, Stone);
					TileOffset (px, py, Stone, TileCodes.Sand);
					break;
				case (byte)TileCodes.Water:
					//GenSquare(px, py, Water);
					//SandOffset(px, py, Water);
					TileOffset (px, py, Water, TileCodes.Sand);
					break;
				case (byte)TileCodes.Sand:
					GenSquare (px, py, Sand);
					break;
				case (byte)TileCodes.Dirt:
					//GenSquare (px, py, Dirt);
					//SandOffset(px, py, Dirt);
					TileOffset (px, py, Dirt, TileCodes.Sand);
					break;
				case (byte)TileCodes.Radiation:
					//gen radiation
					//StoneOffset(px, py, Radiation);
					TileOffset (px, py, Radiation, TileCodes.Stone);
					break;
				}
			}
		}
	}

	public void CalculateMeshTangents(){
		//speed up math by copying the mesh arrays
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		
		//variable definitions
		int triangleCount = triangles.Length;
		int vertexCount = vertices.Length;
		
		Vector3[] tan1 = new Vector3[vertexCount];
		Vector3[] tan2 = new Vector3[vertexCount];
		
		Vector4[] tangents = new Vector4[vertexCount];
		
		for (long a = 0; a < triangleCount; a += 3)
		{
			long i1 = triangles[a + 0];
			long i2 = triangles[a + 1];
			long i3 = triangles[a + 2];
			
			Vector3 v1 = vertices[i1];
			Vector3 v2 = vertices[i2];
			Vector3 v3 = vertices[i3];
			
			Vector2 w1 = uv[i1];
			Vector2 w2 = uv[i2];
			Vector2 w3 = uv[i3];
			
			float x1 = v2.x - v1.x;
			float x2 = v3.x - v1.x;
			float y1 = v2.y - v1.y;
			float y2 = v3.y - v1.y;
			float z1 = v2.z - v1.z;
			float z2 = v3.z - v1.z;
			
			float s1 = w2.x - w1.x;
			float s2 = w3.x - w1.x;
			float t1 = w2.y - w1.y;
			float t2 = w3.y - w1.y;
			
			float r = 1.0f / (s1 * t2 - s2 * t1);
			
			Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
			Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
			
			tan1[i1] += sdir;
			tan1[i2] += sdir;
			tan1[i3] += sdir;
			
			tan2[i1] += tdir;
			tan2[i2] += tdir;
			tan2[i3] += tdir;
		}
		
		
		for (long a = 0; a < vertexCount; ++a)
		{
			Vector3 n = normals[a];
			Vector3 t = tan1[a];
			
			//Vector3 tmp = (t - n * Vector3.Dot(n, t)).normalized;
			//tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
			Vector3.OrthoNormalize(ref n, ref t);
			tangents[a].x = t.x;
			tangents[a].y = t.y;
			tangents[a].z = t.z;
			
			tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
		}
		
		mesh.tangents = tangents;
	}

	/*
	protected void SandOffset(int px, int py, Vector2 start){
		Vector2 assignedTexture = new Vector2(start.x, start.y);
		if (blocks [Mathf.Min(px + 1, gridWidth - 1), py] == (byte)TileCodes.Sand) {
			assignedTexture.x++;
		}
		if(blocks[Mathf.Max (px - 1, 0), py] == (byte)TileCodes.Sand){
			assignedTexture.x--;
		}
		if(blocks[px, Mathf.Min (py + 1, gridHeight - 1)] == (byte)TileCodes.Sand){
			assignedTexture.y++;
		}
		if(blocks[px, Mathf.Max (py - 1, 0)] == (byte)TileCodes.Sand){
			assignedTexture.y--;
		}
		GenSquare (px, py, assignedTexture);
	}

	protected void StoneOffset(int px, int py, Vector2 start){
		Vector2 assignedTexture = new Vector2(start.x, start.y);
		if (blocks [Mathf.Min(px + 1, gridWidth - 1), py] == (byte)TileCodes.Stone) {
			assignedTexture.x++;
		}
		if(blocks[Mathf.Max (px - 1, 0), py] == (byte)TileCodes.Stone){
			assignedTexture.x--;
		}
		if(blocks[px, Mathf.Min (py + 1, gridHeight - 1)] == (byte)TileCodes.Stone){
			assignedTexture.y++;
		}
		if(blocks[px, Mathf.Max (py - 1, 0)] == (byte)TileCodes.Stone){
			assignedTexture.y--;
		}
		GenSquare (px, py, assignedTexture);
	}*/

	protected void TileOffset(int px, int py, Vector2 start, TileCodes type){
		Vector2 assignedTexture = new Vector2(start.x, start.y);
		int bumps = 0;
		if (blocks [Mathf.Min(px + 1, gridWidth - 1), py] == (byte)type) {
			assignedTexture.x++;
			++bumps;
		}
		if(blocks[Mathf.Max (px - 1, 0), py] == (byte)type){
			assignedTexture.x--;
			++bumps;
		}
		if(blocks[px, Mathf.Min (py + 1, gridHeight - 1)] == (byte)type){
			assignedTexture.y++;
			++bumps;
		}
		if(blocks[px, Mathf.Max (py - 1, 0)] == (byte)type){
			assignedTexture.y--;
			++bumps;
		}
		//if bumps == 0, it is either right where it is, or it is an inner corner
		//if bumps == 1 or 2, it should be right where it is
		//if bumps == 3, it is a  corner surrounded on 3 sides by the border texture
		//if bumps == 4, it is a single tile surrounded on all sides by its border
		/*
		if (bumps == 0) {
			//check if it is an inner corner
			if(blocks[Mathf.Min (px + 1, gridWidth - 1), Mathf.Min (py + 1, gridHeight - 1)] != blocks[px, py]){
				assignedTexture.x--;
				assignedTexture.y--;
				GenSquare (px, py, assignedTexture);
				return;
			}
			if(blocks[Mathf.Min (px + 1, gridWidth - 1), Mathf.Max (py - 1, 0)             ] != blocks[px, py]){
				assignedTexture.x--;
				assignedTexture.y++;
				GenSquare (px, py, assignedTexture);
				return;
			}
			if(blocks[Mathf.Max (px - 1, 0)            , Mathf.Min (py + 1, gridHeight - 1)] != blocks[px, py]){
				assignedTexture.x++;
				assignedTexture.y--;
				GenSquare (px, py, assignedTexture);
				return;
			}
			if(blocks[Mathf.Max (px - 1, 0)            , Mathf.Max (py - 1, 0)             ] != blocks[px, py]){
				assignedTexture.x++;
				assignedTexture.y++;
				GenSquare (px, py, assignedTexture);
				return;
			}
		}
		*/
		GenSquare (px, py, assignedTexture);
	}

	protected virtual void AddResources(){
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				//add trees
				if(blocks[i,j] == (byte)TileCodes.Grass){
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
				//add buildings
				if(blocks[i,j] == (byte)TileCodes.Sand){
					Vector3 location = new Vector3(worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                               0.49f);
					GameObject newBuilding = Instantiate (building, location, new Quaternion(0, 0, 0, 0)) as GameObject;
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
		//COMMENTING THIS OUT FOR NOW
		/*
		//starting point to check in the middle
		player = GameObject.Find ("Player");
		int midx = gridWidth/2;
		int midy = gridHeight/2;
		//an array of vectors to try moving to to find a safe spot
		//these go radially around a square
		Vector2[] locationVectors = { new Vector2( 0,  0),
									  new Vector2(-1, -1),
									  new Vector2( 0, -1),
									  new Vector2( 1, -1),
									  new Vector2(-1,  0),
									  new Vector2( 1,  0),
									  new Vector2(-1,  1),
									  new Vector2( 0,  1),
									  new Vector2( 1,  1) };
		//multiplied factor to the vector2's above
		for(int offset = 1; true; offset++){//this is broken by the return
			for(int i = 0; i < locationVectors.Length; i++){//check each item in the array
				if(blocks[midx + (int)locationVectors[i].x * offset, midy + (int)locationVectors[i].y * offset]
				   != (byte)TileCodes.Water){
					Vector2 newLocation = new Vector2((midx + locationVectors[i].x * offset),
					                                  (midy + locationVectors[i].y * offset));
					player.transform.Translate (0, 0, 0);//reset to make the next computation easier
					player.transform.Translate ( newLocation.x, newLocation.y, 0);
					player.transform.Translate (0,0,-player.transform.position.z);
					Debug.Log ("Moved Player: " + locationVectors[i].ToString () + " " + midx + " " + midy + " " 
					           + blocks[midx + (int)locationVectors[i].x, midy + (int)locationVectors[i].y]);
					return;
				}
			}
		}*/
		//Temorary fix.
		player = GameObject.Find ("Player");
		/*
		bool safe = false;
		while (!safe) {
			//player.transform.Translate (-player.transform.position.x, -player.transform.position.y, 0);
			player.transform.position =  new Vector3(Random.Range (-25,PolygonGenerator.gridWidth * PolygonGenerator.worldScale)
			                                         ,Random.Range (-30,PolygonGenerator.gridHeight * PolygonGenerator.worldScale), 0);
			//player.transform.Translate (0,0,-player.transform.position.z);
			if (blocks [Mathf.CeilToInt (player.transform.position.x / PolygonGenerator.worldScale - 1), 
			            Mathf.CeilToInt (player.transform.position.y / PolygonGenerator.worldScale)] 
			    != (byte)PolygonGenerator.TileCodes.Water) {
				safe = true;
			}
			else Debug.Log ("InWater, moving");
		}
		*/
		player.transform.position = new Vector3 (150, 150, 0);
	}
	//Cloned from GameFlowController, since it's private.
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
