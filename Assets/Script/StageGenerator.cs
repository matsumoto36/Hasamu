using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {
	
	const int StageSize = 1;
	const int KeepStage = 100;

	GameObject stagePre;
	GameObject player;

	int genStageCount = 0;

	List<GameObject> genStageList;

	// Use this for initialization
	void Start () {

		genStageList = new List<GameObject>();

		stagePre = Resources.Load<GameObject>("Stage");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(player.transform.position.y + (KeepStage / 2) > genStageCount) {
			GenerateStage();
		}
	}

	void GenerateStage() {

		while(player.transform.position.y + (KeepStage / 2) > genStageCount) {
			genStageList.Add(
				Instantiate(stagePre, new Vector3(0, genStageCount++, 0), Quaternion.Euler(90, 0, 0)));

			if(genStageList.Count > KeepStage) DeleteOldStage();
		}

	}

	void DeleteOldStage() {
		GameObject o = genStageList[0];
		genStageList.RemoveAt(0);
		Destroy(o);
	}
}
