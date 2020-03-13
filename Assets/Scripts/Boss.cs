using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

	public int life;
	public Transform origemBala;
	SpriteRenderer sprite;
	public List<GameObject> Jogadores;
	public float velocidade;
	Rigidbody2D rg2d;
	int jogador;
	public GameObject Tiro;

	public List<GameObject> MiniBoss;

	public GameObject popup;
	// Use this for initialization
	void Start () {
		for(int i=0; i<MiniBoss.Count; i++){
			MiniBoss [i].SetActive (false);
		}


		sprite = GetComponent<SpriteRenderer> ();
		rg2d = GetComponent<Rigidbody2D> ();

		Invoke ("Atack1", 1f);
		Invoke ("Busca", 2f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rg2d.velocity = new Vector2 (velocidade, rg2d.velocity.y);

		if(Jogadores[jogador].transform.position.x > transform.position.x && sprite.flipX
			|| Jogadores[jogador].transform.position.x < transform.position.x && !sprite.flipX){
			sprite.flipX = !sprite.flipX;
			velocidade *= -1;

			if (velocidade > 0) {
				origemBala.position = new Vector3 (this.transform.position.x + 0.48f, origemBala.position.y, origemBala.position.z);
			} else {
				origemBala.position = new Vector3 (this.transform.position.x - 0.48f, origemBala.position.y, origemBala.position.z);

			}

		}

		if(life<6){
			for(int i=0; i<MiniBoss.Count; i++){
				MiniBoss [i].SetActive (true);
			}
				
		}

	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			Jogador abj = other.gameObject.GetComponent<Jogador> ();
			abj.DamagePlayer ();
		}


	}
	void Atack1(){
		
		GameObject cloneBala = Instantiate (Tiro, origemBala.position, origemBala.rotation);
		float aux = Random.Range (1, 3);

		if (sprite.flipX) {
			cloneBala.transform.eulerAngles = new Vector3 (0,0,180);
		}

		Invoke ("Atack1", aux);
	}

	void Busca(){
		jogador = Random.Range (0, Jogadores.Count);
		Invoke ("Busca", 1f);
	}

	IEnumerator Damage(){
		
		for (int i = 0; i < 3; i ++) {
			sprite.color = Color.red;
			yield return new WaitForSeconds (0.3f);
			sprite.color = Color.white;
			yield return new WaitForSeconds (0.3f);
		}

	}

	public void DamageBoss(){

		if(life>0){
			life--;

			StartCoroutine (Damage());

			if(life < 2){
				popup.SetActive (true);
				SceneManager.LoadScene ("Final");
				Destroy (gameObject);
			}
		}
	}
}
