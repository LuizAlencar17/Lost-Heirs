using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour {

	public void ProximaFase(string fase){
		SceneManager.LoadScene (fase);
	}

	public void Menu(){
		SceneManager.LoadScene ("Menu");
	}
}
