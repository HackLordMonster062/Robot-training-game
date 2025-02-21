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
    [SerializeField] Transform holdingPoint;
    [SerializeField] float pickupDistance;
    [SerializeField] LayerMask boxLayer;

	public event System.Action startAction;
	public event System.Action endAction;

    public bool IsActive { get; private set; }

    Rigidbody _rb;
	Animator _animator;

	Box _box = null;

	int _pickupSpeedID;
	int _dropForceID;
	int _pickupID;
	int _startMovingID;
	int _spinningID;

    void Start() {
        _rb = GetComponent<Rigidbody>();
		_animator = GetComponent<Animator>();

		_pickupSpeedID = Animator.StringToHash("PickupSpeed");
		_dropForceID = Animator.StringToHash("DropForce");
		_pickupID = Animator.StringToHash("Pickup");
		_startMovingID = Animator.StringToHash("StartMoving");
		_spinningID = Animator.StringToHash("Spinning");
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
		startAction?.Invoke();
		_animator.SetFloat(_pickupSpeedID, amplitude);
		_animator.SetTrigger(_pickupID);
    }

    public void Drop(float amplitude) {
		_animator.SetFloat(_dropForceID, amplitude);
		_animator.SetTrigger(_pickupID);
	}

	public void PickBox(int _) {
		if (_box != null) return;

		if (Physics.SphereCast(holdingPoint.position, pickupDistance, -holdingPoint.up, out RaycastHit info, 0, boxLayer)) {
				print("caught");
			if (info.collider.TryGetComponent(out Box box) && box.TryCapture(_animator.GetFloat(_pickupSpeedID))) {
				box.transform.position = holdingPoint.position;
				box.transform.parent = holdingPoint;

				_box = box;
			}
		}

		endAction?.Invoke();
	}

	public void DropBox(int _) {
		if (_box == null) return;

		_box.transform.parent = holdingPoint;
		_box.Throw(transform.forward * _animator.GetFloat(_dropForceID));

		_box = null;

		endAction?.Invoke();
	}

    IEnumerator MoveCoroutine(float speed, float duration) {
		float elapsedTime = 0f;
		float halfDuration = duration / 2f;

		startAction?.Invoke();
		_animator.SetTrigger(_startMovingID);

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
		_animator.SetBool(_spinningID, true);

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
		_animator.SetBool(_spinningID, false);
	}
}
