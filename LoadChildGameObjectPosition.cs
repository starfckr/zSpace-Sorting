using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutorial_2d
{
    public class LoadChildGameObjectPosition : MonoBehaviour
    {

        [SerializeField] private ZDepthSortingObject zDepthSortingObject;

        public void SetAllChildSortings()
        {

            foreach(Transform child in transform)
            {
                ILoadGameObjectPosition objectToSet = child.gameObject.GetComponent<ILoadGameObjectPosition>();

                if(objectToSet != null)
                {
                    objectToSet.LoadGameObjectPosition(gameObject, ZDepthAction.SET);
                }
                else
                {
                    Debug.Log("Could not find interface ILoadGameObjectPostion on child");
                }
            }
            

        }

        

        public void LoadAllChildPositions()
        {
            foreach(Transform child in transform)
            {


                ILoadGameObjectPosition objectToLoad = child.gameObject.GetComponent<ILoadGameObjectPosition>();

                if(objectToLoad != null)
                {
                    objectToLoad.LoadGameObjectPosition(gameObject, ZDepthAction.LOAD);
                }
                else
                {
                    Debug.Log("Could not find interface ILoadGameObjectPosition on child");
                }
            }

        }

        public void SaveAllChildPositions()
        {
            foreach(Transform child in transform)
            {
                ILoadGameObjectPosition objectToSave = child.gameObject.GetComponent<ILoadGameObjectPosition>();

                if(objectToSave != null)
                {
                    objectToSave.LoadGameObjectPosition(gameObject, ZDepthAction.SAVE);
                }
                else
                {
                    Debug.Log("Could not find interface ILoadGameObjectPosition on child");
                }

            }
        }

        public void ResolveOverlappingSprites()
        {
            
            Dictionary<ZDepthSortingGroupName, List<GameObject>> gameobjectsByZDepth = new Dictionary<ZDepthSortingGroupName, List<GameObject>>();
            List<ZDepthSortingGroupName> _sortingGroups = new List<ZDepthSortingGroupName>();


            foreach(Transform child in transform)
            {
                ZDepthSortingGroupName _sgName = child.GetComponent<ZDepthSorting>().SortingGroupName;
                if(gameobjectsByZDepth.TryGetValue(_sgName, out List<GameObject> _objectsList))
                {
                    _objectsList.Add(child.gameObject);
                }
                else
                {
                    List<GameObject> _newObjectList = new List<GameObject>();
                    _newObjectList.Add(child.gameObject);
                    gameobjectsByZDepth.Add(_sgName, _newObjectList);
                    _sortingGroups.Add(_sgName);
                }
                
            }

            foreach(ZDepthSortingGroupName _sgName in _sortingGroups)
            {

                Queue<int> _sortingOrderQueue = new Queue<int>();
                for (int i = 0; i < zDepthSortingObject.sortingGroups.Length; i++)
                {
                    if(_sgName == zDepthSortingObject.sortingGroups[i].zDepthGroupName)
                    {

                        var dir = zDepthSortingObject.sortingGroups[i].maxSortingLayerOrder > 0 ? 1 : -1;
                        var min = zDepthSortingObject.sortingGroups[i].maxSortingLayerOrder > 0 ? 0 : zDepthSortingObject.sortingGroups[i].maxSortingLayerOrder;
                        var max = zDepthSortingObject.sortingGroups[i].maxSortingLayerOrder > 0 ? zDepthSortingObject.sortingGroups[i].maxSortingLayerOrder : 0;

                        for (int y = zDepthSortingObject.sortingGroups[i].minSortingLayerOrder; y >= min && y <= max; y += dir)
                        {
                            _sortingOrderQueue.Enqueue(y);
                            //Debug.Log(y);
                        }
                    }
                }

                if(gameobjectsByZDepth.TryGetValue(_sgName, out List<GameObject> _objectsToSort))
                {
                    foreach(GameObject _objectToSort in _objectsToSort)
                    {
                        
                        if (_objectToSort.GetComponent<ZDepthSorting>().SetSortingLayer(_sortingOrderQueue.Peek()))
                        {
                            _sortingOrderQueue.Dequeue();
                        }
                        //else
                        //{
                        //    Debug.Log("Object was set to override and we did not use " + _sortingOrderQueue.Peek());
                        //}
                        
                    }
                    _sortingOrderQueue.Clear();
                }
                //else
                //{
                //    Debug.Log("Could not find any gameobjects to sort");
                //}
            }
      

        }

        

    }

}
