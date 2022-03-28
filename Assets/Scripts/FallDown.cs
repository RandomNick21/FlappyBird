using UnityEngine;

public class FallDown : MonoBehaviour
{
    [SerializeField] private float SpeedFall;
    private Rigidbody2D _rigidbody;

    private bool IsPaused => PauseManager.Instance.IsPaused;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(IsPaused)
            return;
        
        Fall();
    }

    private void Fall()
    {
        _rigidbody.velocity = Vector2.down * SpeedFall;
    }
}
