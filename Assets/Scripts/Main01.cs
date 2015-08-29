using UnityEngine;
using System.Collections;

public class Main01 : MonoBehaviour {

	public Transform cameraHolder;

	public MeshRenderer[] xrendrrrs = new MeshRenderer[0];
	public MeshRenderer[] yrendrrrs = new MeshRenderer[0];
	public float speed = 0f;
	public float phase = 0f;

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

	public void OnCloseClicked()
	{
		Application.Quit ();
	}

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
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		phase += speed * Time.deltaTime;
		while (phase > 1f) 
		{
			phase -= 1f;
		}
		foreach (MeshRenderer rendrrr in yrendrrrs) 
		{
			if (rendrrr != null) {
				//			float p = rendrrr.material.GetTextureOffset("_MainTex").x;
				rendrrr.material.SetTextureOffset("_MainTex",new Vector2(0f,1f-phase));
			}
		}
		foreach (MeshRenderer rendrrr in xrendrrrs) 
		{
			if (rendrrr != null) {
				//			float p = rendrrr.material.GetTextureOffset("_MainTex").x;
				rendrrr.material.SetTextureOffset("_MainTex",new Vector2(1f-phase,0f));
			}
		}

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
