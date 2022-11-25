using System.Collections;
using UnityEngine;

// This controls the haunt screen (time killer or whatever name they're using)
// The trigger is in playerMovementController
public class hauntTimer : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float speed = 0.05f;
    [SerializeField][Range(0, 5)] float min = 0;
    [SerializeField][Range(0, 5)] float max = 5;
    float maxMax; // Reference to initial max value
    float currentLVL;

    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.onPassFloat += stopwatch;
        maxMax = max;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void stopwatch(string parameter, float value)
    {
        if (parameter.Contains("haunt"))
        {
            StopAllCoroutines();
            if (value > 0)
                StartCoroutine(idleTimer(Time.time, value, 0));
            else
                StartCoroutine(fadeOut());
        }
    }

    IEnumerator idleTimer(float startTime, float idleTime, float t)
    {
        while (Time.time < startTime + idleTime)
        {
            yield return null;
        }
        while (t <= 1)
        {
            currentLVL = Mathf.Lerp(min, max, t);
            gameEvents.current.passFloat("fade", currentLVL);
            t += Time.deltaTime * speed;
            yield return null;
        }
        yield break;
    }

    IEnumerator fadeOut()
    {
        float t = 0;
        float localMin = currentLVL;
        while (t <= 1 && currentLVL > 0.1f)
        {
            currentLVL = Mathf.Lerp(localMin, min, t);
            gameEvents.current.passFloat("fade", currentLVL);
            t += Time.deltaTime * speed*6;
            yield return null;
        }
        gameEvents.current.passFloat("fade", 0);
        yield break;
    }
}
