using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Transform prim; // prim
	Transform image; // image
	public AudioClip idle;
	public AudioClip attack;

    private float smoothTime = .8f;
    private Transform enemyTransform;
    private Vector3 velocity;
    private GameObject[] traps;

    private double enemySightRange = 10;
    private double enemyTooCloseToTrapDistance = 1.5;
	private Boolean attacking = false;

    private float deltaJump = .1f;
	private float jumpheight = 8.0f;
	private float jumpedsofar;
	private bool isjumping;
	private float distToGround;
	private bool ischasing;

    void Start()
    {
        enemyTransform = transform;
        prim = GameObject.FindWithTag("Player").transform;
        traps = GameObject.FindGameObjectsWithTag("Trap");
		distToGround = enemyTransform.collider.bounds.extents.y;
		ischasing = false;
    }

	bool IsGrounded() {
		return Physics.Raycast(enemyTransform.position, -Vector3.up, distToGround + 0.1f);
	}

    void Update()
    {
		if (ischasing) {
			enemyTransform.audio.clip = attack;
			enemyTransform.audio.loop = true;
			if (!transform.audio.isPlaying) {
				transform.audio.Play ();
			}
		} 
		else {
			attacking = false;
			transform.audio.clip = idle;
			transform.audio.loop = true;
			if (!transform.audio.isPlaying) {
				transform.audio.Play ();
			}
		}
		if (IsGrounded()) {
			jumpedsofar = 0.0f;
			isjumping = false;
		}

		GameObject imageObject = GameObject.FindWithTag ("Image");
		if (imageObject != null) {
			image = imageObject.transform;
//			Debug.Log ("found image");
		}

		double distanceToImage = 999999999.0;

	    double distanceToPlayer = Mathf.Sqrt(
	        Mathf.Pow(enemyTransform.position.x - prim.position.x, 2) 
	        + Mathf.Pow(enemyTransform.position.y - prim.position.y, 2));

		if(distanceToImage < 2 || distanceToPlayer < 4) {
			animation.CrossFade("Attack");
			attacking = true;
		} else {
			attacking = false;
		}

		if (image != null)
		{
//			Debug.Log ("calculating image distance");
			distanceToImage = Mathf.Sqrt (
				Mathf.Pow(enemyTransform.position.x - image.position.x, 2) 
				+ Mathf.Pow(enemyTransform.position.y - image.position.y, 2));
		}

//		Debug.Log ("image: " + distanceToImage + " prim: " + distanceToPlayer);
        if ((distanceToImage < enemySightRange) && (distanceToImage < distanceToPlayer)) {
//		Debug.Log("chasing image- distance: " + distanceToImage);
			if (image.position.y > enemyTransform.position.y) {
					if (IsGrounded ()) {
							Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y + deltaJump, enemyTransform.position.z);
							isjumping = true;
							enemyTransform.position = jumpPosition;
							jumpedsofar += deltaJump;
					} else if (isjumping) {
							if (jumpedsofar < jumpheight) {
									Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y + deltaJump, enemyTransform.position.z);
									enemyTransform.position = jumpPosition;
									jumpedsofar += deltaJump;
							} else {
									isjumping = false;
									Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y - (deltaJump / 2.0f), enemyTransform.position.z);
									enemyTransform.position = jumpPosition;
							}
					}
			}

		
			Chase (image);
		} else if (distanceToPlayer < enemySightRange) {
				//           Debug.Log("chasing player- distance: " + distanceToPlayer);
				Chase (prim);
				if (prim.position.y > enemyTransform.position.y) {
						if (IsGrounded ()) {
								Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y + deltaJump, enemyTransform.position.z);
								isjumping = true;
								enemyTransform.position = jumpPosition;
								jumpedsofar += deltaJump;
						} else if (isjumping) {
								if (jumpedsofar < jumpheight) {
										Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y + deltaJump, enemyTransform.position.z);
										enemyTransform.position = jumpPosition;
										jumpedsofar += deltaJump;
								} else {
										isjumping = false;
										Vector3 jumpPosition = new Vector3 (enemyTransform.position.x, enemyTransform.position.y - deltaJump, enemyTransform.position.z);
										enemyTransform.position = jumpPosition;
								}
						}
				}				
		} else {
			ischasing = false;
		}
		image = null;
        
    }

	void Chase(Transform target) {
		ischasing = true;
		float x = Mathf.SmoothDamp(enemyTransform.position.x, target.position.x, ref velocity.x, smoothTime);
		float y = Mathf.SmoothDamp(enemyTransform.position.y, target.position.y, ref velocity.y, smoothTime);
		float z = Mathf.SmoothDamp(enemyTransform.position.z, target.position.z, ref velocity.z, smoothTime);
		
		//if it's going to be too close to a trap, don't move.
		foreach (GameObject trap in traps)
		{
			double distanceFromTrap = Mathf.Sqrt(
				Mathf.Pow(trap.transform.position.x - x, 2) 
				+ Mathf.Pow(trap.transform.position.y - y, 2) 
				+ Mathf.Pow(trap.transform.position.z - z, 2));
			
			if (distanceFromTrap <= enemyTooCloseToTrapDistance)
			{
				x = enemyTransform.position.x;
				y = enemyTransform.position.y;
				z = enemyTransform.position.z;
			}
		}

		if (enemyTransform.position.x - target.position.x < 0.0f) {
			enemyTransform.eulerAngles = new Vector3 (0.0f, 90.0f, 0.0f);
		}
		else {
			enemyTransform.eulerAngles = new Vector3 (0.0f, 270.0f, 0.0f);
		}

		enemyTransform.position = new Vector3(x, enemyTransform.position.y, z);
		if (!attacking) {
			animation.CrossFade ("Run");
		}
	}
	
}
