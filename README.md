# UnityLevelManager

For better game scene management.

Provides an implementation of "Levels" as stacks of scenes that are loaded and unloaded together. 

- Define levels in editor
- Top to bottom load order (reverse unload order)
- Load and unload by name

![image](https://user-images.githubusercontent.com/39347669/126259473-2a73c996-ac9e-4b0c-b0ac-2cce237f8d56.png)

TODO:
- Convert to async
- Persistent scenes across multiple levels
- Better readme

Uses [Unity Scene Reference](https://github.com/JohannesMP/unity-scene-reference) to handle dragging and dropping scenes.
