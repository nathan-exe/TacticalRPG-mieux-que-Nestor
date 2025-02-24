using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] CanvasGroup _currentPanel;
    [field : SerializeField]public CanvasGroup FormerPanel { get; private set; } = null;
    public CanvasGroup GameOverPanel, WinPanel, PausePanel;

    [Header("Parameters")]
    [SerializeField] float _transitionTime = .2f;

    public static UiManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void GoToScene(int buildIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(buildIndex);
    }


    public void ShowPanel(CanvasGroup canvasGroup)
    {
        HideCurrentPanel();

        _currentPanel = canvasGroup;
        if (FormerPanel == canvasGroup) FormerPanel = null;

        _currentPanel.blocksRaycasts = true;
        _currentPanel.interactable = true;
        _currentPanel.DOFade(1, _transitionTime).SetUpdate(true);
    }

    public void HideCurrentPanel()
    {
        if (_currentPanel == null) return;
        _currentPanel.interactable = false;
        _currentPanel.blocksRaycasts = false;
        _currentPanel.DOFade(0, _transitionTime).SetUpdate(true);

        FormerPanel = _currentPanel;
        _currentPanel = null;
    }


    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    

    public void setTimeScale(float timeScale)
    {

    }

}
