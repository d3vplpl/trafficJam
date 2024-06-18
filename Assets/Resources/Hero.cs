using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	private float speed = 0.0F;
	private float maxSpeed = 20.0f;
	private float accel = 3.0f ;
	private float brake = 4.0f;
	private string str = "";
	private Vector3 movement = new Vector3(0f,0.65f,7f);
	private Transform thisTransform  ;
	private bool isCarSpawned = false;
	private int noOfCars;

	private float distance = 0f;
	public GameObject opponent;
	private int score = 0;

	void Start () {
		thisTransform = GetComponent<Transform>();


	}
	void Update() {				

		if ((Input.GetKey ("down")) && (speed < maxSpeed) && speed >0) {
				speed = speed - brake * Time.deltaTime;					
		} else if ((Input.GetKey ("up")) && (speed > -maxSpeed)) {
				speed = speed + accel * Time.deltaTime;
		}
		else {
				if (speed > brake * Time.deltaTime)
						speed = speed - brake * Time.deltaTime;
				else if (speed < -accel * Time.deltaTime)
					speed = speed + accel * Time.deltaTime;
					else
					speed = 0;
			}
		thisTransform.position = movement;
		movement.z = transform.position.z + speed * Time.deltaTime;
		distance = measureDistance ();
		if (distance > 10f) { 
			GameOver();
		}
		if ((int)opponent.renderer.bounds.size.z * noOfCars < score) {
			SpawnCars();

		}
	}


	void OnCollisionEnter(Collision collision) {
		str += "HIT";
		GameOver ();

	}
	float measureDistance () {
		score = (int)thisTransform.position.z;

		return ( opponent.transform.position.z - thisTransform.position.z);

	}

	void GameOver(){
		Application.LoadLevel (Application.loadedLevel);
	}
	void SpawnCars(){
		GameObject opponent2 = (GameObject)Instantiate (Resources.Load ("opponent2"));
		opponent2.transform.position = thisTransform.position;
		float x = 0f - (1.5f* opponent2.renderer.bounds.size.x);
		float z = thisTransform.position.z + 17.0f;
		float y = thisTransform.position.y - 0.2f;
		opponent2.transform.position = new Vector3 (x, y, z);

		GameObject opponent3 = (GameObject)Instantiate (Resources.Load ("opponent2"));
		opponent3.transform.position = thisTransform.position;
		float x2 = 0f + (1.5f* opponent2.renderer.bounds.size.x);
		float z2 = thisTransform.position.z + 15.0f;
		float y2 = thisTransform.position.y - 0.2f;
		opponent3.transform.position = new Vector3 (x2, y2, z2);
		//isCarSpawned = true;
		noOfCars+=2;
	}
	void OnGUI()
	{
		float menuY = 30;
		const float menuYM = 22;
		GUI.Box(new Rect(10, 10, 120, 180), "Debug"  + System.Environment.NewLine + "Speed: "+ speed.ToString() 
		        + System.Environment.NewLine + movement.ToString() );
		GUI.Label (new Rect (12, menuY += menuYM, 120, 20), str);
		GUI.Label (new Rect (12, menuY += menuYM, 120, 20), distance.ToString ());
		GUI.Label (new Rect (12, menuY += menuYM, 120, 20), "Score "+score.ToString ());

		GUI.Label (new Rect (12, menuY += menuYM, 120, 20), (opponent.renderer.bounds.size.z).ToString());
	}

}