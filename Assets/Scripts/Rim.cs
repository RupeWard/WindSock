﻿using UnityEngine;
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
		Debug.Log(gameObject.name+" hit "+col.gameObject.name);
		if (col.gameObject.tag == "Floor") 
		{
			audio.clip = floorNoise;
			audio.Play();
		}
		else if (col.gameObject.tag == "Pole") 
		{
			audio.clip = poleNoise;
			audio.Play();
		}
		else if (col.gameObject.tag == "Rope") 
		{
			audio.clip = ropeNoise;
			audio.Play();
		}
		else if (col.gameObject.tag == "Cone") 
		{
			audio.clip = coneNoise;
			audio.Play();
		}
	}
}