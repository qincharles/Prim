using UnityEngine;
using System.Collections;

public class ImageAnimations : MonoBehaviour {
	bool playingswap = false;

	// Use this for initialization
	void Start () {
		animation.CrossFade ("Swap_Image");
		playingswap = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (playingswap && !animation.IsPlaying("Swap_Image")) {
			playingswap = false;
		}
		else {
			animation.CrossFade("Idle_Loop");
		}
	}
}
