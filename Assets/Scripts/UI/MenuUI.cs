using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button newStartButton, continueButton, settingsButton, exitButton;
    [SerializeField] private string startScene, continueScene;
    [SerializeField] private GameObject settingsPanel;
    void Start()
    {
         settingsPanel.SetActive(false);
        newStartButton.onClick.AddListener(newStart);
        continueButton.onClick.AddListener(ContinueStart);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Application.Quit);
    }

    void newStart()
    {
        SceneManager.LoadScene(startScene);
    }

    void ContinueStart()
    {
        SceneManager.LoadScene(continueScene);
        //NEED MORE STUFF
    }

    void Settings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
