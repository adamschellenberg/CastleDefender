using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalController : MonoBehaviour {

	[SerializeField] private float moveSpeed;
	[SerializeField] private Collider2D enemyCollider;

	[SerializeField] private float attackCooldown;
	[SerializeField] private int attackPower;
	[SerializeField] private int health;


	private bool isAtCastle;


	// Use this for initialization
	void Start () {

		isAtCastle = false;
		attackPower = 1;
		attackCooldown = 0f;
		health = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isAtCastle == false) {
			Move ();
		}

		if (isAtCastle == true) {
			Attack ();
		}

		if (Input.GetKeyDown("space")) {
			OnHit ();
		}
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		// stop moving when at castle
		isAtCastle = true;
	}
		

	void Move() {
		transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
	}

	void Attack() {
		if (attackCooldown <= 0f) {
			// attack
			Debug.Log("Enemy-Normal hit Castle");
			// reset cooldown timer
			attackCooldown = 2f;
		} else {
			// decrease cooldown timer
			attackCooldown -= Time.deltaTime;
		}
	}

	void OnHit(){

		// decrease heath
		Debug.Log ("I'm hit!");

	}
}
