using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnPositionX = -13;
    [SerializeField] private Vector2 _spawnPositionYRange = new Vector2(-4f, 4.5f);

    [SerializeField] private EnemyController _normalEnemy;
    [SerializeField] private float normalEnemySpawnRateDefault;
	[SerializeField] public float currentNormalEnemySpawnRate;
    private float _normalEnemySpawnCooldown;

    [SerializeField] private EnemyController _fastEnemy;
    [SerializeField] private float fastEnemySpawnRateDefault;
	[SerializeField] public float currentFastEnemySpawnRate;
    private float _fastEnemySpawnCooldown;

    [SerializeField] private EnemyController _heavyEnemy;
    [SerializeField] private float heavyEnemySpawnRateDefault;
	[SerializeField] public float currentHeavyEnemySpawnRate;
    private float _heavyEnemySpawnCooldown;

	[SerializeField] private float normalEnemySpawnRateModifier;
	[SerializeField] private float fastEnemySpawnRateModifier;
	[SerializeField] private float heavyEnemySpawnRateModifier;

	[SerializeField] private float normalEnemyRespawnCap;
	[SerializeField] private float fastEnemyRespawnCap;
	[SerializeField] private float heavyEnemyRespawnCap;

    private List<EnemyController> _enemyPool = new List<EnemyController>();
    private GameObject _enemyContainer;

    private CastleController _castleController;

    private void Start()
    {
		currentNormalEnemySpawnRate = normalEnemySpawnRateDefault;
		currentFastEnemySpawnRate = fastEnemySpawnRateDefault;
		currentHeavyEnemySpawnRate = heavyEnemySpawnRateDefault;

        _normalEnemySpawnCooldown = currentNormalEnemySpawnRate;
        _fastEnemySpawnCooldown = currentFastEnemySpawnRate;
        _heavyEnemySpawnCooldown = currentHeavyEnemySpawnRate;

        _castleController = GameObject.FindObjectOfType<CastleController>();

        _enemyContainer = new GameObject("EnemyContainer");
    }

    private void Update()
    {
        if (_normalEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_normalEnemy, EnemyType.Normal);
            _normalEnemySpawnCooldown = currentNormalEnemySpawnRate;
        }

        if (_fastEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_fastEnemy, EnemyType.Fast);
            _fastEnemySpawnCooldown = currentFastEnemySpawnRate;
        }

        if (_heavyEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_heavyEnemy, EnemyType.Heavy);
            _heavyEnemySpawnCooldown = currentHeavyEnemySpawnRate;
        }

        _normalEnemySpawnCooldown -= Time.deltaTime;
        _fastEnemySpawnCooldown -= Time.deltaTime;
        _heavyEnemySpawnCooldown -= Time.deltaTime;

		IncreaseDifficulty ();

    }

    private void SpawnEnemy(EnemyController enemyPrefab, EnemyType enemyType)
    {
        EnemyController pooledEnemyController = _enemyPool.FirstOrDefault(enemy => enemy.IsActive == false && enemy.EnemyType == enemyType);

        Vector2 position = new Vector2(_spawnPositionX, Random.Range(_spawnPositionYRange.x, _spawnPositionYRange.y));

        if (pooledEnemyController == null)
        {
            GameObject enemyGameObject = Instantiate(enemyPrefab.gameObject, position, Quaternion.identity, _enemyContainer.transform);

            pooledEnemyController = enemyGameObject.GetComponent<EnemyController>();
            pooledEnemyController.Initialize(_castleController);

            _enemyPool.Add(pooledEnemyController);
        }

        pooledEnemyController.Activate(position);
    }

	private void IncreaseDifficulty() {

		float currentScore = (float) PlayerPrefsManager.GetCurrentScore ();

		if (currentNormalEnemySpawnRate >= normalEnemyRespawnCap) {
			currentNormalEnemySpawnRate = normalEnemySpawnRateDefault - (currentScore / normalEnemySpawnRateModifier);
		}

		if (currentFastEnemySpawnRate >= fastEnemyRespawnCap) {
			currentFastEnemySpawnRate = fastEnemySpawnRateDefault - (currentScore / fastEnemySpawnRateModifier);
		}

		if (currentHeavyEnemySpawnRate >= heavyEnemyRespawnCap) {
			currentHeavyEnemySpawnRate = heavyEnemySpawnRateDefault - (currentScore / heavyEnemySpawnRateModifier);
		}

	}
}
