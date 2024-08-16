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
        //loadingPanel.transform.position = new Vector3 (loadingPanel.transform.position.x, loadingPanel.transform.position.y + 1000, 0);
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
