using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour {

	public float Velocidade;      //Velocidade do Jogador
	private float move;		 //Controlador de velocidade do Jogador
	public float ForcaPulo;  //Força do Pulo
	public int QuantidadeBala; //Quantidade de tiros
	private bool noChao;   //Verifica se esta no chão
	public bool pulando = false;    //Verifica se esta pulando
	public bool isAlive = true;      //Verifica se esta vivo 
	public int life;               //Vidas do jogador
	public bool invunerabilidade = false; //invunerabilidade do jogador
	AudioSource somPulo; //Som de pulo
	AudioSource somTiro;
	public float proximoTiro;   //Quantidade de tiros
	public float tempoTiro;   //Intervalo de tempo entre tiros
	public GameObject Bala; //Bala
	public Transform origemBala; //Lugar onde a bala é criada
	public Transform checkChao; //Local de verificação se o jogador toca no chão
	SpriteRenderer sprite;		  //Controladora do SpriteRenderer do jogador
	Animator anim;				  //Controladora de animção do jogador
	Rigidbody2D rb2d;			  //Controladora do Rigidbody(fisica) do jogador

	public int NJogador;

	string horizontal;
	string vertical;
	string atack;

	public GameObject popup;

	void Awake(){
		if (NJogador == 1) {
			horizontal = "Horizontal";
			vertical = "Jump";
			atack = "Fire";

		} else {
			horizontal = "Horizontal2";
			vertical = "Jump2";
			atack = "Fire2";
		}
			
		pulando = false;
		//Pega componentes
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		somPulo = GetComponent<AudioSource> ();
		somTiro = GameObject.Find ("Main Camera").GetComponent<AudioSource> ();
	}

	void FixedUpdate(){
		if (isAlive) {
			//Pega a direção da movimentação
			move = Input.GetAxis (horizontal);
			//Adiciona velocidade
			rb2d.velocity = new Vector2 (Velocidade * move, rb2d.velocity.y);
			//Vira jogador
			if ((move > 0f && sprite.flipX) || (move < 0f && !sprite.flipX)) {
				Flip ();
			}
			//Muda animaçoes
			anim.SetBool ("Pulo", rb2d.velocity.y != 0f);
			anim.SetFloat ("Velocidade", Mathf.Abs (move));

			//Atira
			if (Input.GetButton (atack) && Time.time > proximoTiro && QuantidadeBala>0) {
				Fire ();
			}
			//Pula
			if (Input.GetButton (vertical) && !pulando) {
				if(PlayerPrefs.GetInt ("Som")==1){
					somPulo.Play ();
				}

				pulando = true;
				rb2d.AddForce (new Vector2 (0f, ForcaPulo));

				Invoke ("Pulo",0.9f);
			}

		} 
	}

	void Pulo(){
		pulando = false;
	}

	void Fire(){
		//Animação de tiro
		QuantidadeBala--;

		if(PlayerPrefs.GetInt ("Som")==1){
			somTiro.Play ();
		}

		proximoTiro = Time.time+tempoTiro;
		//Cria tiro
		GameObject cloneBala = Instantiate (Bala, origemBala.position, origemBala.rotation);
		//Vira a bala
		if (sprite.flipX) {
			cloneBala.transform.eulerAngles = new Vector3 (0,0,180);
		}

	}
	void Flip (){
		//Vira jogador e local onde bala é criada
		sprite.flipX = !sprite.flipX;
		if (!sprite.flipX) {
			origemBala.position = new Vector3 (this.transform.position.x + 0.48f, origemBala.position.y, origemBala.position.z);

		} else {
			origemBala.position = new Vector3 (this.transform.position.x - 0.48f, origemBala.position.y, origemBala.position.z);

		}
	}

	IEnumerator Damage(){
		//Jogador fica invuneravel
		invunerabilidade = true;
		//Jogador pisca
		for (int i = 0; i < 3; i ++) {
			sprite.color = Color.red;
			yield return new WaitForSeconds (0.3f);
			sprite.color = Color.white;
			yield return new WaitForSeconds (0.3f);
		}
		//Jogador fica vuneravel
		invunerabilidade = false;
	}

	public void DamagePlayer(){

		if(isAlive){
			//Ativa a vunerabilidade
			invunerabilidade = true;
			//Perde vida
			life--;

			StartCoroutine (Damage());

			if(life < 1){
				//Morte
				isAlive = false;
				anim.SetTrigger ("Morte");
				Invoke ("ReloadLevel",3f);
			}
		}
	}

	void ReloadLevel(){
		//Recarrega fase atual
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.transform.tag == "Plataforma") {
			transform.parent = obj.transform;
		}
		if (obj.transform.tag == "Finish") {
			isAlive = false;
			anim.SetTrigger ("Morte");
			Invoke ("ReloadLevel",2f);
		}
		if (obj.transform.tag == "Trigger") {
			Ativadores x = obj.gameObject.GetComponent<Ativadores> ();

			x.ativado = true;

		}

	}
	void OnCollisionExit2D(Collision2D obj){
		if (obj.transform.tag == "Plataforma") {
			transform.parent = null;
		}

		if (obj.transform.tag == "Trigger") {
			Ativadores x = obj.gameObject.GetComponent<Ativadores> ();

			x.ativado = false;


		}

	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.transform.tag == "Vida") {
			if (life < 3) {
				life++;
			}
			Destroy (obj.gameObject);
		}
		if (obj.transform.tag == "Bau") {
			PlayerPrefs.SetInt (SceneManager.GetActiveScene().name, 1);
			if (SceneManager.GetActiveScene ().name == "Fase2") {
				SceneManager.LoadScene ("Fase3");
			} else {
				popup.SetActive (true);
			}
		}
		if(obj.gameObject.name == "Boss"){
			DamagePlayer ();
		}
	}
}