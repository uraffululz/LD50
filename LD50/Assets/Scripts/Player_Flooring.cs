using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Flooring : MonoBehaviour {

	[SerializeField] Animator anim;

	[SerializeField] GroundMaster groundScript;
 
	[SerializeField] float floorRayDist;
	[SerializeField] float floorVertOffset;

	public bool carryingFloor {get; private set;} = false;
	[SerializeField] GameObject carryFloorPiece;

	GameObject floorToReplace = null;
	public int totalFloorsPlaced = 0;
	


    void Start() {
        
    }

   

    void Update() {
        DetectFloorsToReplace();
    }


	private void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position + (Vector3.up * floorVertOffset), transform.forward * floorRayDist);
		Gizmos.DrawRay(transform.position + (Vector3.up * floorVertOffset), (transform.forward * floorRayDist) + (transform.right * -floorRayDist / 4));
		Gizmos.DrawRay(transform.position + (Vector3.up * floorVertOffset), (transform.forward * floorRayDist) + (transform.right * floorRayDist / 4));

	}


	GameObject DetectFloorsToReplace() {
		floorToReplace = null;

		Ray forwardRay = new Ray(transform.position + (Vector3.up * floorVertOffset), transform.forward * floorRayDist);
		Ray leftRay = new Ray(transform.position + (Vector3.up * floorVertOffset), (transform.forward * floorRayDist) + (transform.right * -floorRayDist / 4));
		Ray rightRay = new Ray(transform.position + (Vector3.up * floorVertOffset), (transform.forward * floorRayDist) + (transform.right * floorRayDist / 4));

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
		else if(Physics.Raycast(leftRay, out leftHit, floorRayDist, 1 << 6)) {
			GameObject floorChild = leftHit.collider.transform.GetChild(0).gameObject;

			if (floorChild != null && !floorChild.activeInHierarchy) {
				floorToReplace = floorChild;
				print("I found a spot to replace the floor");
			}
		}
		else if (Physics.Raycast(rightRay, out rightHit, floorRayDist, 1 << 6)) {
			GameObject floorChild = rightHit.collider.transform.GetChild(0).gameObject;

			if (floorChild != null && !floorChild.activeInHierarchy) {
				floorToReplace = floorChild;
				print("I found a spot to replace the floor");
			}
		}



		if (floorToReplace != null && carryingFloor) {
			///Highlight the Floor Spawn that the player is "touching"
			///Leave all other Floor Spawns transparent (red?)
			groundScript.HighlightFloorSpawns(floorToReplace);
			
			return floorToReplace;
		}
		else {
			groundScript.HighlightFloorSpawns(null);

			return null;
		}
	}


	public void PickupFloorItem (GameObject floor) {
		Destroy(floor);
		carryingFloor = true;
		groundScript.activeFloorPickups--;

		///Switch animation to "Walk_FloorCarry"
		anim.SetLayerWeight(1, 1f);
		carryFloorPiece.SetActive(true);
	}


	public void PlaceFloor() {
		if (carryingFloor && DetectFloorsToReplace() != null) {
			groundScript.ReplaceFloor(floorToReplace);
			carryingFloor = false;
			totalFloorsPlaced++;

			anim.SetLayerWeight(1, 0f);
			carryFloorPiece.SetActive(false);
		}
		else {
			print("I can't place a floor there");
		}
	}


}
