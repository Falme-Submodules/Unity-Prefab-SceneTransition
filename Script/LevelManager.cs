using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
