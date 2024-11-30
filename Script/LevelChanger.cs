using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    [ValueDropdown("SelectScene", DropdownTitle = "Scene Selection")]
    public string sceneName;

    private static IEnumerable SelectScene()
    {
        List<string> scenes = new List<string>();

        for(int a=0; a<SceneManager.sceneCountInBuildSettings; a++)
            scenes.Add(
                System.IO.Path.GetFileNameWithoutExtension(
                    SceneUtility.GetScenePathByBuildIndex(a)
                )
            );

        return scenes;
    }

    public virtual void ChangeLevel()
    {
        LevelManager.Instance.LoadScene(sceneName);
    }
}
