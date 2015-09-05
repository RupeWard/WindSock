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

	public Color[] colourOptions = new Color[1];
	private int coneColourIndex_ = 0;
	private int rimColourIndex_ = 0;
	public UnityEngine.UI.Image coneColourButtonImage;
	public UnityEngine.UI.Image rimColourButtonImage;

	public Transform cameraTransform;

	public Transform myLight;

	private float rimSeparation_;

	public Rigidbody[] windForceReceivers = new Rigidbody[0];

	public void OnConeColourClicked()
	{
		coneColourIndex_++;
		if (coneColourIndex_ >= colourOptions.Length) 
		{
			coneColourIndex_ = 0;
		}
		OnConeColourChanged ();
	}

	public void OnRimColourClicked()
	{
		rimColourIndex_++;
		if (rimColourIndex_ >= colourOptions.Length) 
		{
			rimColourIndex_ = 0;
		}
		OnRimColourChanged ();
	}

	private void OnConeColourChanged()
	{
		coneColourButtonImage.color = colourOptions [coneColourIndex_];
		meshRenderer.material.color = colourOptions [coneColourIndex_];
	}

	private void OnRimColourChanged()
	{
		rimColourButtonImage.color = colourOptions [rimColourIndex_];
		for (int i = 0; i<2; i++)
		{
			rims[i].SetColours(colourOptions[rimColourIndex_], new Vector2(0f,1f));
		}
	}

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
//		meshRenderer.material.SetFloat ("_AlphaMin", fractions [0]);
//		meshRenderer.material.SetFloat ("_AlphaMax", fractions [1]);
		float fmin = (rdiff + rimSeparation_) / (2f * rimSeparation_);
		float fmax = 1f-fmin;
		meshRenderer.material.SetFloat ("_AlphaMin", fmax);
		meshRenderer.material.SetFloat ("_AlphaMax", fmin);

		if (myLight != null) 
		{
			myLight.LookAt(meshRenderer.transform.position);
		}
	}

}
