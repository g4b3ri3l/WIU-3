using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeGroup : MonoBehaviour
{
    [SerializeField] Item slot1, slot2;
    [SerializeField] PlayerManager manager;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip EatClip;
    [SerializeField] private float EatingCooldown = 1.0f;
    private float EatingSoundCooldownTimer = 0f;

    void Update()
    {
        if (EatingSoundCooldownTimer > 0)
        {
            EatingSoundCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (slot1.itemCount > 0 && manager.health != manager.maxHp)
            {

                if (EatingSoundCooldownTimer <= 0f)
                {
                    audioSource.PlayOneShot(EatClip);
                    EatingSoundCooldownTimer = EatingCooldown; // Reset the cooldown timer
                }
                slot1.itemCount--;
                manager.Heal(10);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (slot2.itemCount > 0 && manager.health != manager.maxHp)
            {

                if (EatingSoundCooldownTimer <= 0f)
                {
                    audioSource.PlayOneShot(EatClip);
                    EatingSoundCooldownTimer = EatingCooldown; // Reset the cooldown timer
                }
                slot2.itemCount--;
                manager.Heal(20);
            }
        }

    }
}
