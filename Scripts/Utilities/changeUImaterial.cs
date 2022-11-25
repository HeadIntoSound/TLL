using UnityEngine;
using UnityEngine.UI;

// Changes the material from an image, used for the shader on the haunt screen
public class changeUImaterial : MonoBehaviour
{
    Image img;
    [SerializeField] Material uiMat;
    [Range(0, 5)] public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        gameEvents.current.onPassFloat += fade;
        img = GetComponent<Image>();
        img.gameObject.SetActive(true);
        img.material = uiMat;
        img.material.SetFloat("_Offset", intensity);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void set()
    {
        img = GetComponent<Image>();
        img.gameObject.SetActive(true);
        img.material = uiMat;
        img.material.SetFloat("_Offset", intensity);
    }
    void fade(string parameter, float value)
    {
        if (parameter.Contains("fade"))
        {
            intensity = value;
            img.material.SetFloat("_Offset", intensity);
            // if(intensity < 1.1f)
            // {
            //     gameEvents.current.passBool("death", false);
            //     return;
            // }
            // if(intensity > 4.9f)
            //     gameEvents.current.passBool("death", true);
        }

    }
}
