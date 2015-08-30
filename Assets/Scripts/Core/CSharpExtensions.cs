﻿using UnityEngine;
using System.Collections;

public static class CSharpExtensions
{
	#region Numeric
	public static bool EqualsApprox(this double d, double other, double tolerance) 
	{
		#if UNITY_EDITOR
		if (tolerance < 0)
		{
			Debug.LogWarning ("Negative tolerance!");
			tolerance *= -1;
		}
		#endif
		return System.Math.Abs (other-d) <= tolerance;
	}

	public static bool EqualsApprox(this float d, float other, float tolerance) 
	{
		#if UNITY_EDITOR
		if (tolerance < 0f)
		{
			Debug.LogWarning ("Negative tolerance!");
			tolerance *= -1f;
		}
		#endif
		return Mathf.Abs (other-d) <= tolerance;
	}

	public static bool GreaterThanApprox(this float d, float other, float tolerance) 
	{
		#if UNITY_EDITOR
		if (tolerance < 0f)
		{
			Debug.LogWarning ("Negative tolerance!");
			tolerance *= -1f;
		}
		#endif
		return d > (other+tolerance);
	}
	
	public static bool LessThanApprox(this float d, float other, float tolerance) 
	{
		#if UNITY_EDITOR
		if (tolerance < 0f)
		{
			Debug.LogWarning ("Negative tolerance!");
			tolerance *= -1f;
		}
		#endif
		return d < (other-tolerance);
	}

	public static bool EqualsApprox(this Vector3 v, Vector3 other, float tolerance)
	{
		#if UNITY_EDITOR
		if (tolerance < 0f)
		{
			Debug.LogWarning ("Negative tolerance!");
			tolerance *= -1f;
		}
		#endif
		return ( Vector3.Distance (v, other) < tolerance );
	}
	#endregion
	

}
