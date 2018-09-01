using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnPositionX = -11;
    [SerializeField] private Vector2 _spawnPositionYRange = new Vector2(-4f, 4.5f);

    [SerializeField] private EnemyController _normalEnemy;
    [SerializeField] private float normalEnemySpawnRate;
    private float _normalEnemySpawnCooldown;

    [SerializeField] private EnemyController _fastEnemy;
    [SerializeField] private float fastEnemySpawnRate;
    private float _fastEnemySpawnCooldown;

    [SerializeField] private EnemyController _heavyEnemy;
    [SerializeField] private float heavyEnemySpawnRate;
    private float _heavyEnemySpawnCooldown;

    private List<EnemyController> _enemyPool = new List<EnemyController>();
    private GameObject _enemyContainer;

    private CastleController _castleController;

    private void Start()
    {
        _normalEnemySpawnCooldown = normalEnemySpawnRate;
        _fastEnemySpawnCooldown = fastEnemySpawnRate;
        _heavyEnemySpawnCooldown = heavyEnemySpawnRate;

        _castleController = GameObject.FindObjectOfType<CastleController>();

        _enemyContainer = new GameObject("EnemyContainer");
    }

    private void Update()
    {
        if (_normalEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_normalEnemy, EnemyType.Normal);
            _normalEnemySpawnCooldown = normalEnemySpawnRate;
        }

        if (_fastEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_fastEnemy, EnemyType.Fast);
            _fastEnemySpawnCooldown = fastEnemySpawnRate;
        }

        if (_heavyEnemySpawnCooldown <= 0)
        {
            SpawnEnemy(_heavyEnemy, EnemyType.Heavy);
            _heavyEnemySpawnCooldown = heavyEnemySpawnRate;
        }

        _normalEnemySpawnCooldown -= Time.deltaTime;
        _fastEnemySpawnCooldown -= Time.deltaTime;
        _heavyEnemySpawnCooldown -= Time.deltaTime;
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
}
