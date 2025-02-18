using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Movement : MonoBehaviour {
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	private float playerSpeed = 2.0f;
	private float jumpHeight = 1.0f;
	private float gravityValue = -9.81f;
	private Animator animator;

	private void Start () {
		controller = gameObject.GetComponent<CharacterController>();
		if (controller == null) {
			controller = gameObject.AddComponent<CharacterController>();
		}
		animator = gameObject.GetComponent<Animator>();
		Debug.Log("Controller initialized to:");
		Debug.Log(controller);
		Debug.Log("Animator initialized to:");
		Debug.Log(animator);
	}

	void Update () {
		if (controller == null) {
			Debug.Log("CharacterController not set... why?!??!?");
			return;
		}
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0) {
			playerVelocity.y = 0f;
		}

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		controller.Move(move * Time.deltaTime * playerSpeed);
		gameObject.transform.LookAt(move * Time.deltaTime * playerSpeed + gameObject.transform.position);

		if (move != Vector3.zero) {
			gameObject.transform.forward = move;
		}

		if (Input.GetAxis("Vertical") > 0.0f) {
			animator.SetFloat("Blend", 0.5f);
			Debug.Log(animator.GetFloat("Blend"));
		} else if(Input.GetAxis("Vertical") < 0.0f) {
			animator.SetFloat("Blend", 1.0f);
			Debug.Log(animator.GetFloat("Blend"));
		} else {
			animator.SetFloat("Blend", 0.0f);
		}


		// Makes the player jump
		if (Input.GetButtonDown("Jump") && groundedPlayer) {
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
}