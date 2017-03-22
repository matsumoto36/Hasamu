using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	[SerializeField] Transform target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {

		if(!target) return;
		transform.position = Vector3.Lerp(transform.position, target.position, 0.3f);
	}
}
