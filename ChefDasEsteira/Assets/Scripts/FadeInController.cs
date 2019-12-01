using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInController : MonoBehaviour
{
    private void Start()
    {
        Invoke("DisableFade", 2f);
    }

    private void DisableFade()
    {
        gameObject.SetActive(false);
    }
}
