using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaster : MonoBehaviour {
 
	public GameObject[] floorSpawns;
	[SerializeField] List<GameObject> groundPiecesActive;
	[SerializeField] GameObject chosenGround;

	[SerializeField] float groundFallDelay;
	[SerializeField] float groundFallCurrent;
	[SerializeField] float groundDropSpeed;

	[SerializeField] GameObject floorPrefab;
	[SerializeField] Material floorMat;
	[SerializeField] Transform floorPickupParent;
	public int activeFloorPickups;
	[SerializeField] float floorSpawnTimer;
	[SerializeField] float floorSpawnCurrent;

	[SerializeField] Material activeMat;
	[SerializeField] Material inactiveMat;


	void Start() {
        groundFallCurrent = groundFallDelay;
		floorSpawnCurrent = floorSpawnTimer;
    }

   

    void Update() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			if (groundFallCurrent > 0) {
				groundFallCurrent -= Time.deltaTime;
			}
			else {
				if (chosenGround == null && groundPiecesActive.Count > 0) {
					ChooseGround();
					StartCoroutine(GroundFalls());

					groundFallCurrent = groundFallDelay;
				}
			}

			if (floorSpawnCurrent > 0) {
				floorSpawnCurrent -= Time.deltaTime;
			}
			else {
				if (activeFloorPickups < 4) {
					SpawnFloorResource();
					floorSpawnCurrent = floorSpawnTimer;
				}
				else {
					floorSpawnCurrent = floorSpawnTimer;
				}
			}
		}
    }


	void ChooseGround() {
		chosenGround = groundPiecesActive[Random.Range(0, groundPiecesActive.Count)];
	}


	IEnumerator GroundFalls() {
		Color fadedGroundColor = new Color(1, 0, 0, .5f);
		chosenGround.GetComponent<MeshRenderer>().material.color = fadedGroundColor;

		chosenGround.transform.parent.GetComponent<GroundPieceScript>().SetToFall();

		yield return new WaitForSeconds(1.2f);

		chosenGround.transform.parent.GetComponent<GroundPieceScript>().EndFall();
		groundPiecesActive.Remove(chosenGround);
		chosenGround = null;
	}


	public void HighlightFloorSpawns(GameObject floor) {
		foreach (GameObject floorSpawn in floorSpawns) {
			MeshRenderer renderer = floorSpawn.GetComponent<MeshRenderer>();

			if (floorSpawn.transform.GetChild(0).gameObject.activeInHierarchy) {
				renderer.enabled = false;
			}
			else {
				renderer.enabled = true;
				renderer.material = inactiveMat;
			}
		}

		if (floor != null) {
			floor.transform.parent.GetComponent<MeshRenderer>().material = activeMat;
		}
	}


	public void ReplaceFloor(GameObject floorObject) {
		//GameObject replacedFloor = floorObject.transform.GetChild(0).gameObject;
		floorObject.GetComponent<MeshRenderer>().material = floorMat;
		floorObject.transform.parent.GetComponent<GroundPieceScript>().ResetGround();
		groundPiecesActive.Add(floorObject);
	}

	void SpawnFloorResource() {
		Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 17, Random.Range(-6, 10));

		GameObject floorPickup = Instantiate(floorPrefab, spawnPos, Quaternion.identity, floorPickupParent);
		floorPickup.GetComponent<FloorPickupScript>().groundScript = this;

		activeFloorPickups++;
	}

}
