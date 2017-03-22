using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

	public float speed = 5;

	public bool isFree = true;

	// Use this for initialization
	void Start () {

		transform.position = new Vector3(Random.Range(-4, 4), 10, Random.Range(-4, 4));
	}
	
	// Update is called once per frame
	void Update () {

		if(isFree) transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
	}

	void OnCollisionEnter(Collision other) {
		//Destroy(gameObject);
	}

	public void Death() {
		Destroy(gameObject);
	}
}
