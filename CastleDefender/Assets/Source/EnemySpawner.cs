using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private GameObject normalEnemy;
	[SerializeField] private float normalEnemySpawnRate;
	private float normalEnemySpawnCooldown;

	[SerializeField] private GameObject fastEnemy;
	[SerializeField] private float fastEnemySpawnRate;
	private float fastEnemySpawnCooldown;

	[SerializeField] private GameObject heavyEnemy;
	[SerializeField] private float heavyEnemySpawnRate;
	private float heavyEnemySpawnCooldown;

	// Use this for initialization
	void Start () {

		normalEnemySpawnCooldown = normalEnemySpawnRate;
		fastEnemySpawnCooldown = fastEnemySpawnRate;
		heavyEnemySpawnCooldown = heavyEnemySpawnRate;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (normalEnemySpawnCooldown <= 0) {
			SpawnNormalEnemy();
		} else {
			normalEnemySpawnCooldown -= Time.deltaTime;
		}

		if (fastEnemySpawnCooldown <= 0) {
			SpawnFastEnemy();
		} else {
			fastEnemySpawnCooldown -= Time.deltaTime;
		}

		if (heavyEnemySpawnCooldown <= 0) {
			SpawnHeavyEnemy();
		} else {
			heavyEnemySpawnCooldown -= Time.deltaTime;
		}
		
	}

	private void SpawnNormalEnemy() {
		Vector2 position = new Vector2(-11, Random.Range(-4f, 4.5f));
		Instantiate(normalEnemy, position, Quaternion.identity);
		normalEnemySpawnCooldown = normalEnemySpawnRate;
	}

	private void SpawnFastEnemy() {
		Vector2 position = new Vector2(-11, Random.Range(-4f, 4.5f));
		Instantiate(fastEnemy, position, Quaternion.identity);
		fastEnemySpawnCooldown = fastEnemySpawnRate;
	}

	private void SpawnHeavyEnemy() {
		Vector2 position = new Vector2(-11, Random.Range(-4f, 4.5f));
		Instantiate(heavyEnemy, position, Quaternion.identity);
		heavyEnemySpawnCooldown = heavyEnemySpawnRate;
	}
}
