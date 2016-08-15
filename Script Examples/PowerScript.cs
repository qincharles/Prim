using UnityEngine;
using System.Collections;

public class PowerScript : MonoBehaviour {
	
//	private static PowerScript instance = null;
	public static float powerlevel;
	public static float adjustments;
	
	// Use this for initialization
	
	void Start () {

		PowerScript.powerlevel = 100f;
		PowerScript.adjustments = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		PowerScript.powerlevel+=PowerScript.adjustments;
	}
}
