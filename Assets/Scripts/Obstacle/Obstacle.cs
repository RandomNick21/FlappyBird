using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour, IPauseHandler
{
    [SerializeField] private SpriteRenderer Renderer;
    [Space]
    [SerializeField] private float MinSizeY = 3f;
    [SerializeField] private float MaxSizeY = 5f;
    [Space]
    [SerializeField] private float SpeedGettingIntoPlace = 1f;
    
    private Camera _camera;
    private bool _isUpAnchor;
    private Coroutine _moveCoroutine;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void OnEnable()
    {
        PauseManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        PauseManager.Instance.UnRegister(this);
    }

    private void Start()
    {
        SetSize();
        
        _moveCoroutine = StartCoroutine(MoveToBoard());
        
        _isUpAnchor = Random.Range(0, 2) != 0;
        Renderer.flipY = !_isUpAnchor;
    }

    private void SetSize()
    {
        var randomSizeY = Random.Range(MinSizeY, MaxSizeY);
        
        Renderer.size = new Vector2(Renderer.size.x, randomSizeY);
        Renderer.GetComponent<BoxCollider2D>().size = Renderer.size;
    }

    private void Update()
    {
        if (!_camera.IsObjectVisible(Renderer))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveToBoard()
    {
        var upCameraPoint = _camera.ViewportToWorldPoint(Vector3.up).y;
        var offsetY = Renderer.size.y / 2;
        
        for (var t = 0f; t < 1; t += Time.deltaTime * SpeedGettingIntoPlace)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, 
                _isUpAnchor ? (upCameraPoint + -offsetY) : (-upCameraPoint + offsetY), transform.position.z), t);

            yield return null;
        }
    }

    public void SetPaused(bool isPaused)
    {
        if(_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }
}
