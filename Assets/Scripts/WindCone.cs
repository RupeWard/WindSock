using UnityEngine;
using System.Collections;

public class WindCone : MonoBehaviour 
{
	public float speed = 0f;
	public float phase = 0f;
	public MeshRenderer meshRenderer;

	public bool doX = false;
	public bool doY = false;

	public float speedFactor = 1f;

	public Rim[] rims = new Rim[2];

	public Transform cameraTransform;

	private float rimSeparation_;

	public Rigidbody[] windForceReceivers = new Rigidbody[0];

	// Use this for initialization
	void Start () 
	{
		rimSeparation_ = Vector3.Distance (rims [0].transform.position, rims [1].transform.position);
		WindManager.Instance.speedChangedAction += HandleWindSpeedChange;
	}

	void HandleWindSpeedChange(float s)
	{
		speed = speedFactor * s;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (WindManager.Instance.CurrentWindSpeed > 0f) 
		{
			foreach (Rigidbody r in windForceReceivers)
			{
				r.AddForce (WindManager.Instance.CurrentWindVelocity);		
			}
		}
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

		float r0dist = Vector3.Distance (rims [0].transform.position, cameraTransform.position);
		float r1dist = Vector3.Distance (rims [1].transform.position, cameraTransform.position);

		float[] fractions =  new float[2]{-1f,-1f};

		float rdiff = r0dist - r1dist;

		if (rdiff >= 0) 
		{
			fractions[0] = Mathf.Lerp(0.5f, 1f, rdiff/rimSeparation_);
			fractions[1] = Mathf.Lerp(0.5f, 0f, rdiff/rimSeparation_);
		} 
		else 
		{
			fractions[1] = Mathf.Lerp(0.5f, 1f, -1f*rdiff/rimSeparation_);
			fractions[0] = Mathf.Lerp(0.5f, 0f, -1f*rdiff/rimSeparation_);
		}
		for (int i = 0; i<2; i++) 
		{
			if (fractions[i] >= 0f) 
			{
				rims[i].HandleClosenessFactor(fractions[i]);
			}
		}
	}

}
