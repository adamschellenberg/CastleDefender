using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleController : MonoBehaviour
{

    [SerializeField] private float _maxHealth;

    private void Awake()
    {
        PlayerPrefsManager.SetCastleHealth(_maxHealth);
        PlayerPrefsManager.SetCurrentScore(0);
    }

    private void OnHit(float damage)
    {
        float currentHealth = PlayerPrefsManager.GetCastleHealth();
        currentHealth -= damage;

        PlayerPrefsManager.SetCastleHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Hit(float damage)
    {
        OnHit(damage);
    }

    private void Die()
    {
        SceneManager.LoadScene("EndMenu");
    }
}
