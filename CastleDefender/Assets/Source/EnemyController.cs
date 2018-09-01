using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] private float moveSpeed;
	[SerializeField] private Collider2D enemyCollider;

	[SerializeField] private float attackCooldownDefault;
	[SerializeField] private float attackPower;
	[SerializeField] private int health;
	[SerializeField] private int pointsWorth = 1;


	private bool isAtCastle;
	private float attackCooldown;

	private CastleController castleController;


	// Use this for initialization
	private void Start () {

		isAtCastle = false;
		attackCooldown = 0f;

		castleController = GameObject.FindObjectOfType<CastleController> ();
		
	}
	
	// Update is called once per frame
	private void Update () {

		if (isAtCastle == false) {
			Move ();
		}

		if (isAtCastle == true) {
			Attack ();
		}

		if (health <= 0) {
			Die ();
		}
		
	}

	private void OnTriggerEnter2D (Collider2D col) {
		// stop moving when at castle
		if (col.gameObject.tag == "Player") {
			isAtCastle = true;
		}

		if(col.gameObject.tag == "Projectile"){
			OnHit ();
		}

	}
		

	private void Move() {
		transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
	}


	private void Attack() {
		if (attackCooldown <= 0f) {
			// attack
			castleController.Hit(attackPower);
			// reset cooldown timer
			attackCooldown = attackCooldownDefault;
		} else {
			// decrease cooldown timer
			attackCooldown -= Time.deltaTime;
		}
	}


	private void OnHit(){
		// decrease heath
		health -= 1;
	}


	private void Die() {

		int currentScore = PlayerPrefsManager.GetCurrentScore ();
		currentScore += pointsWorth;
		PlayerPrefsManager.SetCurrentScore (currentScore);

		Destroy (this.gameObject);
	}
		
}
