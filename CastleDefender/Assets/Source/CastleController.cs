using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleController : MonoBehaviour {

	[SerializeField] private float _maxHealth;

	private void Awake() {

		PlayerPrefsManager.SetCastleHealth (1);
		PlayerPrefsManager.SetCurrentScore (0);

	}

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

	private void OnHit(float damage) {
		float currentHealth = PlayerPrefsManager.GetCastleHealth () * 100;
		currentHealth -= damage;
		PlayerPrefsManager.SetCastleHealth (currentHealth / 100);
		Debug.Log (PlayerPrefsManager.GetCastleHealth().ToString());

		if (currentHealth <= 0) {
		
			Die ();

		}
	}

	public void Hit(float damage) {
		OnHit (damage);
	}

	private void Die() {
	
		SceneManager.LoadScene ("EndMenu");
	
	}
}
