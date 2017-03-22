using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	GameObject enemy;

	[SerializeField]
	float speed = 5;

	// Use this for initialization
	void Start () {
		enemy = Resources.Load<GameObject>("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.U)) {
			GameObject g = Instantiate(enemy);
			g.GetComponent<TestEnemy>().speed = speed;
		}
	}
}
