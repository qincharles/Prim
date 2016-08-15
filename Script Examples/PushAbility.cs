using UnityEngine;
using System.Collections;

public class PushAbility : MonoBehaviour {

	public float pushdistance;
	public float pushfullcd;
	public float pushcdleft;
	public float fullforcemag;
	public float middle;
	public float pushFactor;
	public float timedone;
	public float timeslow;
	public float gravity;
	public ParticleSystem pushparts;

	public AudioClip pushSound;

//	private float numintervals;
	private float pushcdup;
//	public Player player1;
	private GameObject player;
	private Collider hitobject;
//	private CharacterController controller;
//	private bool pushing;
//	private Vector3 currentforce;
//	private Vector3 slowforce;
//	private Vector3 distancetravelled;
//	private float timesincepush;

	private float level;
	private float pushspeed;

	private GameObject primanim;
	private PrimAnimations animscript;

	// call when push hits an immoveable object
	void pushPrim(Vector3 force) {
//		controller.Move(force * Time.deltaTime * pushspeed);
		player.rigidbody.AddForce (force * Time.deltaTime * pushspeed);
//		distancetravelled += currentforce * Time.deltaTime;
	}

	// call when push hits a moveable object
	void pushObject(Vector3 force) {

	}

	// call when push hits an enemy
	void pushEnemy(Vector3 force) {
		hitobject.rigidbody.AddForce (force * Time.deltaTime * pushspeed * 2);
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
//		controller = GetComponent<CharacterController> ();
//		distancetravelled = new Vector3(0.0f,0.0f,0.0f);
//		timesincepush = 0.0f;
//		numintervals = (timedone - timeslow) / Time.deltaTime;

        PlayerPrefs.SetFloat("pushCooldownTime", pushfullcd);

		primanim = GameObject.FindWithTag("Animations");
		animscript = (PrimAnimations) primanim.GetComponent (typeof (PrimAnimations));
	}
	
	// Update is called once per frame
	void Update () {

//		level = (player.GetComponent(typeof(PrimLVController)) as PrimLVController).getLevel ();
		level = (float) PlayerPrefs.GetInt ("Level") / (float) PlayerPrefs.GetInt ("maxLevel");
		pushspeed = pushFactor * Mathf.Sqrt (level);

		// Apply images logic
		Push();

		pushcdleft = pushcdup - Time.time;
		if (pushcdleft <= 0) {
			PlayerPrefs.SetInt ("pushing", 0);
			pushcdleft = 0;
		}
		PlayerPrefs.SetFloat("pushCooldownLeft", pushcdleft);
	}

	// check keys, cooldowns
	void Push() {

		if (animscript.dead) {
			return;
		} 

		if (Input.GetAxis("Push") > 0) {
			if (pushcdleft <= 0) {
				Vector3 playerloc = player.rigidbody.transform.position;
	//			Vector3 playerloc = controller.transform.position;
				Vector3 mouse = Input.mousePosition;
				mouse.z = middle;
				mouse = Camera.main.ScreenToWorldPoint (mouse);
				mouse.z = 0.0f;
				RaycastHit hit = new RaycastHit();
				Vector3 direction = (mouse-playerloc);
				direction.Normalize ();
				float angle = Vector3.Angle (transform.forward,direction);
				if (direction.y >= transform.forward.y) {
					angle = -angle;
				}
//				Instantiate (pushparts,player.transform.position,Quaternion.identity);
//				float anglex = player.transform.eulerAngles.x;
				float angley = player.transform.eulerAngles.y;
				float anglez = player.transform.eulerAngles.z;
				pushparts.transform.eulerAngles = new Vector3(angle,angley,anglez);
//				pushparts.transform.position = player.transform.position;
				pushparts.Play ();
				if (!(animscript.state == 1)) {
					audio.PlayOneShot(pushSound);
				}
				if (PlayerPrefs.GetInt ("pushing") == 0) {
					animscript.state = 1;
					primanim.transform.animation.CrossFade("Push");
					PlayerPrefs.SetInt ("pushing", 1);
				}

				// check for hit
				if (Physics.Raycast (playerloc, direction, out hit, pushdistance)) {
					hitobject = hit.collider;
					float distance = hit.distance;
					float forceapply = (pushdistance-distance)/pushdistance * fullforcemag;
					Vector3 forceapplyobject = forceapply * direction;
//					pushing = true;
					if (hitobject.tag == "Moveable") {
						pushObject (forceapplyobject);
					} else if (hitobject.tag == "Enemy" || hitobject.tag == "Pig") {
						pushEnemy (forceapplyobject);
					} else {
	//					Vector3 forceapplyprim = forceapply * hit.normal;
						Vector3 forceapplyprim = -forceapplyobject;
	//					currentforce = forceapplyprim;
	//					slowforce = -forceapplyprim / numintervals;
						pushPrim(forceapplyprim);
					}
				}
				pushcdup = Time.time + pushfullcd;
				pushcdleft = pushcdup - Time.time;
			}
		}
	}
}
