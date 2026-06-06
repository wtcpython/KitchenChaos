using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        GameMenuScene = 0,
        LoadingScene,
        GameScene
    }

    private static Scene s_targetScene;

    public static void Load(Scene targetScene)
    {
        Time.timeScale = 1;
        s_targetScene = targetScene;
        SceneManager.LoadScene((int)Scene.LoadingScene);
    }

    public static void LoaderBack()
    {
        SceneManager.LoadScene((int)s_targetScene);
    }
}
