using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour {

	public string namaScene;

	public void MovetoScene()
	{
		Scene sceneIni = SceneManager.GetActiveScene();

		if(sceneIni.name != namaScene){
			SceneManager.LoadScene (namaScene);
		}
	}
	public void Quit(){
		Application.Quit();
	}
}
