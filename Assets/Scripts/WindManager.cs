using UnityEngine;
using System.Collections;

public class WindManager : SingletonSceneLifetime<WindManager> 
{
	public float maxWindSpeed = 100f;

	public WindPointer windPointer;

	public System.Action<Vector3> velocityChangedAction;
	public System.Action<float> speedChangedAction;

	public Transform windContainer;

	private Vector3 lastDirection = Vector3.left;


	public Vector3 CurrentWindVelocity
	{
		get { return currentWindVelocity_; }
	}
	public Vector3 CurrentWindAt(Vector3 pos)
	{
		// TDOD
		return CurrentWindVelocity;
	}
	public float CurrentWindSpeed
	{
		get { return currentWindVelocity_.magnitude; }
	}
	public Vector3 CurrentWindDirection
	{
		get { return currentWindVelocity_ / currentWindVelocity_.magnitude; }
	}

	private Vector3 currentWindVelocity_ = Vector3.zero;
	private void SetVelocity(Vector3 v)
	{
		currentWindVelocity_ = v;
		if (velocityChangedAction != null) 
		{
			velocityChangedAction(CurrentWindVelocity);
		}
		if (speedChangedAction != null) 
		{
			speedChangedAction(CurrentWindSpeed);
		}
	}
	private void SetSpeed(float f)
	{
		if (CurrentWindSpeed > 0) 
		{
			SetVelocity (f * CurrentWindVelocity / CurrentWindSpeed);
		} 
		else 
		{
			SetVelocity (f * lastDirection);
		}
	}

	public void IncreaseWindSpeed(float f)
	{
		float newMagnitude = CurrentWindSpeed + f;
		if (newMagnitude > maxWindSpeed)
		{
			newMagnitude = maxWindSpeed;
		}
		SetVelocity( newMagnitude * CurrentWindDirection);
	}

	public void DecreaseWindSpeed(float f)
	{
		float newMagnitude = CurrentWindSpeed - f;
		if (newMagnitude < 0f)
		{
			newMagnitude =0f;
		}
		SetVelocity(newMagnitude * CurrentWindDirection);
	}

	public void RotateWind(Vector3 axis)
	{
		windContainer.transform.rotation = Quaternion.Euler (axis.x, axis.y /* + Mathf.PI / 2f */, axis.z) * windContainer.transform.rotation;
		SetVelocity(Quaternion.Euler(axis.x, axis.y, axis.z) * currentWindVelocity_);
	}

	private enum ESpeedChangeState
	{
		NONE,
		UP,
		DOWN
	}
	
	private ESpeedChangeState speedChangeState_ = ESpeedChangeState.NONE;

	private bool doubleSpeed = false;
	public float doubleUp = 10f;

	public void OnDoubleUpPressed()
	{
		speedChangeState_ = ESpeedChangeState.UP;
		doubleSpeed = true;
	}
	
	public void OnDoubleReleased()
	{
		speedChangeState_ = ESpeedChangeState.NONE;
	}
	

	public void OnUpPressed()
	{
		speedChangeState_ = ESpeedChangeState.UP;
	}
	
	public void OnUpReleased()
	{
		speedChangeState_ = ESpeedChangeState.NONE;
	}
	
	public void OnDownPressed()
	{
		speedChangeState_ = ESpeedChangeState.DOWN;
	}
	
	public void OnDownReleased()
	{
		speedChangeState_ = ESpeedChangeState.NONE;
	}

	public void OnKillClicked()
	{
		speedChangeState_ = ESpeedChangeState.NONE;
		SetSpeed (0f);
	}

	public float speedChangeAcceleration = 1f;

	private enum EDirectionChangeState
	{
		NONE,
		LEFT,
		RIGHT
	}
	
	private EDirectionChangeState directionChangeState_ = EDirectionChangeState.NONE;
	
	
	public void OnLeftPressed()
	{
		directionChangeState_ = EDirectionChangeState.LEFT;
	}
	
	public void OnLeftReleased()
	{
		directionChangeState_ = EDirectionChangeState.NONE;
	}
	
	public void OnRightPressed()
	{
		directionChangeState_ = EDirectionChangeState.RIGHT;
	}
	
	public void OnRightReleased()
	{
		directionChangeState_ = EDirectionChangeState.NONE;
	}
	
	public float directionChangeAcceleration = 1f;
	

	// Update is called once per frame
	void Update () 
	{
		float newmag = CurrentWindSpeed;
		switch (speedChangeState_) 
		{
			case ESpeedChangeState.UP:
			{
				float factor = (doubleSpeed)?(doubleUp):(1f);
				newmag += factor * Time.deltaTime * speedChangeAcceleration;
				if (maxWindSpeed > 0f && newmag > maxWindSpeed)
				{
					newmag = maxWindSpeed;
				}
				break;
			}
			case ESpeedChangeState.DOWN:
			{
				newmag -= Time.deltaTime * speedChangeAcceleration;
				if (newmag < 0f)
				{
					newmag = 0f;
				}
				break;
			}
			case ESpeedChangeState.NONE:
			{
				break;
			}					
		}
		if (newmag != CurrentWindSpeed) 
		{
			SetSpeed(newmag);
		
		}
		switch (directionChangeState_) 
		{
			case EDirectionChangeState.LEFT:
			{
				RotateWind(Vector3.up * directionChangeAcceleration * Time.deltaTime); 
				break;
			}
			case EDirectionChangeState.RIGHT:
			{
				RotateWind(-1f * Vector3.up * directionChangeAcceleration * Time.deltaTime); 
				break;
			}
			case EDirectionChangeState.NONE:
			{
				break;
			}
		}
	}

}
