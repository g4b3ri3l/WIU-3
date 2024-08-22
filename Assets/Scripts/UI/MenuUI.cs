using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button newStartButton, continueButton, settingsButton, exitButton;
    [SerializeField] private string continueScene;
    [SerializeField] private GameObject settingsPanel, loadingPanel;
    void Start()
    {
        settingsPanel.SetActive(false);
        newStartButton.onClick.AddListener(newStart);
        continueButton.onClick.AddListener(ContinueStart);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Application.Quit);
    }

    public void newStart()
    {
        SceneManager.LoadScene(continueScene);
    }

    void ContinueStart()
    {
        
    }

    void Settings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
