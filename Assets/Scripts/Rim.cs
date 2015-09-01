using UnityEngine;
using System.Collections;


public class Rim : MonoBehaviour {

	public AudioClip floorNoise;
	public AudioClip poleNoise;

	public Color nearColour;
	public Color farColour;

	private MeshRenderer meshRenderer_;

	void Start () 
	{
		meshRenderer_ = GetComponent<MeshRenderer> ();
	}
	
	void Update () 
	{
	
	}

	public void HandleClosenessFactor(float f)
	{
		meshRenderer_.material.color = Color.Lerp (nearColour, farColour, f);
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(gameObject.name+" hit "+col.gameObject.name);
	}
}
