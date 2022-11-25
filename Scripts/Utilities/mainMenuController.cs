using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onAnyButtonPress.CallOnce(_ => SceneManager.LoadScene(1,LoadSceneMode.Single));
    }
}
