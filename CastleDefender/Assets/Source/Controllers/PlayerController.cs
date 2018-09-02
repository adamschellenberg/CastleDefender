using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private float _heatWaveSpawnDistance = 5;
    [SerializeField] private float _heatWaveProjectileCount = 15;

    private List<ProjectileController> _projectilePool = new List<ProjectileController>();

    private GameObject _projectileContainer;

    private float _fireRateTimer = 0;

    private void Awake()
    {
        _projectileContainer = new GameObject("ProjectileContainer");
    }

    private void Update()
    {
        _fireRateTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) == true && _fireRateTimer >= PlayerPrefsManager.GetFireRate())
        {
            FireProjectile(this.transform.position, this.transform.rotation);
            _fireRateTimer = 0;
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void HeatWave()
    {
        for (int i = 0; i < _heatWaveProjectileCount; i++)
        {
            FireProjectile(new Vector3(this.transform.position.x, this.transform.position.x + (i * _heatWaveSpawnDistance), 0), Quaternion.identity);
        }

        for (int i = 0; i < _heatWaveProjectileCount; i++)
        {
            FireProjectile(new Vector3(this.transform.position.x, this.transform.position.x - (i * _heatWaveSpawnDistance), 0), Quaternion.identity);
        }
    }

    private void FireProjectile(Vector3 position, Quaternion rotation)
    {
        ProjectileController pooledProjectile = _projectilePool.FirstOrDefault(projectile => projectile.IsActive == false);

        if (pooledProjectile == null)
        {
            GameObject projectileGameObject = Instantiate(_projectilePrefab.gameObject, position, rotation, _projectileContainer.transform);

            pooledProjectile = projectileGameObject.GetComponent<ProjectileController>();
            _projectilePool.Add(pooledProjectile);
        }

        pooledProjectile.Activate(this.transform.position, this.transform.rotation);
    }
}
