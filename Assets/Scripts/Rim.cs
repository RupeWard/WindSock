using UnityEngine;
using System.Collections;


public class Rim : MonoBehaviour {

	public AudioClip floorNoise;
	public AudioClip poleNoise;
	public AudioClip coneNoise;
	public AudioClip ropeNoise;

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

	public void OnCollisionEnter(Collision col)
	{
		Debug.Log(gameObject.name+" hit "+col.gameObject.name+" with tag '"+col.gameObject.tag+"'");
		if (col.gameObject.tag == "Floor") 
		{
			audio.PlayOneShot(floorNoise);
		}
		else if (col.gameObject.tag == "Pole") 
		{
			audio.PlayOneShot(poleNoise);
		}
		else if (col.gameObject.tag == "Rope") 
		{
			audio.PlayOneShot(ropeNoise);
		}
		else if (col.gameObject.tag == "Cone") 
		{
			audio.PlayOneShot(coneNoise);
		}
	}
}
