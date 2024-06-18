using UnityEngine;
using System.Collections;

public class Opponent : MonoBehaviour {
	private string action = "go";
	// Use this for initialization
	private float speed;
	private float velocityD =2f;
	private static float maxSpeed= 20f;
	private Transform thisTransform  ;
	private Vector3 movement = new Vector3(0f,0.45f,10f) ;
	private float timer = 0.0f;
	private float random;
	public GameObject [] RearLights;

	void Start () {
		thisTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime);
		timer += Time.deltaTime;
		if ((action=="stop") && (speed < maxSpeed)&& speed > 0) {
			speed = speed - velocityD * Time.deltaTime;
			
		} else if ((action=="go") && (speed > -maxSpeed)) {
			speed = speed + velocityD * Time.deltaTime;
			
		}
		else {
			if (speed > velocityD * Time.deltaTime)
				speed = speed - velocityD * Time.deltaTime;
			else if (speed < -velocityD * Time.deltaTime)
				speed = speed + velocityD * Time.deltaTime;
			else
				speed = 0;
		}
		thisTransform.position = movement;
		movement.z = transform.position.z + speed * Time.deltaTime;
		random = Random.Range (0.0f, 10f);
		if (random > 5f) {
			if ((action == "stop") && (speed <=0)) {
				action = "go";
				SwitchHalo(false);
			};
			if (action =="go" && speed > 10f){
				action = "stop";
				SwitchHalo(true);
			}
		}
	}
	
	private void SwitchHalo(bool On)
	{
		Component halo = RearLights [0].GetComponent ("Halo");
		Component halo2 = RearLights [1].GetComponent ("Halo");
		
		if (On)
		{
			halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
			halo2.GetType ().GetProperty ("enabled").SetValue (halo2, true, null);
		}
		else
		{
			halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
			halo2.GetType ().GetProperty ("enabled").SetValue (halo2, false, null);
		}
	}
}