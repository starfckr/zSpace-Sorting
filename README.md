# zSpace-Sorting
Small tool for sorting sprites in z-depth when combining orthographic and perspective cameras in Unity.

The main purposes of this tool is to handle z-depth sorting of background and foreground elements in cases where you have many "steps" in z-depth. The tool allows you to set defaults for each individual sprite, as well as automatically set some parameters on objects:
- change sprite (usually because of different baked blur levels)
- change name (to make it easier to identify which "layer" the sprite is on
- change sorting layer
- change z-depth
- change scale
- change material

How to use it:
- LoadChildGameObject.cs goes on main GameObject in hierarchy that every other gameobject that needs sorting is childs of.
- ZDepthSorting.cs goes on every gameobject that needs sorting.
- ZDepthSortingSortingVector3Buffer.cs is a ScriptableObject that is used by objects to store their information while in playmode, and restore that information while in the editor.
- ZDepthSortingObject.cs defines all the default groups and settings.
- Editorfiles goes in an /editor/ folder in the project

Sprites are stored in a spriteList scriptable object, following a naming convention for level of blur.

When in play mode, sprite position can be saved on individual sprites, or all sprites in hierarchy below gameobject containing ZDepthSorting.cs. When exiting playmode, sprite position can be set in the same fashion. 

After adding a new sprite, it can be sorted automatically based on the group settings.


