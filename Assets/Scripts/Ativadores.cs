using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativadores : MonoBehaviour {

	public Collider2D colisor;
	public SpriteRenderer sprite;

	public bool ativado;

	void Update () {
		if (ativado) {
			colisor.enabled = true;
			sprite.color = Color.white;
		} else {
			colisor.enabled = false;
			sprite.color = Color.gray;
		}
	}
}
