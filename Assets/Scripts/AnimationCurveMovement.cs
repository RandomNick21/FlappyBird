using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimationCurveMovement : MonoBehaviour, IPauseHandler
{
    [SerializeField] private AnimationCurve FlyCurve;
    [SerializeField] private float Height = 1f;
    [SerializeField] private float Speed = 1f;
    
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    
    private Rigidbody2D _rigidbody;
    private RigidbodyConstraints2D _startConstraints;
    private Coroutine _flyCoroutine;
    private Camera _camera;

    private bool IsPaused => PauseManager.Instance.IsPaused;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        PauseManager.Instance.Register(this);
    }

    private void Update()
    {
        if(IsPaused)
            return;
        
        if (Input.GetKeyDown(KeyCode.A))
            _flyCoroutine = StartCoroutine(Move());
        
        if(!_camera.IsObjectVisible(gameObject))
            GlobalGameStateMachine.Instance.SetState<GameOverState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GlobalGameStateMachine.Instance.SetState<GameOverState>();
    }

    private IEnumerator Move()
    {
        var startPositionY = transform.position.y;
        
        for (var t = 0f; t < 1; t += Time.deltaTime * Speed)
        {
            transform.position = new Vector3(transform.position.x, 
                startPositionY + FlyCurve.Evaluate(t) * Height, transform.position.z);

            yield return _waitForFixedUpdate;
        }
    }

    public void SetPaused(bool isPaused)
    {
        StopCoroutine(_flyCoroutine);
        FreezePhysic(isPaused);
    }

    private void FreezePhysic(bool isPaused)
    {
        if (isPaused)
        {
            _startConstraints = _rigidbody.constraints;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = _startConstraints;
        }
    }
}
