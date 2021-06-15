using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    [SerializeField] AudioClip[] death, doubl, jump, swich;

    public static Sounds Instante;

    public void Awake()
    {
        Instante = this;
    }

    public void PlayDeath()
    {
        audio.PlayOneShot(death[Random.Range(0, 2)]);
    }
    public void PlayDouble()
    {
        audio.PlayOneShot(doubl[Random.Range(0, 2)]);
    }

    public void PlayJump()
    {
        audio.PlayOneShot(jump[Random.Range(0, 2)]);
    }

    public void PlaySwitch()
    {
        audio.PlayOneShot(swich[Random.Range(0, 2)]);
    }
}
