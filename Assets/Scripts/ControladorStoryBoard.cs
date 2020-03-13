using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorStoryBoard : MonoBehaviour {
	
	public void MudaCena () {
		SceneManager.LoadScene ("Fase0");
	}

}
