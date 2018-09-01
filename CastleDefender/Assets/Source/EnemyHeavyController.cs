using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyController : MonoBehaviour {

	[SerializeField] private float moveSpeed;
	[SerializeField] private Collider2D enemyCollider;

	[SerializeField] private float attackCooldownDefault;
	[SerializeField] private int attackPower;
	[SerializeField] private int health;

	private bool isAtCastle;
	private float attackCooldown;


	// Use this for initialization
	void Start () {

		isAtCastle = false;
		attackCooldown = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {

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

	void OnTriggerEnter2D (Collider2D col) {
		// stop moving when at castle
		if (col.gameObject.tag == "Player") {
			isAtCastle = true;
		}

		if(col.gameObject.tag == "Projectile"){
			OnHit ();
		}

	}
		

	void Move() {
		transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
	}


	void Attack() {
		if (attackCooldown <= 0f) {
			// attack
			Debug.Log("Enemy-Normal hit Castle");
			// reset cooldown timer
			attackCooldown = attackCooldownDefault;
		} else {
			// decrease cooldown timer
			attackCooldown -= Time.deltaTime;
		}
	}


	void OnHit(){
		// decrease heath
		health -= 1;
	}


	void Die() {
		Destroy (this.gameObject);
	}
		
}
