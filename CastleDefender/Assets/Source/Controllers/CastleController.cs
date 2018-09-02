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

        SceneManager.LoadScene("EndMenu");
    }
}
