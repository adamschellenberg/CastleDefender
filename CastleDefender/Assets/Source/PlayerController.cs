using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ProjectileController _projectilePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            FireProjectile();
        }

        var pos = Camera.main.WorldToScreenPoint(this.transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FireProjectile()
    {
        Instantiate(_projectilePrefab.gameObject, this.transform.position, this.transform.rotation);
    }
}
