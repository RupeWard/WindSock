using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
		if (transform.position.y < -0.5f) 
		{
			GameObject.Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		rigidbody.AddForce (WindManager.Instance.CurrentWindVelocity);		
	}
}
