using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour 
{
	public ParticleSystem partSystem;

	public enum ActiveWhen
	{
		xPos,
		zPos,
		xNeg,
		zNeg
	}

	public ActiveWhen activeWhen;

	public float emissionRate = 25f;

	void Start () 
	{
		WindManager.Instance.velocityChangedAction += HandleWindVelocity;
		HandleWindVelocity (WindManager.Instance.CurrentWindVelocity);
	}
	
	void Update () 
	{
	
	}

	public void HandleWindVelocity(Vector3 v)
	{
		// for wall b, want to set active when v.x > 0f
		// wall b at -10 7 0, rot 0 270 0
		// for wall a, want to set active when v.x < 0f
		// 
		bool active = false;
		switch (activeWhen) 
		{
			case ActiveWhen.xPos:
			{
				if (v.x > 0f) 
				{
					active = true;
				}
				break;
			}
			case ActiveWhen.xNeg:
			{
				if (v.x < 0f) 
				{
					active = true;
				}
				break;
			}
			case ActiveWhen.zPos:
			{
				if (v.z > 0f) 
				{
					active = true;
				}
				break;
			}
			case ActiveWhen.zNeg:
			{
				if (v.z < 0f) 
				{	
					active = true;
				}
				break;
			}
		}
		partSystem.gameObject.SetActive(true);
		partSystem.emissionRate = (active) ? (emissionRate) : (0);
	}

	public void OnTriggerEnter(Collider col)
	{
		Debug.Log (gameObject.name + " OnTriggerEnter " + col.gameObject.name);
	}
	public void OnTriggerExit(Collider col)
	{
		Debug.Log (gameObject.name + " OnTriggerExit " + col.gameObject.name);
	}
}
