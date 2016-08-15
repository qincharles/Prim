using UnityEngine;
using System;

//Shock behavior will be triggered via the left shift button being held down, 
//and the mouse being clicked in the direction that Prim will shock in.
public class Shock : MonoBehaviour
{
    public float range = 20f;//set it so that Prim can shock things a distance of 10 away, will decrement later.
    public float shockwave = 10f;//Size of the shock will also decrement
	public ParticleSystem shockparticles;
    Transform target;
	public float middle;

	public AudioClip shockSound;

    public PrimLVController primLevelControl;

	public float shocksoundcd = 1.0f;
	public float shocksoundcdleft;
	private float shocksoundcdup;

	private GameObject primanim;
	private PrimAnimations animscript;

    void Start()
    {
        GameObject prim = GameObject.FindWithTag("Player");
        target = prim.transform;
        primLevelControl = (PrimLVController)prim.GetComponent(typeof(PrimLVController));
		primanim = GameObject.FindWithTag("Animations");
		animscript = (PrimAnimations) primanim.GetComponent (typeof (PrimAnimations));
		PlayerPrefs.SetInt ("shocking", 0);
    }

    void Update()
    {
		shocksoundcdleft = shocksoundcdup - Time.time;
		if (shocksoundcdleft <= 0) {
			PlayerPrefs.SetInt ("shocking", 0);
			shocksoundcdleft = 0;
			animscript.state = 0;
		} 
		if (animscript.dead) {
			return;
		}
		if (Input.GetAxis("Shock") > 0)
        {
			if (shocksoundcdleft <= 0) {
				shocksoundcdup = Time.time + shocksoundcd;
				shocksoundcdleft = shocksoundcdup - Time.time;
			}
	        Vector3 mVec = Input.mousePosition;//get from the player where to shock
			mVec.z = middle;
			mVec = Camera.main.ScreenToWorldPoint (mVec);

	        Vector3 shockVec = mVec;
	        if(distance2D(mVec, target.position) > range){//if they are out of range, put it to the max
	            shockVec = scaleToRange(target.position, mVec);
	        }

			shockparticles.transform.position = shockVec;
			shockparticles.Play();
			if (!(animscript.state == 4)) {
				audio.PlayOneShot(shockSound);
			}
			if (PlayerPrefs.GetInt ("shocking") == 0) {
				animscript.state = 4;
				primanim.transform.animation.CrossFade("Shock");
				PlayerPrefs.SetInt ("shocking", 1);
			}

	        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

	        foreach (GameObject enemy in enemies)
	        {
	            float distToEnemy = distance2D(enemy.transform.position, shockVec);

	            if (distToEnemy <= shockwave)
	            {
	                Destroy(enemy);
                    primLevelControl.decrementLevel();
	            }
	        }

			GameObject[] crystals = GameObject.FindGameObjectsWithTag ("Crystal");

			foreach (GameObject crystal in crystals) {
				float distToCrystal = distance2D (crystal.transform.position, shockVec);

				if (distToCrystal <= shockwave) {
					Destroy (crystal);
					PlayerPrefs.SetInt("numShards", PlayerPrefs.GetInt("numShards") + 1);
				}
			}
        }
    }

    private float distance2D(Vector3 a, Vector3 b)
    {
        return (float)(Math.Sqrt(
            Math.Pow(a.x - b.x, 2)
            + Math.Pow(a.y - b.y, 2)
            ));
    }

    private Vector3 scaleToRange(Vector3 start, Vector3 end)
    {
        float size = distance2D(start, end);
        return new Vector3((float)(((end.x - start.x) * range / size) + start.x), (float)(((end.y - start.y) * range / size) + start.y), 0);
    }

}
