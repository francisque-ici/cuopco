using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; set;}

    public AudioSource Flag;
    public AudioSource Fall;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void PlayFlag()
    {
        Flag.Play();
    }
    
    public void PlayFall()
    {
        Fall.Play();
    }
}
