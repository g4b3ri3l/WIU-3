using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] BossMovement boss;

    public AudioSource currentBGM;
    public AudioClip newBGM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeline.gameObject.SetActive(true);
            boss.gameObject.SetActive(true);


            if (currentBGM.isPlaying)
            {
                currentBGM.Stop();
            }
            currentBGM.clip = newBGM;
            currentBGM.Play();
        }

    }
}
