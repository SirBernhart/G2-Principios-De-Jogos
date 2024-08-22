using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip idleAnimationClip; 
    [SerializeField] private Vector3 idlePosition;
    private Coroutine coroutine;
    public void PlayAnimation(AnimationClip animation, Vector2 positionToMoveTo, float duration)
    {
        animator.Play(animation.name);
        transform.position = positionToMoveTo;
        StartCoroutine(AnimationTimer(duration));
    }
    
    IEnumerator AnimationTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        StopAllAnimations();
    }
    
    private void StopAllAnimations()
    {
        StopAllCoroutines();
        AnimateIdle();
    }
    
    public void AnimateIdle()
    {
        transform.position = idlePosition;
        animator.Play(idleAnimationClip.name);
    }
}