using UnityEngine;

public class playerAnimController : MonoBehaviour
{
    Animator anim;

    // Plays the desired animation clip
    public void setAnimation(string clipName)
    {
        anim.Play(clipName);
    }

    // Returns the clip's length, useful for when you need to wait till the end of an animation for something
    // Later I moved the method to an static class but for sake of compatibility I kept this method
    public float animLength(string clipName)
    {
        return animationInfo.animLength(anim, clipName);
    }

    void Awake() {
        anim = GetComponent<Animator>();    
    }
}
