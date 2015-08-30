using UnityEngine;
using System.Collections;

public class Main01 : MonoBehaviour {

	public Transform cameraHolder;

	public MeshRenderer[] xrendrrrs = new MeshRenderer[0];
	public MeshRenderer[] yrendrrrs = new MeshRenderer[0];
	public float speed = 0f;
	public float phase = 0f;


	public void OnCloseClicked()
	{
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		phase += speed * Time.deltaTime;
		while (phase > 1f) 
		{
			phase -= 1f;
		}
		foreach (MeshRenderer rendrrr in yrendrrrs) 
		{
			if (rendrrr != null) {
				//			float p = rendrrr.material.GetTextureOffset("_MainTex").x;
				rendrrr.material.SetTextureOffset("_MainTex",new Vector2(0f,1f-phase));
			}
		}
		foreach (MeshRenderer rendrrr in xrendrrrs) 
		{
			if (rendrrr != null) {
				//			float p = rendrrr.material.GetTextureOffset("_MainTex").x;
				rendrrr.material.SetTextureOffset("_MainTex",new Vector2(1f-phase,0f));
			}
		}


	}
}
