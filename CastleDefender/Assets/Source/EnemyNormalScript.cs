using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalScript : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	Collider2D enemyCollider;

	private bool isAtCastle;

	// Use this for initialization
	void Start () {

		isAtCastle = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isAtCastle == false) {
			Move ();
		}
		
	}

	void OnTriggerEnter2D (Collider2D col) {
	
		// stop moving when at castle
		isAtCastle = true;

	}
		

	void Move() {
		transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
	}
}
