using UnityEngine;
using System.Collections;

public class ImageAbility : MonoBehaviour {
	
//	public float speed;
//	public float loudspeed;
	public float middle;
	public float rangeFactor;
//	public float playerspeed;
	public float imagecdleft;
	public float imagefullcooldown;
	public ParticleSystem imageparticles;
	public ParticleSystem playerparticles;

	public AudioClip placeSound;
	public AudioClip swapSound;

	private GameObject image;
	private GameObject player;
//	private Vector3 imoriginalPosition;
	private Quaternion imoriginalRotation;
	private bool switched;
	private float imagecdup;
//	private CharacterController controller;

	private GameObject primanim;
	private PrimAnimations animscript;

	private float imagedistance;
	private int level;

	void Start() {
		switched = false;
		image = GameObject.FindGameObjectsWithTag ("Image")[0];
		image.SetActive (false);
		player = GameObject.FindGameObjectsWithTag ("Player") [0];
//		controller = GetComponent <CharacterController> ();
//		imoriginalPosition = image.rigidbody.transform.position;
//		imoriginalRotation = image.rigidbody.transform.rotation;
//		LateUpdate ();
//		audio.Play ();

        PlayerPrefs.SetFloat("imageCooldownTime", imagefullcooldown);
		primanim = GameObject.FindWithTag("Animations");
		animscript = (PrimAnimations) primanim.GetComponent (typeof (PrimAnimations));
	}

	void ResetImage() {
		switched = false;
		PlayerPrefs.SetInt ("swapping", 0);
//		image.rigidbody.transform.position = imoriginalPosition;
//		image.rigidbody.transform.rotation = imoriginalRotation;
//		if (player.rigidbody != null) {
			image.rigidbody.velocity = Vector3.zero;
			image.rigidbody.angularVelocity = Vector3.zero;
//		}
	}

	void Images() {

		if (animscript.dead) {
			return;
		}
		// place button clicked
		if (Input.GetAxis("PlaceImage") > 0) {
			if (imagecdleft <= 0) {
				ResetImage ();
				Vector3 v3 = Input.mousePosition;
				v3.z = middle;
				v3 = Camera.main.ScreenToWorldPoint (v3);
				Vector3 distance = v3 - player.rigidbody.transform.position;
//				Vector3 distance = v3 - controller.transform.position;
				if (distance.magnitude > imagedistance) {
					float scale = imagedistance/distance.magnitude;
					distance *= scale;
					v3 = player.rigidbody.transform.position + distance;
//					v3 = controller.transform.position + distance;
				}
				v3.z = 0.0f;
//				if (v3.y >= 1) {
				imagecdup = Time.time + imagefullcooldown;
				image.rigidbody.transform.position = v3;
				if (!(animscript.state == 2)) {
					audio.PlayOneShot(placeSound);
				}
				if (PlayerPrefs.GetInt ("imaging") == 0) {
					animscript.state = 2;
					primanim.transform.animation.CrossFade("Make_Image");
					PlayerPrefs.SetInt ("imaging", 1);
				}
				image.SetActive (true);
//				}
//				imageparticles.transform.position = v3;
				imageparticles.Play();
			} else {
				// maybe indicate sound for skill on cd
			}
		}
		// swap button clicked
		else if (Input.GetAxis("SwapImage") > 0) {
			if ((image.activeInHierarchy) && (!switched)) {
				// image on the field
				Vector3 playerpos = rigidbody.transform.position;
//				Vector3 playerpos = controller.transform.position;
				Vector3 imagepos = image.rigidbody.transform.position;

				if (!(animscript.state == 3)) {
					audio.PlayOneShot(swapSound);
				}

				if (PlayerPrefs.GetInt ("swapping") == 0) {
					animscript.state = 3;
					primanim.transform.animation.CrossFade("Swap_Image");
					PlayerPrefs.SetInt ("swapping", 1);
				}

				rigidbody.transform.position = imagepos;
//				controller.transform.position = imagepos;
				image.rigidbody.transform.position = playerpos;
				switched = true;

				playerparticles.Play ();
			}
		}
	}

	void Update() {

		level = GetComponent<PrimLVController> ().getLevel ();
		imagedistance = rangeFactor * level;
		if (switched) {
			PlayerPrefs.SetInt ("swapping", 1);
		}

		imagecdleft = imagecdup - Time.time;
		if (imagecdleft <= 0) {
			imagecdleft = 0;
			PlayerPrefs.SetInt ("imaging", 0);
		}
		PlayerPrefs.SetFloat("imageCooldownLeft", imagecdleft);
		// Apply images logic
		Images();
	}
}