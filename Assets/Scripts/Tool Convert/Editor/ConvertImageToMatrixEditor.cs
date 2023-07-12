using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConvertImageToMatrix))]
public class ConvertImageToMatrixEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Convert"))
        {
            var t = (ConvertImageToMatrix)target;
            t.Convert();
        }
    }
}
