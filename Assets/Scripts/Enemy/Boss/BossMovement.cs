using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private bool moveRight = true;

    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private EnemySpawner spawner;

    private void Update()
    {
        if (timeline.state != PlayState.Playing)
        {
            if (transform.position.x > -60f)
            {
                moveRight = false;
            }

            else if (transform.position.x < -137f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }

            spawner.gameObject.SetActive(true);
        }
        
    }
}
