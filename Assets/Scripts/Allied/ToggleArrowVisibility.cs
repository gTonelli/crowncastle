using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleArrowVisibility : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // init
        animator = GetComponentInParent<Animator>();
        // hide the arrow until we are ready to show it from AttackState
        GetComponent<Renderer>().enabled = false;
        AnimationClip animationClip;
        AnimationEvent setArrowVisibleEvent = new AnimationEvent(), setArrowInvisibleEvent = new AnimationEvent();
        setArrowVisibleEvent.time = 0.45f;
        setArrowVisibleEvent.functionName = "ToggleArrow";
        setArrowInvisibleEvent.time = ArcherFSM.arrowReleaseTime;
        setArrowInvisibleEvent.functionName = "ToggleArrow";

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "ShootingArrow")
            {
                if (clip.events.Length < 1)
                {
                    animationClip = clip;
                    animationClip.AddEvent(setArrowVisibleEvent);
                    animationClip.AddEvent(setArrowInvisibleEvent);
                    break;
                }
            }
        }
    }
}
