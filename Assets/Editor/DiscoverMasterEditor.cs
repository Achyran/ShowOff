using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DiscoverableMaster))]
public class DiscoverMasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DiscoverableMaster discoverableMaster = (DiscoverableMaster)target;
        if (GUILayout.Button("Reset Save"))
        {
            discoverableMaster.ResetSave();
        }
    }
}
