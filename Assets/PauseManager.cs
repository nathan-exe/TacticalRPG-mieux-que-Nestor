using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused = false;
    
    public void PauseGame()
    {
        UiManager.Instance.ShowPanel(UiManager.Instance.PausePanel);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        if (UiManager.Instance.FormerPanel != null)
        {
            UiManager.Instance.ShowPanel(UiManager.Instance.FormerPanel);
        }
        else
        {
            Debug.Log("ohhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh-");
            UiManager.Instance.HideCurrentPanel();
        }
        
        isPaused = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
