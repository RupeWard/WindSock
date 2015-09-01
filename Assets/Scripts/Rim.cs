using UnityEngine;
using System.Collections;


public class Rim : MonoBehaviour {

	public AudioClip floorNoise;
	public AudioClip poleNoise;

	public Color nearColour;
	public Color farColour;

	void Start () 
	{
		WindManager.Instance.velocityChangedAction += HandleWindVelocity;
	}
	
	void Update () 
	{
	
	}

	void HandleWindVelocity(Vector3 v)
	{

	}


	void OnCollisionEnter(Collision col)
	{
		Debug.Log(gameObject.name+" hit "+col.gameObject.name);
	}
}
