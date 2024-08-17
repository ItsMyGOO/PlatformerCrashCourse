using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1;
    public bool playOnEnter, playOnExit, playAfterDelay;

    public float playDelay;
    public float timeSinceEntered;
    public bool hasDelaySoundPlayed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);

        timeSinceEntered = 0;
        hasDelaySoundPlayed = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelaySoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if (timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelaySoundPlayed = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
    }
}
