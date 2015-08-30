using UnityEngine;
using System.Collections;

public class AppManager : SingletonApplicationLifetime<AppManager> 
{

	public void OnCloseClicked()
	{
		Application.Quit ();
	}
	
}
