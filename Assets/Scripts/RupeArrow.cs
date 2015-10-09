using UnityEngine;
using System.Collections;

public class RupeArrow : MonoBehaviour 
{
	public float speed = 0f;
	public float phase = 0f;
	public MeshRenderer meshRenderer;

	public bool doX = false;
	public bool doY = false;

	public float speedFactor = 1f;

	public Transform cameraTransform;

	public Transform myLight;

	// Use this for initialization
	void Start () 
	{
	}

	void HandleWindSpeedChange(float s)
	{
		speed = speedFactor * s;
	}

	// Update is called once per frame
	void Update () 
	{
		phase += speed * Time.deltaTime;
		while (phase > 1f) 
		{
			phase -= 1f;
		}
		Vector2 offset = Vector2.zero;
		if (doX)
		{
			offset.x = 1f-phase;
		}
		if (doY)
		{
			offset.y = 1f-phase;
		}
//		if (offset.magnitude != 0f) 
		{
			meshRenderer.material.SetTextureOffset("_MainTex",offset);
		}

		meshRenderer.material.SetFloat ("_AlphaMin", 1f);
		meshRenderer.material.SetFloat ("_AlphaMax", 1f);

	}

}
