using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {
	public void PrintEvent(string s) {
		Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
	}
}
