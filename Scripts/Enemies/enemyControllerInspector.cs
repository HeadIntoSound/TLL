using System;
using UnityEngine;
using UnityEditor;

// Custom inspector for the enemy controller script. Sets the enemy using a dropdown
#if (UNITY_EDITOR) 
[CustomEditor(typeof(enemyController))]
public class enemyControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        enemyController enemyController =  (enemyController)target;
        GUIContent labels = new GUIContent("Enemy Type");
        int aux = EditorGUILayout.Popup(labels,enemyController.selectedEnemy,enemyController.enemy);
        if(aux != enemyController.selectedEnemy)
        {
            enemyController.selectedEnemy = aux;
            string methodName = enemyController.enemy[enemyController.selectedEnemy].ToLower() + "Enemy";
            System.Reflection.MethodInfo info = typeof(enemyController).GetMethod(methodName);
            info.Invoke(enemyController,new object[]{aux});
        }

    }
}
#endif