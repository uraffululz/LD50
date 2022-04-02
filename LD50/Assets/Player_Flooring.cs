using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Flooring : MonoBehaviour {

	[SerializeField] GroundMaster groundScript;
 
	[SerializeField] float floorRayDist;
	[SerializeField] float floorVertOffset;

	public bool carryingFloor {get; private set;} = false;

	GameObject floorToReplace = null;


    void Start() {
        
    }

   

    void Update() {
        DetectFloorsToReplace();
    }


	private void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position + (Vector3.up * floorVertOffset), transform.forward * floorRayDist);
	}


	GameObject DetectFloorsToReplace() {
		floorToReplace = null;

		Ray forwardRay = new Ray(transform.position + (Vector3.up * floorVertOffset), transform.forward * floorRayDist);


		RaycastHit forwardHit;
		RaycastHit leftHit;
		RaycastHit rightHit;

		if (Physics.Raycast(forwardRay, out forwardHit, floorRayDist, 1 << 6)) {
			GameObject floorChild = forwardHit.collider.transform.GetChild(0).gameObject;

			if (floorChild != null && !floorChild.activeInHierarchy) {
				floorToReplace = floorChild;
				print("I found a spot to replace the floor");
			}
		}



		if (floorToReplace != null) {
			return floorToReplace;
		}
		else {
			return null;
		}
	}


	public void PickupFloorItem (GameObject floor) {
		Destroy(floor);
		carryingFloor = true;
		groundScript.activeFloorPickups--;

		///Switch animation to "Walk_FloorCarry"
	}


	public void PlaceFloor() {
		if (carryingFloor && floorToReplace != null) {
			groundScript.ReplaceFloor(floorToReplace);
			carryingFloor = false;
		}
		else {
			print("I can't place a floor there");
		}
	}


}
