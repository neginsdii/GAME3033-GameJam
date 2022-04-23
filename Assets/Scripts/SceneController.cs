using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
	public GameObject PausePanel;
    public void LoadGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Main 1");
	}

	public void LoadTutorial()
	{
		SceneManager.LoadScene("Instructions");
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");

	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void puase()
	{
		PausePanel.SetActive(true);
		Time.timeScale = 0;
	}


	public void unpause()
	{
		PausePanel.SetActive(false);

		Time.timeScale = 1;
	}
}
