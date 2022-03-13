using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using Framework;

public class Editor : MonoBehaviour
{
    [MenuItem("Assets/Create/CreateStateClass %#d", false, 0)]
    private static void CreateStateClass()
    {
        GameObject selected = Selection.activeObject as GameObject;
        if (selected == null || selected.name.Length == 0)
         {
            Debug.LogError("A GameObject must be selected to provide the state class name.");
            return;
        }

        // remove whitespace and minus
        string fileName = selected.name.Replace(" ", "_").Replace("-", "_");

        string createPath = "Assets/" + fileName + ".cs";
        string copyFromPath = "Assets/Framework/Scripts/States/Base/ExampleState.cs";

        if (File.Exists(createPath) == false)
        {
            using (StreamWriter outfile = new StreamWriter(createPath))
            {
                if(File.Exists(copyFromPath) != false)
                {
                    using (StreamReader reader = new StreamReader(copyFromPath))
                    {
                        string contents = reader.ReadToEnd();
                        contents = contents.Replace("ExampleState", fileName);
                        outfile.WriteLine(contents);
                        Debug.Log("Creating State Class: " + fileName);
                    }
                }
            }
        }

        AssetDatabase.Refresh();
    }
/*
    [MenuItem("Assets/Create/AddComponent %#q", false, 1)]
    private static void AddStateClass()
    {
        GameObject selected = Selection.activeObject as GameObject;

        if (selected == null || selected.name.Length == 0)
        {
            Debug.LogError("A GameObject must be selected to provide the state class name.");
            return;
        }

        string copyFromPath = "Assets/Framework/Scripts/States/Base/ExampleState.cs";
        Type type = AssetDatabase.GetMainAssetTypeAtPath(copyFromPath);
        

        if (type != null)
        {
            Debug.Log(type.ToString());
            selected.AddComponent("ExampleState");
            Debug.Log(selected.GetComponent<ExampleState>());
        }
        else
        {
            Debug.Log("none found");
        }
    }*/
}
