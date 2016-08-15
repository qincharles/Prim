using UnityEngine;
using System.Collections;

public class FreePrim : MonoBehaviour {

	public GameObject cageSplodeParticles;
	public GameObject cage;
	public GameObject pigMat;
	public GameObject pig;
	public float fadeOutParam = 0.1f;

	private Object particles;
//	private int n = 0;
	private bool noParticles = true;
	private bool pigRescued = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(pigRescued && pig){
			if (pigMat.renderer.material.color.a > 0){
				Color pigMatCol = pigMat.renderer.material.color;
//				Debug.Log(pigMatCol.a);
				pigMatCol.a = pigMatCol.a - fadeOutParam;
//				Debug.Log(pigMatCol.a);
				pigMat.renderer.material.color = pigMatCol;
			} else if (pigMat.renderer.material.color.a <= 0){
				Destroy(pig);
				Destroy(this);
			}
		}

	}

	void OnTriggerEnter(Collider coll)
	{
		
		if ( coll.gameObject.CompareTag("Player") && noParticles){
			noParticles = false;
			//splode the particles
			Vector3 pos = this.transform.position;
			//pos.z = -3;
			particles = Instantiate(cageSplodeParticles, pos, Quaternion.identity);
			Invoke("killParticles", 1.5f);

			//eradicate the cage
			Invoke("cageAway", 1.0f);

			//away the pig
			pigMat.renderer.material.shader= Shader.Find("Transparent/Diffuse");
			Invoke ("pigAway",2.0f);

			//increment the score
			PlayerPrefs.SetInt("numPigs", PlayerPrefs.GetInt("numPigs") + 1);

		}

	}

	void killParticles(){
		Destroy(particles);
		noParticles = true;
	}

	void cageAway(){
		Destroy(cage);
	}

	void pigAway(){
		//pig.transform.rotation.SetFromToRotation(Vector3.forward, Vector3.right);
		pigRescued = true;
	}

}
