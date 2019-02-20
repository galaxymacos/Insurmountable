using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private int menuIndex = 0;

	public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameManager.Instance.gameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
    }

    private void Pause()
    {
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameManager.Instance.gameIsPaused = true;
    }

    public void Resume()
    {
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameManager.Instance.gameIsPaused = false;
    }

    public void LoadMenu()
    {
	    Time.timeScale = 1f;
	    SceneManager.LoadScene(menuIndex);
    }

    public void Quit()
    {
		Application.Quit();
    }
}
