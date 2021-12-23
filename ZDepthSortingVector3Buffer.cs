using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutorial_2d
{
    [CreateAssetMenu(fileName = "newzDepthSortingBuffer", menuName = "Data/Utility/zDepth Sorting Buffer")]
    public class ZDepthSortingVector3Buffer : ScriptableObject
    {

        public Dictionary<GameObject, Vector3> zDepthBuffer = new Dictionary<GameObject, Vector3>();
        public Dictionary<GameObject, ZDepthSortingGroupName> zDepthBufferSGname = new Dictionary<GameObject, ZDepthSortingGroupName>();

        public void AddToBuffer(GameObject _object, Vector3 _position, ZDepthSortingGroupName _name)
        {
            zDepthBuffer.Add(_object, _position);
            zDepthBufferSGname.Add(_object, _name);
        }

        public void RemovePosDataFromBuffer(GameObject _object)
        {
            zDepthBuffer.Remove(_object);
            
        }

        public void RemoveSGnameDataFromBuffer(GameObject _object)
        {
            zDepthBufferSGname.Remove(_object);
        }

        [ContextMenu("Clear buffer")]
        public void ClearBuffer()
        {
            zDepthBuffer.Clear();
            zDepthBufferSGname.Clear();
            zDepthBuffer = new Dictionary<GameObject, Vector3>();
            zDepthBufferSGname = new Dictionary<GameObject, ZDepthSortingGroupName>();
        }

        public void ChangeValueInBuffer(GameObject _object, Vector3 _position, ZDepthSortingGroupName _name)
        {
            zDepthBuffer[_object] = _position;
            zDepthBufferSGname[_object] = _name;
        }

        
    }

}
