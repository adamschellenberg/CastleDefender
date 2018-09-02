using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private float _heatWaveSpawnDistance = 5;
    [SerializeField] private float _heatWaveProjectileCount = 15;
    [SerializeField] private float _heatWaveProjectileOffset = 0.2f;

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

        if (Input.GetMouseButton(0) == true && _fireRateTimer >= PlayerPrefsManager.GetFireRate())
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
        var rotation = Quaternion.Euler(new Vector3(0, 0, 180));;

        for (float i = 0; i < _heatWaveProjectileCount; i++)
        {
            FireProjectile(new Vector3(this.transform.position.x, this.transform.position.y + (i * _heatWaveSpawnDistance * _heatWaveProjectileOffset), 0), rotation);
        }

        for (float i = 0; i < _heatWaveProjectileCount; i++)
        {
            FireProjectile(new Vector3(this.transform.position.x, this.transform.position.y - (i * _heatWaveSpawnDistance * _heatWaveProjectileOffset), 0), rotation);
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

        pooledProjectile.Activate(position, rotation);
    }
}
