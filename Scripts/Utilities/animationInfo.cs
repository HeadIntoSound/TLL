using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class animationInfo
{
    public static float animLength(Animator anim, string clipName)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Contains(clipName))
                return clip.length;
        }
        return 0;
    }
}
