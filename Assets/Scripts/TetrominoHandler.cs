using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoHandler : MonoBehaviour {
	[SerializeField]
	private float fallSpeed = 1.0f;
	[SerializeField]
	private bool allowRotation = true;
	[SerializeField]
		private bool limitRotation = false;
	private float fall = 0.0f;

	private GamePlayManager gamePlayManager;
	private void Start()
	{
		gamePlayManager = FindObjectOfType<GamePlayManager>();
	}
	private void Update()
	{
		UpdateTetromino();
		InputKeyboardHandler();
	}

	private void UpdateTetromino(){
		if(Time.time - fall >= fallSpeed){
			Handler("Down");
			fall = Time.time ;
		}
	}
	private void InputKeyboardHandler()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			Handler("Right");
		}else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Handler("Left");
		}else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
		Handler("Down");
		}else if(Input.GetKeyDown(KeyCode.UpArrow)){
			Handler("Action");
		}
	}

	private void Handler(string command)
	{
		switch (command)
		{
			case "Right" :
				//transform.position += Vector3.right;
				MoveHorizontal(Vector3.right);
				break;
			case "Left" :
				//transform.position += Vector3.left;
				MoveHorizontal(Vector3.left);
				break;
			case "Down" :
				//transform.position += Vector3.down;
				MoveVertical();
				break;
			case "Action" :
				//transform.Rotate(Vector3.forward*90); 
				if(allowRotation)
				{
					ActionLimitRotation(1);
					if(!IsInValidPosition())
						ActionLimitRotation(-1);
					else
						gamePlayManager.UpdateGrid(this);
				}
				
				break;
		}
	}
	private void ActionLimitRotation(int modifier){
		if(limitRotation){
			if(transform.rotation.eulerAngles.z >= 90)
				transform.Rotate(Vector3.forward*-90);
			else
				transform.Rotate(Vector3.forward*90);
		}	
		else transform.Rotate(Vector3.forward*90*modifier); 
	}
	private void MoveVertical()
	{
		transform.position += Vector3.down;
		if(!IsInValidPosition())
		{
			transform.position += Vector3.up;
			gamePlayManager.DestroyRow();
			enabled = false;
			if(gamePlayManager.isReactLimitGrid(this)) gamePlayManager.GameOver(this);
			else gamePlayManager.GenerateTetromino();
		}else{
			gamePlayManager.UpdateGrid(this);
		}
			
	} 
	private void MoveHorizontal(Vector3 direction)
	{
		transform.position += direction;
		if(!IsInValidPosition()) 
			transform.position += direction*-1;
		else
			gamePlayManager.UpdateGrid(this);
	}
	private bool IsInValidPosition()
	{
		foreach (Transform mino in transform)
		{
			Vector3 pos = gamePlayManager.Round( mino.position);
			if(!gamePlayManager.IsTetrominoInsideGrid(pos)) return false;
			if(gamePlayManager.GetTransformGridPosition(pos) != null &&
				gamePlayManager.GetTransformGridPosition(pos).parent != transform)
				{
					return false;
				}
		}
		return true;
	}
 
}

