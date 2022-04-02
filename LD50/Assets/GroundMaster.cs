using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaster : MonoBehaviour {
 
	[SerializeField] GameObject[] floorSpawns;
	[SerializeField] List<GameObject> groundPiecesActive;
	[SerializeField] GameObject chosenGround;

	[SerializeField] float groundFallDelay;
	[SerializeField] float groundFallCurrent;

	[SerializeField] GameObject floorPrefab;
	[SerializeField] Material floorMat;
	[SerializeField] Transform floorPickupParent;
	public int activeFloorPickups;
	[SerializeField] float floorSpawnTimer;
	[SerializeField] float floorSpawnCurrent;


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

		yield return new WaitForSeconds(1.5f);

		chosenGround.SetActive(false);
		groundPiecesActive.Remove(chosenGround);
		chosenGround = null;
	}


	public void ReplaceFloor(GameObject floorObject) {
		//GameObject replacedFloor = floorObject.transform.GetChild(0).gameObject;
		floorObject.GetComponent<MeshRenderer>().material = floorMat;
		floorObject.SetActive(true);
		groundPiecesActive.Add(floorObject);
	}

	void SpawnFloorResource() {
		Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 20, Random.Range(-6, 10));

		GameObject floorPickup = Instantiate(floorPrefab, spawnPos, Quaternion.identity, floorPickupParent);
		floorPickup.GetComponent<FloorPickupScript>().groundScript = this;
		activeFloorPickups++;
	}

}
