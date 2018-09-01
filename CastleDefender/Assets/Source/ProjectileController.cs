using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _damage = 1;

    private void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
