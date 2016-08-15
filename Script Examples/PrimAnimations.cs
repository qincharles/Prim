using UnityEngine;
using System.Collections;

public class PrimAnimations : MonoBehaviour {
	public int state = 0;

	public bool dead;

	void Start() {
		dead = false;
		PlayerPrefs.SetInt ("pushing", 0);
		PlayerPrefs.SetInt ("imaging", 0);
		PlayerPrefs.SetInt ("swapping", 1);
		PlayerPrefs.SetInt ("jumping", 0);
	}

	void Update() {
		//Update state based on current animation
		//Allows animations to be played once fully through
		//then return to idle loop
		if(state == 1 && !animation.IsPlaying("Push")) {
			state = 0;
		}
		else if(state == 2 && !animation.IsPlaying("Make_Image")) {
			state = 0;
		}
		else if(state == 3 && !animation.IsPlaying("Swap_Image")) {
			state = 0;
		}
		else if(state == 4 && !animation.IsPlaying("Shock")) {
			//state = 0;
		}
		else if(state == 5 && !animation.IsPlaying("Jump")) {
			state = 0;
		}

		//Initiate animations based on the keys pressed and current
		//state of the character
		if (dead) {
			return;
		}
		if((Input.GetAxis("Push") > 0) || state == 1) {
			/* if (PlayerPrefs.GetInt ("pushing") == 0) {
				state = 1;
				animation.CrossFade("Push");
				PlayerPrefs.SetInt ("pushing", 1);
			} */
		}
		else if((Input.GetAxis("PlaceImage") > 0) || state == 2) {
			/* if (PlayerPrefs.GetInt ("imaging") == 0) {
				state = 2;
				animation.CrossFade("Make_Image");
				PlayerPrefs.SetInt ("imaging", 1);
			} */
		}
		else if((Input.GetAxis("SwapImage") > 0) || state == 3) {
			/* if (PlayerPrefs.GetInt ("swapping") == 0) {
				state = 3;
				animation.CrossFade("Swap_Image");
			} */
		}
		else if((Input.GetAxis("Shock") > 0) || state == 4) {
			/* state = 4;
			animation.CrossFade("Shock"); */
		}
		else if((Input.GetAxis("Jump") > 0) || state == 5) {
			/* if (PlayerPrefs.GetInt ("jumping") == 0) {
				state = 5;
				animation.CrossFade("Jump");
				PlayerPrefs.SetInt ("jumping", 1);
			} */
		}
		else if((Input.GetAxis("LeftKey") > 0) || (Input.GetAxis ("RightKey") > 0)) {
			if (state == 0) {
				animation.CrossFade("Move_Forward");
			}
		}
		else {
			animation.CrossFade("Idle_Loop");
		} 
	}
}
