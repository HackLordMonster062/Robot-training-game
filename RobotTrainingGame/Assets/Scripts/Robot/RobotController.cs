using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animation))]
public class RobotController : MonoBehaviour {
    [SerializeField] float baseSpeed;
    [SerializeField] float baseRotationSpeed;
    [SerializeField] float baseDuration;

	[Space]
    [SerializeField] float pickupDistance;
    [SerializeField] LayerMask boxLayer;

	public event System.Action startAction;
	public event System.Action endAction;

    public bool IsActive { get; private set; }

    Rigidbody _rb;
	Animator _animator;

    void Start() {
        _rb = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();
    }

    public void GoForward(float amplitude, float time) {
        StartCoroutine(MoveCoroutine(amplitude * baseSpeed, time * baseDuration));
    }

    public void TurnLeft(float amplitude, float time) {
		StartCoroutine(RotateCoroutine(-amplitude * baseRotationSpeed, time * baseDuration));
    }

    public void TurnRight(float amplitude, float time) {
		StartCoroutine(RotateCoroutine(amplitude * baseRotationSpeed, time * baseDuration));
	}

    public void Pickup(float amplitude) {
		_animator.SetTrigger("Pickup");
    }

    public void Drop(float amplitude) {
		_animator.SetTrigger("Pickup");
	}

	public void PickBox(int _) {
		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit info, pickupDistance, boxLayer)) {

		}
	}

    IEnumerator MoveCoroutine(float speed, float duration) {
		float elapsedTime = 0f;
		float halfDuration = duration / 2f;

		startAction?.Invoke();
		_animator.SetTrigger("StartMoving");

		while (elapsedTime < duration) {
			elapsedTime += Time.fixedDeltaTime;
			float t = elapsedTime < halfDuration
				? elapsedTime / halfDuration
				: (duration - elapsedTime) / halfDuration;

			float currentSpeed = speed * Mathf.Clamp01(t);
			_rb.velocity = transform.forward * currentSpeed;

			yield return new WaitForFixedUpdate();
		}

        endAction?.Invoke();
	}

	IEnumerator RotateCoroutine(float rotateSpeed, float duration) {
		float elapsedTime = 0f;
		float halfDuration = duration / 2f;

		startAction?.Invoke();
		_animator.SetBool("Spinning", true);

		while (elapsedTime < duration) {
			elapsedTime += Time.fixedDeltaTime;
			float t = elapsedTime < halfDuration
				? elapsedTime / halfDuration
				: (duration - elapsedTime) / halfDuration;

			float currentSpeed = rotateSpeed * Mathf.Clamp01(t);
			transform.Rotate(transform.up, currentSpeed * Time.fixedDeltaTime);

			yield return new WaitForFixedUpdate();
		}

		endAction?.Invoke();
		_animator.SetBool("Spinning", false);
	}
}
