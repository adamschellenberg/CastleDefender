using UnityEngine;

public enum EnemyType 
{
    Fast,
    Normal,
    Heavy,
}

public class EnemyController : MonoBehaviour
{
    public bool IsActive { get; set; }
    public EnemyType EnemyType { get { return _enemyType; } }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackCooldownDefault;
    [SerializeField] private float _attackPower;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _pointsWorth = 1;
    [SerializeField] private EnemyType _enemyType;

    private bool _isAtCastle;
    private float _attackCooldown;
    private int _currentHealth;

    private CastleController _castleController;

    public void Initialize(CastleController castleController)
    {
        _castleController = castleController;
    }

    public void Activate(Vector3 startPosition)
    {
        this.gameObject.name = $"{_enemyType.ToString()} - Active";
        this.transform.position = startPosition;

        _isAtCastle = false;
        _attackCooldown = 0f;
        _currentHealth = _maxHealth;

        IsActive = true;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.name = $"{_enemyType.ToString()} - Inactive";

        IsActive = false;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsActive == false)
        {
            return;
        }

        if (_isAtCastle == false)
        {
            Move();
        }

        if (_isAtCastle == true)
        {
            Attack();
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Castle")
        {
            _isAtCastle = true;
        }

        if (col.gameObject.tag == "Projectile")
        {
            OnHit();
        }
    }

    private void Move()
    {
        transform.Translate(new Vector2(_moveSpeed * Time.deltaTime, 0));
    }

    private void Attack()
    {
        if (_attackCooldown <= 0f)
        {
            _castleController.Hit(_attackPower);
            _attackCooldown = _attackCooldownDefault;
        }
        else
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void OnHit()
    {
        _currentHealth -= 1;
    }

    private void Die()
    {
        int currentScore = PlayerPrefsManager.GetCurrentScore();
        currentScore += _pointsWorth;

        PlayerPrefsManager.SetCurrentScore(currentScore);

        Deactivate();
    }
}
