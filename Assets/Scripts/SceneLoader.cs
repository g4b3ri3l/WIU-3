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
    private TMP_Text percentText;
    [SerializeField] private GameObject loadingPanel;
    // Start is called before the first frame update

    private void Start()
    {
        percentText = GetComponent<TMP_Text>();
    }
    public void LoadScene(string scenename)
    {
        StartCoroutine(LoadSceneRoutine(scenename));
    }

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        AsyncOperationHandle op =
        Addressables.LoadSceneAsync(sceneName);
        while (op.PercentComplete < 1)
        {
            percentText.text =
            string.Format("Loading: {0}%",
            (int)(op.PercentComplete * 100));
            yield return null;
        }
    }


}
