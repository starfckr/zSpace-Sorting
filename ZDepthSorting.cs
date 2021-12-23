using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace tutorial_2d
{
    public class ZDepthSorting : MonoBehaviour, ILoadGameObjectPosition
    {
        

        public ZDepthSortingGroupName SortingGroupName { get => sortingGroupName; private set => sortingGroupName = value; }
        public ZDepthSortingObject SortingGroupObject { get => zDepthSortingObject; private set => zDepthSortingObject = value; }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ZDepthSortingObject zDepthSortingObject;
        [SerializeField] private ZDepthSortingGroupName sortingGroupName;
        
        [SerializeField] private SpriteListObject spriteVariations;
        [SerializeField] private ZDepthSortingVector3Buffer vector3buffer;
        [SerializeField] private bool overrideSortingGroup;

        [SerializeField] private Material farBackgroundMaterial;
        [SerializeField] private Material nearBackGroundMaterial;
        

        public void ChangeSorting()
        {
            Debug.Log("Change some sorting stuff");

            if(zDepthSortingObject.sortingGroupDictionary.SortingGroupsDictionary.TryGetValue(sortingGroupName, out ZDepthSortingGroup _sortingGroup))
            {
                SetNewGameObjectName(_sortingGroup);
                if (!overrideSortingGroup)
                {
                    SetSortingLayer(Random.Range(_sortingGroup.minSortingLayerOrder, _sortingGroup.maxSortingLayerOrder));
                    
                    spriteRenderer.sortingLayerName = _sortingGroup.sortObjectInSortingLayer;

                    gameObject.layer = Mathf.RoundToInt(Mathf.Log(_sortingGroup.sortObjectinLayer.value, 2));

                    transform.localScale = _sortingGroup.zDepthScale;



                    Vector3 vec = new Vector3(transform.position.x, transform.position.y, _sortingGroup.zDepthGroupZdepth);

                    transform.position = vec;

                    if(transform.position.z > 0)
                    {
                        spriteRenderer.material = farBackgroundMaterial;
                    }
                    else
                    {
                        spriteRenderer.material = nearBackGroundMaterial;
                    }

                    if (_sortingGroup.zDepthSpriteAffix != null)
                    {
                        foreach (Sprite _spriteVar in spriteVariations.sprites)
                        {
                            if (_spriteVar.name.Contains(_sortingGroup.zDepthSpriteAffix))
                            {
                                spriteRenderer.sprite = _spriteVar;
                            }
                        }

                    }
                    else
                    {
                        Debug.Log("Could not find sprite");
                    }
                }
   
            }
            else
            {
                Debug.Log("No sorting group is set on background element");
            }
        }

        private void SetNewGameObjectName(ZDepthSortingGroup _sortingGroup)
        {

            string _currentName = gameObject.name;
            string _spriteListName = spriteVariations.spriteListName;

            if (_currentName.Contains("-"))
            {
                int _index = _currentName.IndexOf("-");
                if (_index >= 0)
                {
                    _currentName = _currentName.Substring(0, _index);
                }
            }
            else if (_currentName.Contains("("))
            {
                int _index = _currentName.IndexOf("(");
                if (_index >= 0)
                {
                    _currentName = _currentName.Substring(0, _index);
                }
            }

            if (overrideSortingGroup)
            {
                gameObject.name = _currentName + "-" + "OVERRIDE";
            }
            else
            {
                gameObject.name = _spriteListName + "-" + _sortingGroup.zDepthGroupName + "_" + _sortingGroup.zDepthSpriteAffix;
            }
            
        }

        public void SavePositionData()
        {
            if(vector3buffer.zDepthBuffer.TryGetValue(gameObject, out Vector3 buffer))
            {
                vector3buffer.ChangeValueInBuffer(gameObject, transform.position, sortingGroupName);
            }
            else
            {
                vector3buffer.AddToBuffer(gameObject, transform.position, sortingGroupName);
            }
            
        }

        public void LoadPositionData()
        {
            if(vector3buffer.zDepthBuffer.TryGetValue(gameObject, out Vector3 buffer))
            {
                transform.position = buffer;
                vector3buffer.RemovePosDataFromBuffer(gameObject);
            }
            else
            {
                Debug.Log("No POS buffer found for " + gameObject);
            }

            if(vector3buffer.zDepthBufferSGname.TryGetValue(gameObject, out ZDepthSortingGroupName bufferName))
            {
                sortingGroupName = bufferName;
                ChangeSorting();
                vector3buffer.RemoveSGnameDataFromBuffer(gameObject);
            }
            else
            {
                Debug.Log("No SG-name buffer found for " + gameObject);
            }
        }

        public void LoadGameObjectPosition(GameObject parentObject, ZDepthAction action)
        {
            if (action == ZDepthAction.LOAD)
            {
                if (transform.parent.gameObject == parentObject)
                {
                    LoadPositionData();
                }
            }
            else if(action == ZDepthAction.SAVE)
            {
                if(transform.parent.gameObject == parentObject)
                {
                    SavePositionData();
                }
            }
            else if(action == ZDepthAction.SET)
            {
                if(transform.parent.gameObject == parentObject)
                {
                    ChangeSorting();
                }
            }    
        }

        public bool SetSortingLayer(int _sortingLayer)
        {
            if (!overrideSortingGroup)
            {
                spriteRenderer.sortingOrder = _sortingLayer;
                //Debug.Log(gameObject + " has been set to sorting layer " + _sortingLayer);
                return true;
            }
            else return false;
        }


        // used for changing sprites from automated process
        public void ChangeSpriteListObject(SpriteListObject _spriteListObject)
        {
            spriteVariations = _spriteListObject;
            spriteRenderer.sprite = _spriteListObject.sprites[0];
        }


        


    }

    [System.Serializable]
    public class ZDepthSortingGroup
    {
        public ZDepthSortingGroupName zDepthGroupName;
        public string sortObjectInSortingLayer;
        public LayerMask sortObjectinLayer;
        public float zDepthGroupZdepth;
        public int maxSortingLayerOrder;
        public int minSortingLayerOrder;
        public SortingLayer zDepthGroupSortingLayer;
        public Vector3 zDepthScale;
        public string zDepthSpriteAffix;
    }



}

