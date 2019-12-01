using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource musicLoop;
    [SerializeField] AudioSource musicIntro;

    private void Start()
    {
        StartCoroutine(WaitToStartLoop());
    }

    IEnumerator WaitToStartLoop()
    {
        while (true)
        {
            if (!musicIntro.isPlaying)
            {
                musicLoop.Play();
                break;
            }
            yield return null;
        }
    }
}
