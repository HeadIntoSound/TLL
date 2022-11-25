using UnityEngine;
using UnityEditor;

// Custom inspector for the AddForce class
#if (UNITY_EDITOR) 
[CustomEditor(typeof(addForce))]
public class addForceInspector : Editor
{
    public  override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        addForce addForce = (addForce)target;
        if(GUILayout.Button("Add Force"))
            addForce.push();
    }
}
#endif