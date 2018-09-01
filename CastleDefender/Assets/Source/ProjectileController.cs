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

        //this.transform.position = Vector2.MoveTowards(this.transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

        //if(Vector2.Distance(this.transform.position, _targetPosition) <= 0.1f)
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
