using UnityEngine;
using System.Collections;

public class LeafSpawner : MonoBehaviour 
{
	public float forceMult = 100f;
	public static float sForceMult;

	public GameObject leafPrefab;

	public Vector2 frequencyRange =  new Vector2(1f, 5f);

	void Start () 
	{
		StartCoroutine (SpawnCR ());
	}
	
	void Update () 
	{
		sForceMult = forceMult;
	
	}

	private int leafNum = 0;
	private IEnumerator SpawnCR()
	{
		float wait = UnityEngine.Random.Range (frequencyRange.x, frequencyRange.y);
		yield return new WaitForSeconds(wait);

		GameObject go = Instantiate (leafPrefab) as GameObject;
		Leaf leaf = go.GetComponent< Leaf > ();
		go.transform.position = transform.position;
		go.name = "Leaf_" + leafNum;
		leafNum++;

		StartCoroutine (SpawnCR ());
	}
}
