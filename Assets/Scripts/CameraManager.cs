using UnityEngine;
using System.Collections;

public class CameraManager : SingletonSceneLifetime<CameraManager> {

	public Transform cameraHolder;
	public Transform cameraTransform;
	public Transform windConeTransform;
	public Transform viewTarget;

	public float maxCameraTurnSpeed = 1f;
	public float turnAcceleration = 1f;

	private float currentTurnSpeed_ = 0f;

	public float minDistance = 0.5f;
	public float maxDistance = 20f;
	public float zoomAcceleration = 1f;
	public float currentZoomSpeed = 0f;

	public float upDownSpeed = 1f;
	public float minY = 0f;
	public float maxY = 10f;

#region turn

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
#endregion turn

	#region updown
	
	private enum ECameraUpDownState
	{
		STILL,
		UP,
		DOWN
	}
	
	private ECameraUpDownState cameraUpDownState_ = ECameraUpDownState.STILL;
	
	public void OnUpPressed()
	{
		cameraUpDownState_ = ECameraUpDownState.UP;
	}
	
	public void OnUpReleased()
	{
		cameraUpDownState_ = ECameraUpDownState.STILL;
	}
	
	public void OnDownPressed()
	{
		cameraUpDownState_ = ECameraUpDownState.DOWN;
	}
	
	public void OnDownReleased()
	{
		cameraUpDownState_ = ECameraUpDownState.STILL;
	}
	#endregion updown
	
	#region zoom
	
	private enum ECameraZoomState
	{
		NONE,
		IN,
		OUT
	}
	
	private ECameraZoomState cameraZoomState_ = ECameraZoomState.NONE;
	
	public void OnInPressed()
	{
		cameraZoomState_ = ECameraZoomState.IN;
	}
	
	public void OnInReleased()
	{
		cameraZoomState_ = ECameraZoomState.NONE;
	}
	
	public void OnOutPressed()
	{
		cameraZoomState_ = ECameraZoomState.OUT;
	}
	
	public void OnOutReleased()
	{
		cameraZoomState_ = ECameraZoomState.NONE;
	}
#endregion turn
	

	void Start () 
	{
		cameraTransform.LookAt(viewTarget.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (cameraTurnState_) 
		{
			case ECameraTurnState.LEFT:
			{
				currentTurnSpeed_ -= Time.deltaTime * turnAcceleration;
				if (currentTurnSpeed_ < -1f * maxCameraTurnSpeed)
				{
					currentTurnSpeed_ = -1f * maxCameraTurnSpeed;
				}
				break;
			}
			case ECameraTurnState.RIGHT:
			{
				currentTurnSpeed_ += Time.deltaTime * turnAcceleration;
				if (currentTurnSpeed_ > maxCameraTurnSpeed)
				{
					currentTurnSpeed_ = maxCameraTurnSpeed;
				}
				break;
			}
			case ECameraTurnState.STILL:
			{
				currentTurnSpeed_ = 0f;
				break;
			}	
				
		}
		if (currentTurnSpeed_ != 0f) 
		{
			cameraHolder.RotateAround(Vector3.zero, Vector3.up, currentTurnSpeed_);
		}

		float currentDistance = Vector3.Distance (cameraTransform.position, windConeTransform.position);

		switch (cameraZoomState_) 
		{
		case ECameraZoomState.IN:
		{
			if (currentDistance > minDistance)
			{
				currentZoomSpeed += zoomAcceleration * Time.deltaTime;
			}
			else
			{
				currentZoomSpeed = 0f;
			}
			break;
		}
		case ECameraZoomState.OUT:
		{
			if (currentDistance < maxDistance)
			{
				currentZoomSpeed -= zoomAcceleration * Time.deltaTime;
			}
			else
			{
				currentZoomSpeed = 0f;
			}
			break;
		}
		case ECameraZoomState.NONE:
		{
			currentZoomSpeed = 0f;
			break;
		}	

		}
		if (currentZoomSpeed < 0f && cameraTransform.localPosition.y <= minY) 
		{
			currentZoomSpeed = 0f;
		}
		if (currentZoomSpeed != 0f)
		{

			cameraTransform.position 
				= Vector3.MoveTowards(cameraTransform.position, 
				                      windConeTransform.position,
				                      currentZoomSpeed);
		}

		float currentHeight = cameraTransform.localPosition.y;
		switch (cameraUpDownState_) 
		{
		case ECameraUpDownState.UP:
		{
			if (currentHeight < maxY)
			{
				currentHeight += upDownSpeed * Time.deltaTime;
			}
			break;
		}
		case ECameraUpDownState.DOWN:
		{
			if (currentHeight > minY)
			{
				currentHeight -= upDownSpeed * Time.deltaTime;
			}
			break;
		}
		case ECameraUpDownState.STILL:
		{
			break;
		}	
			
		}
		cameraTransform.SetLocalYPosition (currentHeight);
		if (currentZoomSpeed != 0f)
		{
			cameraTransform.position 
				= Vector3.MoveTowards(cameraTransform.position, 
				                      windConeTransform.position,
				                      currentZoomSpeed);
		}

		cameraTransform.LookAt(viewTarget.position);			

		/*
		float z = cameraTransform.localPosition.z;
		switch (cameraZoomState_) 
		{
		case ECameraZoomState.IN:
		{
			z = z - cameraZoomSpeed * Time.deltaTime;
			if (z < minDistance)
			{
				z = minDistance;
			}
			break;
		}
		case ECameraZoomState.OUT:
		{
			z = z + cameraZoomSpeed * Time.deltaTime;
			if (z > maxDistance)
			{
				z = maxDistance;
			}
			break;
		}
		case ECameraZoomState.NONE:
		{
			break;
		}	
			
		}
		if (z != cameraTransform.localPosition.z) 
		{
			cameraTransform.SetLocalZPosition(z);
		}
		*/

	}
}
