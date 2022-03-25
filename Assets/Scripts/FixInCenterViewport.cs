using UnityEngine;

public class FixInCenterViewport : MonoBehaviour
{
    [SerializeField] private bool X;
    [SerializeField] private bool Y;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        var center = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        
        transform.position = new Vector3
        {
            x = X ? center.x : transform.position.x,
            y = Y ? center.y : transform.position.y,
            z = transform.position.z
        };
    }
}