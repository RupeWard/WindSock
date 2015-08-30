using UnityEngine;
using System.Collections;

public class WindCone : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (WindManager.Instance.CurrentWindSpeed > 0f) 
		{
			rigidbody.AddForce (WindManager.Instance.CurrentWindVelocity);		
		}
	}
}
