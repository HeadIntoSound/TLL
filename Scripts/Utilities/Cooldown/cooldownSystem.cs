using System.Collections.Generic;
using UnityEngine;

// Got it from https://www.youtube.com/watch?v=2zGvt4NnIYw
// The video explains this script better than I ever could
public class cooldownSystem : MonoBehaviour
{
    readonly List<cooldownData> cooldowns = new List<cooldownData>();

    void Update() => processCooldowns();

    void processCooldowns()
    {
        float deltaTime = Time.deltaTime;
        for (int i = cooldowns.Count - 1; i >= 0; i--)
        {
            if (cooldowns[i].decrementCooldown(deltaTime))
            {
                cooldowns.RemoveAt(i);
            }
        }
    }

    public void putOnCooldown(IHasCooldown cooldown)
    {
        cooldowns.Add(new cooldownData(cooldown));
    }

    public bool isOnCooldown(int id)
    {
        foreach (cooldownData cd in cooldowns)
        {
            if (cd.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public float getRemainingTime(int id)
    {
        foreach (cooldownData cd in cooldowns)
        {
            if (cd.id != id)
            {
                continue;
            }
            return cd.remainingTime;
        }
        return 0;
    }
}

public class cooldownData
{
    public cooldownData(IHasCooldown cooldown)
    {
        id = cooldown.id;
        remainingTime = cooldown.cooldownDuration;
    }

    public int id { get; }
    public float remainingTime { get; private set; }

    public bool decrementCooldown(float deltaTime)
    {
        remainingTime = Mathf.Max(remainingTime - deltaTime, 0f);

        return remainingTime == 0f;
    }
}
