using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("death of instance");
            return;
        }
        instance = this;
    }
    #endregion
    // Start is called before the first frame update

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}
