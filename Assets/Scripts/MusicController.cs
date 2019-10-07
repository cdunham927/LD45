using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip clip;
    AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        src.PlayOneShot(clip);
    }
}
