using UnityEngine;
using System.Collections;

public class ImageController : MonoBehaviour {

	private Quaternion imrotation;
	private GameObject image;

	// Use this for initialization
	void Start () {
		image = GameObject.Find ("Image");
		imrotation = image.rigidbody.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		image.rigidbody.rotation = imrotation;
//		if (image.rigidbody.position.y <= 0) {
//			image.SetActive (false);
//		}
		if (image.rigidbody.velocity.x != 0) {
			Vector3 nox = image.rigidbody.velocity;
			nox.Set (0.0f, nox.y, nox.z);
			image.rigidbody.velocity = Vector3.Slerp (image.rigidbody.velocity, nox, 0.99f);
		}
		image.rigidbody.position.Set (image.rigidbody.position.x,image.rigidbody.position.y, 0.0f);
		image.rigidbody.transform.Translate (0.0f, 0.0f, 0.0f);
	}

	void OnTriggerEnter(Collider other) {
		if ((other.tag == "Enemy") || (other.tag == "Trap") || (other.tag == "2dConfinePlane") || (other.tag == "Pig")) {
//			GameObject image = GameObject.FindGameObjectsWithTag ("Image")[0];
			image.SetActive (false);
		}
	}

	void OnTriggerStay(Collider other) {
		if ((other.tag == "Enemy") || (other.tag == "Trap")) {
			//			GameObject image = GameObject.FindGameObjectsWithTag ("Image")[0];
			image.SetActive (false);
		}
	}
/*	void OnTriggerStay(Collider other) {
		if ((other.tag == "Ground") || (other.tag == "Platform")) {
//			GameObject image = GameObject.FindGameObjectsWithTag ("Image")[0];
			image.SetActive (false);
		}
	} */
}
