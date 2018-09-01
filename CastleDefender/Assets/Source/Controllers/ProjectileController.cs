using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public bool IsActive { get; set; }

    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _timeout = 5;

    private float _timeoutTimer = 0;

    public void Activate(Vector3 startPosition, Quaternion startRotation)
    {
        this.gameObject.name = $"Projectile - Active";

        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
        _timeoutTimer = 0;

        IsActive = true;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.name = $"Projectile - Inactive";

        IsActive = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(IsActive == false)
        {
            return;
        }

        this.transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed);

        _timeoutTimer += Time.deltaTime;

        if(_timeoutTimer >= _timeout)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Deactivate();
        }
    }
}
