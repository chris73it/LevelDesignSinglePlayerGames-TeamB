using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour
{
	public void ChangeLevel(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void QuitGame() {
		Application.Quit();
	}
}