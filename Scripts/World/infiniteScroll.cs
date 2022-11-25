using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BackgroundObject
{
    public int index = 0;
    public float lastPos = 0;
    public Vector3 iniPos;
    public Transform transform;
}

public class infiniteScroll : MonoBehaviour
{
    public List<BackgroundObject> bgList = new List<BackgroundObject>();
    public Transform CameraTransform;
    private float DistanceToMove = 18;

    private void Start()
    {
        bgList.ForEach(delegate (BackgroundObject b)
        {
            b.iniPos = b.transform.localPosition;
        });
    }

    public void Update()
    {
        transform.position = new Vector3(CameraTransform.position.x, transform.position.y, 0);
        bgList.ForEach(delegate (BackgroundObject bg)
        {
            bg.transform.localPosition -= Vector3.right * Time.deltaTime;
            if (bg.transform.localPosition.x <= bg.iniPos.x - 10 * (bg.index + 2))
            {
                int indx = bg.index - 1 == -1 ? bgList.Count - 1 : bg.index - 1;
                bg.transform.localPosition = bgList[indx].iniPos + Vector3.right * 10;
                bg.iniPos = bgList[indx].iniPos;
                bg.index = indx;
            }

        });
        // if (Mathf.RoundToInt(CameraTransform.transform.position.x) % DistanceToTrigger == 0)
        // {
        //     if (lastPosition == Mathf.RoundToInt(CameraTransform.transform.position.x))
        //     {
        //         return;
        //     }

        //     BackgroundObject bgLast = bgList.Single(x => x.index == 2);
        //     BackgroundObject bgSecond = bgList.Single(x => x.index == 1);
        //     BackgroundObject bgFirst = bgList.Single(x => x.index == 0);

        //     lastPosition = Mathf.RoundToInt(CameraTransform.transform.position.x);

        //     bgFirst.transform.position = new Vector3(bgLast.lastPos + DistanceToMove, bgFirst.transform.position.y, bgFirst.transform.position.z);

        //     bgFirst.lastPos = bgLast.lastPos + DistanceToMove;

        //     bgSecond.index = 2;
        //     bgLast.index = 0;
        //     bgFirst.index = 1;

        // }
    }


    public void ResetBackground()
    {
        for (int i = 3; i >= 0; i--)
        {
            bgList[i].index = i + 1;

            bgList[i].lastPos = (DistanceToMove * i) - DistanceToMove;

            bgList[i].transform.position = new Vector3((DistanceToMove * i) - DistanceToMove, -2, 0);
        }
    }
}
