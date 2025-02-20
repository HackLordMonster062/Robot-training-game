using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotView : MonoBehaviour {
    [SerializeField] ParticleSystem collisionEffect;
	[SerializeField] LayerMask wallsLayer;

	private void OnCollisionEnter(Collision collision) {
		if (collisionEffect != null && (1 << collision.gameObject.layer & wallsLayer) > 0) {
			collisionEffect.transform.position = collision.contacts[0].point;
			collisionEffect.Play();
		}
	}
}
