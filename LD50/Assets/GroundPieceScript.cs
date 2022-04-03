using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPieceScript : MonoBehaviour {

	[SerializeField] GameObject ground;
	Vector3 groundPos;
 
	bool groundFalling = false;
	[SerializeField] float groundFallSpeed;


    void Start() {
        ground = transform.GetChild(0).gameObject;
		groundPos = ground.transform.position;
    }

   

    void Update() {
		//if (groundFalling) {
		//	//Vector3 chosenPos = ground.transform.position;
		//	//ground.transform.position -= new Vector3(chosenPos.x, chosenPos.y + (groundFallSpeed * Time.deltaTime), chosenPos.z);
		//	//ground.transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3.up * -.5f), groundFallSpeed * Time.deltaTime);

		//	//if (ground.transform.position.y <= -10) {
		//	//	EndFall();
		//	//}
		//}
	}


	public void SetToFall() {
		groundFalling = true;

		//ground.GetComponent<Collider>().isTrigger = true;
	}


	public void EndFall() {
		ground.SetActive(false);
		groundFalling = false;
	}


	public void ResetGround () {
		ground.SetActive(true);
		ground.transform.position = groundPos;
		ground.GetComponent<Collider>().isTrigger = false;

		groundFalling = false;
	}


	}
