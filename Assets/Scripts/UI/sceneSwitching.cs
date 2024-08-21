using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class sceneSwitching : MonoBehaviour
{
    [SerializeField] string Nextlevel;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Task 2c - Load the scene using SceneManager.LoadScene()
            DataPersistanceManager.instance.SaveGame();
            SceneManager.LoadScene(Nextlevel);
            DataPersistanceManager.instance.LoadGame();
        }
    }
}
