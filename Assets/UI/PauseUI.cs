using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton, settingsButton, exitButton, soundButton;
    [SerializeField] private string startScene;
    [SerializeField] private GameObject settingsPanel, pausePanel, buttons;

    void Start()
    {
        continueButton.onClick.AddListener(Continue);
        settingsButton.onClick.AddListener(Settings);
        soundButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Exit);
    }

    void Exit()
    {
        SceneManager.LoadScene(startScene);
        Time.timeScale = 1;

    }

    void Continue()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = 1;
    }

    void Settings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
    }
}
