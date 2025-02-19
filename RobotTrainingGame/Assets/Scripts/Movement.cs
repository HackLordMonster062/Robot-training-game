using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {
    public float speed;
    public float steeringSpeed;

	CharacterController controller;
    Animator anim;
	
    void Start() {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        float forward = Input.GetAxis("Vertical") * speed * 10 * Time.deltaTime;
        float steer = Input.GetAxis("Horizontal") * steeringSpeed * 10 * Time.deltaTime;

        controller.Move(transform.forward * forward);
        transform.Rotate(Vector3.up * steer);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            anim.SetTrigger("StartMoving");
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
			anim.SetTrigger("StopMoving");

        anim.SetBool("Spinning", Input.GetAxisRaw("Horizontal") != 0);
	}
}
