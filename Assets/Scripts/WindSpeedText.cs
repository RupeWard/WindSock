using UnityEngine;
using System.Collections;

public class WindSpeedText : MonoBehaviour 
{

	public UnityEngine.UI.Text windSpeedText;

	public void SetSpeed(float s)
	{
		if (windSpeedText != null) 
		{
			windSpeedText.text = s.ToString();
		}
	}

	void Start () 
	{
		WindManager.Instance.speedChangedAction += SetSpeed ;
	}
	
}
