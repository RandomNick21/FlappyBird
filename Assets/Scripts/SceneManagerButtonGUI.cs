using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(SceneManagerButton))]
public class SceneManagerButtonGUI : Editor
{
    private FieldInfo _idField;
    private FieldInfo _sceneField;
    
    private int ID
    {
        get => (int) _idField.GetValue(target);
        set => _idField.SetValue(target, value);
    }

    private Scene Scene
    {
        get => (Scene) _sceneField.GetValue(target);
        set => _sceneField.SetValue(target, value);
    }

    private void OnEnable()
    {
        var type = typeof(SceneManagerButton);
        
        _idField = type.GetField("_id", BindingFlags.Instance | BindingFlags.NonPublic);
        _sceneField = type.GetField("_scene", BindingFlags.Instance | BindingFlags.NonPublic);
    }
    
    public override void OnInspectorGUI()
    {
        Scene = (Scene) EditorGUILayout.EnumPopup(nameof(Scene), Scene);

        if (Scene == Scene.Load || Scene == Scene.LoadAsync)
            ID = EditorGUILayout.IntField(nameof(ID), ID);
    }
}