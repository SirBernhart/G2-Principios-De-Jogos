using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 idlePosition;
    [SerializeField] private Vector3 cuttingPosition;
    [SerializeField] private Vector3 rollingPosition;
    [SerializeField] private Vector3 preparingPosition;
    private Coroutine coroutine;

    public void StopAllAnimations()
    {
        StopAllCoroutines();
        animator.SetBool("Cortando", false);
        animator.SetBool("Enrolando", false);
        animator.SetBool("Preparando", false);
        AnimateIdle();
    }

    public void AnimateCutting()
    {
        animator.SetBool("Idling", false);
        transform.position = cuttingPosition;
        animator.SetTrigger("Cortando");
        coroutine = StartCoroutine(AnimationTimer(1f));
    }

    public void AnimateIdle()
    {
        transform.position = idlePosition;
        animator.SetBool("Idling", true);
    }

    public void AnimateRolling()
    {
        animator.SetBool("Idling", false);
        transform.position = rollingPosition;
        animator.SetTrigger("Enrolando");
        StartCoroutine(AnimationTimer(1f));
    }

    public void AnimatePreparing()
    {
        animator.SetBool("Idling", false);
        transform.position = preparingPosition;
        animator.SetTrigger("Preparando");
        StartCoroutine(AnimationTimer(1f));
    }

    IEnumerator AnimationTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        StopAllAnimations();
    }
}
