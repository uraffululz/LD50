using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour {

	[SerializeField] GameManager gameMan;

	Player_Flooring floorScript;

	Rigidbody rb;
 
	Vector3 currentMoveInput;
	[SerializeField] float moveSpeed;


    void Start() {
		floorScript = GetComponent<Player_Flooring>();
        rb = GetComponent<Rigidbody>();
    }

   

    void Update() {
		//if (currentMoveInput != Vector3.zero) {
		//	rb.AddForce(currentMoveInput * moveSpeed);
		//}

		if (GameManager.gameState == GameManager.GameState.Playing) {
			transform.position = transform.position + (currentMoveInput * moveSpeed * Time.deltaTime);
			transform.LookAt(transform.position + currentMoveInput, Vector3.up);
		}

	}


	private void OnTriggerEnter (Collider col) {
		if (col.CompareTag("FloorPickup") && !floorScript.carryingFloor) {
			floorScript.PickupFloorItem(col.gameObject);
		}
		else if (col.CompareTag("DeathPit")) {
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			gameMan.GameOver();
		}
	}


	//void OnMovement(InputAction.CallbackContext val) {
	//	//print(val.ReadValue<Vector3>());
	//	currentMoveInput = val.ReadValue<Vector3>();
	//}


	public void Move(InputAction.CallbackContext input) {//Vector3 input) {
		print(input.ReadValue<Vector2>());

		//if (input.performed) {
			currentMoveInput = new Vector3(input.ReadValue<Vector2>().x, 0, input.ReadValue<Vector2>().y);
			//rb.AddForce(currentMoveInput * moveSpeed);
			//transform.position = transform.position + (currentMoveInput * moveSpeed * Time.deltaTime);

		//}
	}


	
}
