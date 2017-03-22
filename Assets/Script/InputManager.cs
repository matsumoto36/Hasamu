using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInput {
	Player_Left,
	Player_Right,
	Player_Up,
	Player_Down,
	Player_Attack,
}

public class InputManager : MonoBehaviour {

	static bool[] input;

	// Use this for initialization
	void Start () {
		input = new bool[10];
	}
	
	// Update is called once per frame
	void Update () {

		input[0] = Input.GetKey(KeyCode.A);
		input[1] = Input.GetKey(KeyCode.D);
		input[2] = Input.GetKey(KeyCode.W);
		input[3] = Input.GetKey(KeyCode.S);
		input[4] = Input.GetKey(KeyCode.LeftShift);

		input[5] = Input.GetKey(KeyCode.LeftArrow);
		input[6] = Input.GetKey(KeyCode.RightArrow);
		//input[7] = Input.GetKey(KeyCode.UpArrow);
		//input[8] = Input.GetKey(KeyCode.DownArrow);
		input[9] = Input.GetKey(KeyCode.RightShift);

	}

	/// <summary>
	/// 入力を取得
	/// </summary>
	/// <param name="playerID">プレイヤーのID</param>
	/// <param name="input">入力</param>
	/// <returns>あるかどうか</returns>
	public static bool GetInput(int playerID, PlayerInput playerInput) {
		return input[(playerID * 5) + (int)playerInput];
	}
}
