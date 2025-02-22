using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotView : MonoBehaviour {
    [SerializeField] ParticleSystem collisionEffect;
	[SerializeField] LayerMask wallsLayer;

	Rigidbody _rb;

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision) {
		if (collisionEffect != null && (1 << collision.gameObject.layer & wallsLayer) > 0) {
			collisionEffect.transform.position = collision.contacts[0].point;
			collisionEffect.Play();

			GameManager.instance.HitWalls(_rb.velocity.magnitude);
		}
	}
}
