using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour {
	[SerializeField] float minForce;
	[SerializeField] float maxForce;

	Rigidbody _rb;
	Collider _collider;

	private void Start() {
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	public bool TryCapture(float force) {
		if (force < minForce) return false;

		if (force > maxForce) {
			GameManager.instance.DestroyBox();
		}

		_rb.isKinematic = true;
		_collider.enabled = false;
		
		return true;
	}

	public void Throw(Vector3 force) {
		_rb.isKinematic = false;
		_collider.enabled = true;
		_rb.AddForce(force, ForceMode.Impulse);
	}
}
