using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPickupScript : MonoBehaviour {
 
	public GroundMaster groundScript;


    void Start() {
        
    }

   

    void Update() {
        
    }


	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("DeathPit")) {
			groundScript.activeFloorPickups--;
			Destroy(gameObject);
			///gameObject.SetActive(false);
		}
	}


}
