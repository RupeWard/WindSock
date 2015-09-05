using UnityEngine;
using System.Collections;

public class AppManager : SingletonSceneLifetime<AppManager> 
{

	public void OnCloseClicked()
	{
		Application.Quit ();
	}
	
}
