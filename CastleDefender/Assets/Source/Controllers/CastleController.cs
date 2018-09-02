using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleController : MonoBehaviour
{
    public void Hit(float damage)
    {
        OnHit(damage);
    }

    private void OnHit(float damage)
    {
        PlayerPrefsManager.DecreaseCurrentHealth(damage);

        if (PlayerPrefsManager.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
		//print currentspawnrates

		EnemySpawner currentRespawnRates;
		currentRespawnRates = GameObject.FindObjectOfType<EnemySpawner>();

		float normalEnemyRespawnRate = currentRespawnRates.currentNormalEnemySpawnRate;
		float fastEnemyRespawnRate = currentRespawnRates.currentFastEnemySpawnRate;
		float heavyEnemyRespawnRate = currentRespawnRates.currentHeavyEnemySpawnRate;

		Debug.Log ("Normal Enemy Respawn Rate: " + normalEnemyRespawnRate.ToString());
		Debug.Log ("Fast Enemy Respawn Rate: " + fastEnemyRespawnRate.ToString());
		Debug.Log ("Heavy Enemy Respawn Rate: " + heavyEnemyRespawnRate.ToString());

        SceneManager.LoadScene("EndMenu");
    }
}
