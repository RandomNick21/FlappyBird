using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FrameByFrameAnimation : MonoBehaviour, IPauseHandler
{
    [SerializeField] private Sprite[] Frames;
    [SerializeField] private float ShiftTime = 0.5f;
    
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _waitForSeconds;
    private Coroutine _startAnimationCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _waitForSeconds = new WaitForSeconds(ShiftTime);
    }

    private void Start()
    {
        _startAnimationCoroutine = StartCoroutine(StartAnimation());
    }

    private void OnEnable()
    {
        PauseManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        PauseManager.Instance.UnRegister(this);
    }

    private IEnumerator StartAnimation()
    {
        while (true)
        {
            foreach (var frame in Frames)
            {
                _spriteRenderer.sprite = frame;
                yield return _waitForSeconds;
            }
        }
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
            StopCoroutine(_startAnimationCoroutine);
        else
            _startAnimationCoroutine = StartCoroutine(StartAnimation());
    }
}
