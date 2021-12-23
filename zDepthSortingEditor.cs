using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace tutorial_2d
{
    [CustomEditor(typeof(ZDepthSorting))]
    public class zDepthSortingEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            DrawDefaultInspector();

            ZDepthSorting myScript = (ZDepthSorting)target;

            if(GUILayout.Button("Sort object"))
            {
                myScript.ChangeSorting();
            }

            if(GUILayout.Button("Save position"))
            {
                myScript.SavePositionData();
            }

            if(GUILayout.Button("Load Position"))
            {
                myScript.LoadPositionData();
            }

        }


    }

}
