using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

	enum ArmState {
		Push,
		Hit,
		Pull,
	}

	ArmState state = ArmState.Push;

	[SerializeField]
	Transform body;

	[SerializeField]
	Transform trackPoint;

	[SerializeField]
	Transform[] checkPoints;

	BoxCollider col;
	Animator anim;

	List<TestEnemy> enemyList = new List<TestEnemy>();
	List<Vector3> enemyOffset = new List<Vector3>();

	float pushSpeed = 3.0f;
	float pullSpeed = 1.5f;

	bool isPull = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.speed = pushSpeed;

		col = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {

		col.center = new Vector3(body.localPosition.x, 0, 0);
		float t = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

		switch(state) {
			case ArmState.Push:
				if(t >= 1.0f) Pull();
				break;
			case ArmState.Hit:
				Pull();
				break;
			case ArmState.Pull:
				if(t >= 1.0f) Destroy(gameObject);
				break;
		}

		for(int i = 0;i < enemyList.Count;i++) {
			//位置を調整
			enemyList[i].transform.position = trackPoint.position + enemyOffset[i];
		}
	}

	/// <summary>
	/// 引く動作を設定
	/// </summary>
	void Pull() {

		if(state == ArmState.Pull) return;

		float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
		Debug.Log("Pull " + time);

		anim.speed = pullSpeed;
		anim.CrossFade("Pull", 0, 0, (1 - time));

		state = ArmState.Pull;
	}

	/// <summary>
	/// ほかのArmの角度と自分の角度の差を取得
	/// </summary>
	/// <param name="otherArm">ほかの</param>
	/// <returns>角度の差 - 180</returns>
	float GetArmDir(Arm otherArm) {

		float deg = Mathf.Abs(180 - Vector3.Angle(transform.right, otherArm.transform.right));

		Debug.Log(deg);

		return deg;
	}

	void OnCollisionEnter(Collision other) {

		Debug.Log(other.collider.tag);
		Debug.Log("Hit");
		state = ArmState.Hit;

		if(other.collider.tag == "Enemy") {
			TestEnemy en = other.collider.GetComponent<TestEnemy>();
			en.isFree = false;
			en.GetComponent<Collider>().enabled = false;
			enemyList.Add(en);
			enemyOffset.Add(trackPoint.position - en.transform.position);

			state = ArmState.Push;
			return;
		}

		//引かなくてよいかチェック
		foreach(Transform p in checkPoints) {

			float length = transform.localScale.x + 1;
			Ray ray = new Ray(p.position, p.right);
			Debug.DrawLine(p.position, p.position + (p.right * length), Color.red, 1f);

			if(Physics.Raycast(ray, length)) {

				Debug.Log("Col > pull");

				//Armなら角度で引くスピードを上げる
				if(other.collider.tag == "Arm") {

					Arm otherArm = other.collider.GetComponent<Arm>();
					float dir = GetArmDir(otherArm);
					if(dir < 90.0f && otherArm.state != ArmState.Pull) {
						Debug.Log("SpeedUp");
						pullSpeed *= 6 * (1 - (dir / 90.0f)) + 1;
					}
				}

				//押している敵を倒す
				foreach(TestEnemy en in enemyList) {
					en.Death();
				}
				enemyList = new List<TestEnemy>();
				enemyOffset = new List<Vector3>();

				return;
			}
		}

		state = ArmState.Push;
	}
}
