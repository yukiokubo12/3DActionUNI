using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	Vector2 slideStartPosition;
	Vector2 prevPosition;
	Vector2 delta = Vector2.zero;
	bool moved = false;
	Animator animator;

	bool jump = true;

	void Start()
	{
		animator = GetComponent<Animator>();
	}
		
	void Update()
	{		
		// スライド開始地点.
		if (Input.GetButtonDown("Fire1"))
			slideStartPosition = GetCursorPosition();

		// 画面の１割以上移動させたらスライド開始と判断する.
		if (Input.GetButton("Fire1")) {
			if (Vector2.Distance(slideStartPosition,GetCursorPosition()) >= (Screen.width * 0.1f))
				moved = true;
		}
		
		// スライド操作が終了したか.
		if (!Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1"))
			moved = false; // スライドは終わった.
		
		// 移動量を求める.
		if (moved)
			delta = GetCursorPosition() - prevPosition;
		else
			delta = Vector2.zero;
		
		// カーソル位置を更新.
		prevPosition = GetCursorPosition();



	}
	
	// クリックされたか.
	public bool Clicked()
	{
		if (!moved && Input.GetButtonUp("Fire1"))
			return true;
		else
			return false;
	}	
	
	// スライド時のカーソルの移動量.
	public Vector2 GetDeltaPosition()
	{
		return delta;
	}
	
	// スライド中か.
	public bool Moved()
	{
		return moved;
	}
	
	public Vector2 GetCursorPosition()
	{
		return Input.mousePosition;
	}

	//ボタン移動
	public void ButtonMove()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			moved = true;
		}
	}
	//ジャンプボタン
	public bool JumpButton()
	{
		if(Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Jump"))
		{
				return true;
		}
		return false;
	}
	public bool RunButton()
	{
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Run"))
		{
				return true;
		}
		return false;
	}
	public bool AttackStanbyButton()
	{
		if(Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("AttackStanby"))
		{
			return true;
		}
		return false;
		// if(Input.GetKey(KeyCode.R) || Input.GetButton("AttackStanby"))
		// {
		// 	return false;
		// }
		// return true;
	}
}