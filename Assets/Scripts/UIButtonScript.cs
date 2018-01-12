using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonScript : MonoBehaviour {

	public void onPlayClick()
	{
		SceneManager.LoadScene("gamescene");
	}

	public void onMenuClick()
	{
		SceneManager.LoadScene("mainmenuscene");
        GameManager.QuitGame();
	}

	public void onQuitClick()
	{
		Application.Quit();
	}
}
