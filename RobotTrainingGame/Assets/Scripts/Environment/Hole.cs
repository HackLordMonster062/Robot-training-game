using UnityEngine;

public class Hole : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Box")) {
			GameManager.instance.DropBoxInHole();
		} else if (other.CompareTag("Player")) {
			GameManager.instance.FallInHole();
		}
	}
}
