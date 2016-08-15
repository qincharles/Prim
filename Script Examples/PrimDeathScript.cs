using UnityEngine;
using System.Collections;

public class PrimDeathScript : MonoBehaviour {

	public int lives;

	public AudioClip dieSound;
	public AudioClip crySound;

	private GameObject primanim;
	private PrimAnimations animscript;
	private int timesincedeath;
	private bool sadness;

//	private GameObject player;

	// Use this for initialization
	void Start () {
//		player = GameObject.Find ("Player");
		primanim = GameObject.FindWithTag("Animations");
		animscript = (PrimAnimations) primanim.GetComponent (typeof (PrimAnimations));
		timesincedeath = 0;
	}

	public void hit(bool tears) {
		lives--;
		// play hit sound
		if (lives <= 0) {
			animscript.dead = true;
			if (tears) {
				sadness = true;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if ((other.tag == "Enemy") || (other.tag == "Trap")) {
			hit (false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (lives <= 0) {
			PlayerPrefs.SetString ("LastLevel", Application.loadedLevelName);
			timesincedeath++;
			if (timesincedeath == 1) {
				primanim.animation.Stop ();
			}
			if (sadness) {
				primanim.animation.CrossFade ("Sob", .6f);
				audio.PlayOneShot(crySound);
				if (timesincedeath > 85) {
					Application.LoadLevel("DeathScreenScene");
				}
			} else {
				if (timesincedeath > 85) {
					Application.LoadLevel("DeathScreenScene");
				} else if (timesincedeath > 28) {
					// maybe tune position on the ground later
					primanim.animation.enabled = false;
				} else if (timesincedeath > 5) {
					audio.PlayOneShot(dieSound);
	//				primanim.animation["Death"].wrapMode = WrapMode.Once;
					primanim.animation.CrossFade ("Death", .2f);
				}
			}
		}
	}
}
