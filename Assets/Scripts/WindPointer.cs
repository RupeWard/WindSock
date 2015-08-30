using UnityEngine;
using System.Collections;

public class WindPointer : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		WindManager.Instance.velocityChangedAction += HandleVelocityChanged;
	}

	public void HandleVelocityChanged(Vector3 v)
	{

	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
