using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// This script moves the camera slightly to the bottom when the player is falling, so they can anticipate their next move

public class cameraOffset : MonoBehaviour
{
    [SerializeField] CinemachineCameraOffset cmOffset;
    [SerializeField] float yOffset;
    [SerializeField] float speed = 3;
    float t;

    private void Start()
    {
        cmOffset = GetComponent<CinemachineCameraOffset>();
        cmOffset.m_Offset.y = 0;
        gameEvents.current.onPassBool += delegate (string parameter, bool value)
        {
            if (parameter.Contains("cam") && value)
                this.enabled = false;
        };
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(offsetLerp(cmOffset.m_Offset.y, yOffset, 1, speed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        StartCoroutine(offsetLerp(cmOffset.m_Offset.y, 0, -1, speed * .75f));
    }

    IEnumerator offsetLerp(float iniValue, float toValue, int reverse, float sp)
    {
        t = 0;
        if (reverse > 0)
            yield return new WaitForSeconds(.25f);
        while (cmOffset.m_Offset.y != toValue)
        {
            cmOffset.m_Offset.y = Mathf.Lerp(iniValue, toValue, t);     // An ease method could be applied
            t += Time.deltaTime * sp;
            yield return null;
        }
    }

}
