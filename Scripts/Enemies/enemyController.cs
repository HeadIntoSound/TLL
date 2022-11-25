
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// I created a system that allows you to change between enemies in the editor
// Is useful when creating a level, it makes it so you don't have to drag the preset of the other object and delete the current
public class enemyController : MonoBehaviour
{
    // editor selection variables, sets the kind of enemy the object will represent
    [HideInInspector] public string[] enemy = new string[] { "Grab", "Trap", "Bomb", "Spit","Venom"};
    [HideInInspector] public int selectedEnemy;
    // Here is important to have the list completed with all the current enemies, same for the string array
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setObject(string name, int enemyIndex)
    {
        GameObject obj = Instantiate(prefabs.Find(obj => obj.name == name),transform.position,Quaternion.identity);
        obj.name = obj.name.Remove(obj.name.IndexOf('('));
        obj.GetComponent<enemyController>().selectedEnemy = enemyIndex;
        //Instantiate(prefabs.Find(obj => obj != null),transform.position,Quaternion.identity);
        DestroyImmediate(gameObject);
    }

    // sets enemy's properties, a method for each enemy. I call them in the inspector script via reflection
    public void grabEnemy(int enemyIndex)
    {
        //print("grab");
        setObject("Grab",enemyIndex);
    }

    public void trapEnemy(int enemyIndex)
    {
        //print("trap");
        setObject("Trap",enemyIndex);
    }

    public void bombEnemy(int enemyIndex)
    {
        //print("bomb");
        setObject("Bomb",enemyIndex);
    }
    public void spitEnemy(int enemyIndex)
    {
        //print("spit");
        setObject("Spit",enemyIndex);
    }
    public void venomEnemy(int enemyIndex)
    {
        setObject("Venom",enemyIndex);
    }
        
}
