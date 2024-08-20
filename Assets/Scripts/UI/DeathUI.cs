using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Continue(string stuff)
    {
        SceneManager.LoadScene(stuff);

    }


}
