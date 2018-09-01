using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalScript : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	Collider2D enemyCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
		
	}
}
