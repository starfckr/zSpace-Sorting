using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace tutorial_2d
{
    [CreateAssetMenu(fileName = "newSpriteListObject", menuName = "Data/Objects/Sprite List Object")]
    public class SpriteListObject : ScriptableObject
    {
        public string spriteListName;
        public List<Sprite> sprites = new List<Sprite>();


    }

}
