using UnityEngine;

public class Pipes : MonoBehaviour {
	private void OnCollisionEnter(Collision collision) {
		GameManager.instance.HitEnvironemnt();
	}
}
