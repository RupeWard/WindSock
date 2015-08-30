using UnityEngine;
using System.Collections;

public class CameraManager : SingletonApplicationLifetime<CameraManager> {

	public Transform cameraHolder;

	public float maxCameraTurnSpeed = 1f;
	public float turnAcceleration = 1f;

	private float currentTurnSpeed = 0f;

	private enum ECameraTurnState
	{
		STILL,
		LEFT,
		RIGHT
	}

	private ECameraTurnState cameraTurnState_ = ECameraTurnState.STILL;

	public void OnLeftPressed()
	{
		cameraTurnState_ = ECameraTurnState.LEFT;
	}

	public void OnLeftReleased()
	{
		cameraTurnState_ = ECameraTurnState.STILL;
	}
	
	public void OnRightPressed()
	{
		cameraTurnState_ = ECameraTurnState.RIGHT;
	}
	
	public void OnRightReleased()
	{
		cameraTurnState_ = ECameraTurnState.STILL;
	}
	

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (cameraTurnState_) 
		{
			case ECameraTurnState.LEFT:
			{
				currentTurnSpeed -= Time.deltaTime * turnAcceleration;
				if (currentTurnSpeed < -1f * maxCameraTurnSpeed)
				{
					currentTurnSpeed = -1f * maxCameraTurnSpeed;
				}
				break;
			}
			case ECameraTurnState.RIGHT:
			{
				currentTurnSpeed += Time.deltaTime * turnAcceleration;
				if (currentTurnSpeed > maxCameraTurnSpeed)
				{
					currentTurnSpeed = maxCameraTurnSpeed;
				}
				break;
			}
			case ECameraTurnState.STILL:
			{
				currentTurnSpeed = 0f;
				break;
			}	
				
		}
		if (currentTurnSpeed != 0f) 
		{
			cameraHolder.RotateAround(Vector3.zero, Vector3.up, currentTurnSpeed);
		}

	}
}
