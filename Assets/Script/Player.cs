using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	static int playerCount = 0;
	static List<Player> playerList = new List<Player>();

	int playerID;

	Vector3 moveVec = new Vector3(0, 1, 0);
	Quaternion moveRot;
	GameObject body;
	Arm playerArm;

	float rotSpeed = 100;
	float moveSpeed = 5;

	[SerializeField]
	bool isAttack = false;

	// Use this for initialization
	void Start () {

		playerID = playerCount++;
		playerList.Add(this);

		body = transform.GetChild(0).gameObject;

		moveRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		//入力
		InputKey();
		//移動
		Move();
		//攻撃
		if(RCheck() && EnemyCheck()) Attack();

	}

	/// <summary>
	/// 線上に敵がいるかチェック
	/// </summary>
	/// <returns></returns>
	bool EnemyCheck() {
		RaycastHit hit;
		Vector3 pos = body.transform.position;
		Debug.DrawRay(pos, body.transform.right * 100);
		if(Physics.Raycast(pos, body.transform.right * 100, out hit)) {
			if(hit.collider.tag == "Enemy") {
				Debug.Log("Enemy");
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 攻撃の実行
	/// </summary>
	void Attack() {

		if(!playerArm) {
			GameObject g = Instantiate(Resources.Load<GameObject>("Arm"), body.transform.position + (body.transform.right * 0.55f), body.transform.rotation);
			g.transform.parent = body.transform;
			playerArm = g.GetComponent<Arm>();

		}

	}

	/// <summary>
	/// 対角線上にいるかチェック
	/// </summary>
	/// <returns></returns>
	bool RCheck() {

		float dir = Vector3.Angle(playerList[0].transform.right, playerList[1].transform.right);
		Debug.Log("dir " + dir);

		return 180 - Mathf.Abs(dir) < 10;
	}

	/// <summary>
	/// 移動系
	/// </summary>
	void Move() {
		transform.rotation *= moveRot;
		transform.parent.position += moveVec * moveSpeed * Time.deltaTime;
	}



	/// <summary>
	/// キー入力
	/// </summary>
	void InputKey() {

		//回転の入力
		float inputRot = InputManager.GetInput(playerID, PlayerInput.Player_Left) ? 1 : InputManager.GetInput(playerID, PlayerInput.Player_Right) ? -1 : 0;
		float rotSpd = Time.deltaTime * rotSpeed * inputRot;
		moveRot = Quaternion.AngleAxis(rotSpd, transform.up);

		//移動の入力
		float inputMove = InputManager.GetInput(playerID, PlayerInput.Player_Up) ? 1 : InputManager.GetInput(playerID, PlayerInput.Player_Down) ? -1 : 0;
		moveVec = new Vector3(0, inputMove, 0).normalized;

		//攻撃の入力
		isAttack = InputManager.GetInput(playerID, PlayerInput.Player_Attack);

	}

	/// <summary>
	/// デバッグ用描画
	/// </summary>
	void OnDrawGizmos() {

		Vector3 pos = transform.position - (moveVec * 0.5f);
		Ray r1 = new Ray(pos, moveVec);

		//上下の線
		Gizmos.color = Color.green;
		Gizmos.DrawRay(r1);

		int count = 32;
		float r = 5;

		List<Vector3> vertices = new List<Vector3>();
		for(int i = 0;i < count;i++) {

			float rad = 2 * Mathf.PI * ((float)i / count);
			float x = Mathf.Cos(rad) * r;
			float z = Mathf.Sin(rad) * r;
			vertices.Add(transform.position + new Vector3(x, 0, z));
		}
		vertices.Add(vertices[0]);

		Gizmos.color = Color.cyan;
		for(int i = 0;i < vertices.Count - 1;i++) {
			//周りの円
			Gizmos.DrawLine(vertices[i], vertices[i + 1]);
		}
	}
}
