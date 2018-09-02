using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectilePrefab;

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
            FireProjectile();
            _fireRateTimer = 0;
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FireProjectile()
    {
        ProjectileController pooledProjectile = _projectilePool.FirstOrDefault(projectile => projectile.IsActive == false);

        if (pooledProjectile == null)
        {
            GameObject projectileGameObject = Instantiate(_projectilePrefab.gameObject, this.transform.position, this.transform.rotation, _projectileContainer.transform);

            pooledProjectile = projectileGameObject.GetComponent<ProjectileController>();
            _projectilePool.Add(pooledProjectile);
        }

        pooledProjectile.Activate(this.transform.position, this.transform.rotation);
    }
}
