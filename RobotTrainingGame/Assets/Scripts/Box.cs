using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour {
	[SerializeField] float minForce;
	[SerializeField] float maxForce;

	Rigidbody _rb;

	private void Start() {
		_rb = GetComponent<Rigidbody>();
	}

	public bool TryCapture(float force) {
		if (force < minForce) return false;

		if (force > maxForce) {
			// lose
		}

		_rb.isKinematic = true;
		return true;
	}

	public void Throw(Vector3 force) {
		_rb.isKinematic = false;
		_rb.AddForce(force);
	}
}
