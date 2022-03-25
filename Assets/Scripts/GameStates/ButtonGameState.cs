using UnityEngine;
using UnityEngine.UI;

public class ButtonGameState : MonoBehaviour
{
    [SerializeField] private GameState GameStateTo = GameState.Pause;
    [SerializeField] private GameState GameStateStart = GameState.Game;
    private Button _button;
    private bool _score;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetState);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SetState);
    }
    
    private void SetState()
    {
        _score = !_score;

        GlobalGameStateMachine.Instance.SetState(_score ? GameStateTo : GameStateStart);
    }
}
