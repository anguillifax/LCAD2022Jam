using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
	public static bool GetIfExists<T>(this Component c, out T obj) where T : Component {
		obj = c.GetComponent<T>();
		return obj != null;
	}

	public static bool GetIfExists<T>(this GameObject c, out T obj) where T : Component {
		obj = c.GetComponent<T>();
		return obj != null;
	}
}
