using UnityEngine.SceneManagement;

public static class SceneManagerExtensions
{
    public static void ReloadScene(this SceneManager sceneManager)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
