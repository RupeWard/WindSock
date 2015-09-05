using UnityEngine;
using System.Collections;


public class Rim : MonoBehaviour {

	public AudioClip floorNoise;
	public AudioClip poleNoise;
	public AudioClip coneNoise;
	public AudioClip ropeNoise;

	public Color nearColour;
	public Color farColour;

	public Transform touchFrom;
	public float touchForce = 1f;

	private MeshRenderer meshRenderer_;

	public void SetColours(Color c, Vector2 alphaRange)
	{
		nearColour = c;
		nearColour.a = alphaRange.x;
		farColour = c;
		farColour.a = alphaRange.y;

	}

	void Start () 
	{
		meshRenderer_ = GetComponent<MeshRenderer> ();
	}
	
	void Update () 
	{
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject == gameObject)
					{
						Debug.Log("You touched "+gameObject.name);
						Vector3 direction = transform.position - touchFrom.position;
						direction = direction/direction.magnitude;
						rigidbody.AddForce(direction * touchForce );
					}
				}
			}
		}
	}

	public void HandleClosenessFactor(float f)
	{
		Color c = Color.Lerp (nearColour, farColour, f);
		meshRenderer_.material.color = c;
		meshRenderer_.material.SetFloat ("_Alpha", c.a);
	}


	public void OnCollisionEnter(Collision col)
	{
		string clip = "none";
		if (col.gameObject.tag == "Floor") 
		{
			audio.PlayOneShot(floorNoise);
			clip="FLOOR";
		}
		else if (col.gameObject.tag == "Pole") 
		{
			audio.PlayOneShot(poleNoise);
			clip="POLE";
		}
		else if (col.gameObject.tag == "Rope") 
		{
			audio.PlayOneShot(ropeNoise);
			clip="ROPE";
		}
		else if (col.transform.parent != null && col.transform.parent.gameObject.tag == "Rope") 
		{
			audio.PlayOneShot(ropeNoise);
			clip="ROPENODE";
		}
		else if (col.gameObject.tag == "Cone") 
		{
			audio.PlayOneShot(coneNoise);
			clip="CONE";
		}

		if (AppSettings.DEBUG_AUDIO) 
		{
			Debug.Log(gameObject.name+" hit "+col.gameObject.name+" with tag '"+col.gameObject.tag+"', played "+clip);
		}
	}
}
