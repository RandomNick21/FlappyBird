using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerButton : MonoBehaviour
{
    //CustomInspector
    [SerializeField] private Scene _scene;
    [SerializeField] private int _id;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeScene);
    }

    private void ChangeScene()
    {
        switch (_scene)
        {
            case Scene.Load:
                SceneManager.LoadScene(_id);
                return;
            case Scene.Reload:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            case Scene.LoadAsync:
                SceneManager.LoadSceneAsync(_id);
                return;
            case Scene.ReloadAsync:
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                return;
        }
    }
}