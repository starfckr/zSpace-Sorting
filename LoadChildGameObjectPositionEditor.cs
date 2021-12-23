using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace tutorial_2d
{
    [CustomEditor(typeof(LoadChildGameObjectPosition))]
    public class LoadChildGameObjectPositionEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LoadChildGameObjectPosition myScript = (LoadChildGameObjectPosition)target;

            if(GUILayout.Button("Set all child objects"))
            {
                myScript.SetAllChildSortings();
            }

            if(GUILayout.Button("Load all child positions"))
            {
                myScript.LoadAllChildPositions();
            }

            if(GUILayout.Button("Save all child positions"))
            {
                myScript.SaveAllChildPositions();
            }

            if(GUILayout.Button("Resolve sprite sorting layers"))
            {
                myScript.ResolveOverlappingSprites();
            }
            

        }

    }

}
