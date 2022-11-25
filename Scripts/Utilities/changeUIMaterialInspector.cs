using UnityEditor;
using UnityEngine;

#if (UNITY_EDITOR)
[CustomEditor(typeof(changeUImaterial))]
public class changeUImaterialInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        changeUImaterial changeUImaterial = (changeUImaterial)target;
        if(GUILayout.Button("Set intensity"))
            changeUImaterial.set();
    }
}
#endif