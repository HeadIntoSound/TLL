using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 
[CustomEditor(typeof(dialogueController))]
public class dialogueControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        dialogueController dialogueController = (dialogueController)target;

        if(GUILayout.Button("Load Text"))
            dialogueController.loadText();

        if(GUILayout.Button("Trim"))
            dialogueController.trimText();
    }
}
#endif