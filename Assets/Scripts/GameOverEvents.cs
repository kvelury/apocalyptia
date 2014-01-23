using UnityEngine;
using System.Collections;

public class GameOverEvents : MonoBehaviour {

	/// <summary>
	/// This array stores the talent tree.  
	/// There are 4 talents per path, plus one central talent.
	/// 0->1->2->3
	/// 4->5->5->7
	/// 8->9->10->11
	/// 3 || 7 || 11 -> 12
	/// </summary>
	public static Talent[] talentTree = new Talent[13];

	//GUI
	void OnGUI(){
		int result = -1;

		// Make a background box
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height - 50, 100, 50), "You Are Dead\n Try Again?"))
			Application.LoadLevel ("MainMenu");

		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 100), talentTree [0].Name)) {
			result = talentTree [0].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 100), talentTree [1].Name)) {
			result = talentTree [1].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 100), talentTree [2].Name)) {
			result = talentTree [2].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 1 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 100), talentTree [3].Name)) {
			result = talentTree [3].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 100), talentTree [4].Name)) {
			result = talentTree [4].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 100), talentTree [5].Name)) {
			result = talentTree [5].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 100), talentTree [6].Name)) {
			result = talentTree [6].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 2 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 100), talentTree [7].Name)) {
			result = talentTree [7].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 1/6 - 50, 100, 100), talentTree [8].Name)) {
			result = talentTree [8].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 2/6 - 50, 100, 100), talentTree [9].Name)) {
			result = talentTree [9].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 3/6 - 50, 100, 100), talentTree [10].Name)) {
			result = talentTree [10].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width * 3 / 4 - 50, (Screen.height - 100) * 4/6 - 50, 100, 100), talentTree [11].Name)) {
			result = talentTree [11].ActivateTalent ();
		}
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height * 5 / 6 - 50, 100, 100), talentTree [12].Name)) {
			result = talentTree[12].ActivateTalent ();
		}

		switch (result) {
		case 0:
			Debug.Log("Talent Activated");
			break;
		case 1:
			Debug.Log ("Already Active");
			break;
		case 2:
			Debug.Log ("Prerequisite Needed");
			break;
		case 3:
			Debug.Log ("Fame Stocks are too low, Sire.");
			break;
		default:
			break;
		}

	}
	
	// Use this for initialization
	void Start () {
		//Load the saved talents to the tree
		talentTree [0] = new Talent ("Self 1", 0, false, new int[] {});
		talentTree [1] = new Talent ("Self 2", 0, false, new int[] {0});
		talentTree [2] = new Talent ("Self 3", 0, false, new int[] {1});
		talentTree [3] = new Talent ("Self 4", 0, false, new int[] {2});
		talentTree [4] = new Talent ("Group 1", 0, false, new int[] {});
		talentTree [5] = new Talent ("Group 2", 0, false, new int[] {4});
		talentTree [6] = new Talent ("Group 3", 0, false, new int[] {5});
		talentTree [7] = new Talent ("Group 4", 0, false, new int[] {6});
		talentTree [8] = new Talent ("World 1", 0, false, new int[] {});
		talentTree [9] = new Talent ("World 2", 0, false, new int[] {8});
		talentTree [10] = new Talent ("World 3", 0, false, new int[] {9});
		talentTree [11] = new Talent ("World 4", 0, false, new int[] {10});
		talentTree [12] = new Talent ("Talent 5", 0, false, new int[] {3, 7, 11});

			//onGUI ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

/// <summary>
/// The Talent class holds the prerequisite, the cost, the gui?, and whether it is active.
/// </summary>
public class Talent{

	/// <summary>
	/// The name of the talent
	/// </summary>
	private string name;
	public string Name{
		get { return name; }
	}

	///<summary>
	/// An int that denotes the prerequisite talent
	/// </summary>
	private int[] prereqs;
//	public int Prerequisite{
//		get { return prereq; }
//	}

	///<summary>
	/// A float that denotes the cost of the talent
	/// </summary>
	private float cost;
	public float Cost{
		get { return cost; }
	}

	///<summary>
	/// A bool that denotes whether the talent is active
	/// </summary>
	private bool active;
	public bool Active {
		get { return active; }
	}

	public Talent(string Name, float Cost, bool Active, int[] Prereqs){
		name = Name;
		prereqs = Prereqs;
		cost = Cost;
		active = Active;
	}
	
	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="Talent"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="Talent"/>.</returns>
	public override string ToString(){
		return name + ": Prereq: " + prereqs.ToString() + "; Cost: " + cost.ToString() + "; Active: " + active.ToString() + "\n";
	}

	/// <summary>
	/// Activate this Talent.
	/// </summary>
	/// <returns>If already active, 1. If prerequisite is not active, returns 2.  If not enough fame, returns 3.</returns>
	public int ActivateTalent(){
		//first, check if the talent is already active.
		if (active)
			return 1;
		//next check if the prerequisite is active
		if (prereqs.Length != 0) {
			bool onePrereqMet = false;
			foreach (int i in prereqs) {
				if (GameOverEvents.talentTree [i].Active){
					onePrereqMet = true;
					break;
				}
			}
			if (!onePrereqMet)
				return 2;
		}
		//next, check if there is enough fame
		//if (fame < cost){
		//    return 3;
	    //}
		//fame -= cost;
		active = true;
		return 0;
	}
}
