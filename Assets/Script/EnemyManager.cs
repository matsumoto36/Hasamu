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
		StartCoroutine(GenerateOnce(1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.U)) {
		//	GameObject g = Instantiate(enemy);
		//	g.GetComponent<TestEnemy>().speed = speed;
		//}
	}

	void Generate() {
		GameObject g = Instantiate(enemy);
		g.GetComponent<TestEnemy>().speed = speed;
	}

	IEnumerator GenerateOnce(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Generate();
		StartCoroutine(GenerateOnce(Random.Range(0.5f, 1.5f)));

	}
}
