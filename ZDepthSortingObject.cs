using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutorial_2d
{
    [CreateAssetMenu(fileName = "newZdepthSortingObject", menuName = "Data/Utility/Z-depth sorting order")]
    public class ZDepthSortingObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ZDepthSortingGroup[] sortingGroups;
        public ZDepthSortingGroupDictionary sortingGroupDictionary;

        public void OnAfterDeserialize()
        {
            sortingGroupDictionary = new ZDepthSortingGroupDictionary(sortingGroups);
        }

        public void OnBeforeSerialize()
        {
            
        }
    }

    public class ZDepthSortingGroupDictionary
    {
        public Dictionary<ZDepthSortingGroupName, ZDepthSortingGroup> SortingGroupsDictionary = new Dictionary<ZDepthSortingGroupName, ZDepthSortingGroup>();

        public ZDepthSortingGroupDictionary(ZDepthSortingGroup[] _sortingGroups)
        {

            foreach(ZDepthSortingGroup _sortingGroup in _sortingGroups)
            {
                SortingGroupsDictionary.Add(_sortingGroup.zDepthGroupName, _sortingGroup);
            }

        }

    }
}
