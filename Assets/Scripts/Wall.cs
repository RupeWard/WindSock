using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
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
