using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpeciesInfoTool))]
public class InfoToolingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpeciesInfoTool myScript = (SpeciesInfoTool)target;
        if(GUILayout.Button("Load Script"))
        {
            myScript.LoadInfo();
        }
        if (GUILayout.Button("Save Script"))
        {
            myScript.SaveInfo();
        }
    }
}
