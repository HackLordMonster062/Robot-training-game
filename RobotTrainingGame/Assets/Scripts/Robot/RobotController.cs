using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotController : MonoBehaviour {
    [SerializeField] AnimationCurve curve;
    [SerializeField] float baseSpeed;
    [SerializeField] float baseRotationSpeed;
    [SerializeField] float baseDuration;

	public event System.Action startAction;
	public event System.Action endAction;

    public bool IsActive { get; private set; }

    Rigidbody _rb;

    void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        
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

    }

    public void Drop(float amplitude) {

    }

    IEnumerator MoveCoroutine(float speed, float duration) {
		float elapsedTime = 0f;
		float halfDuration = duration / 2f;

		startAction?.Invoke();

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
	}
}
