using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObstaclesFactory))]
public class ObstaclesMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    private ObstaclesFactory _factory;

    private bool _isPaused => PauseManager.Instance.IsPaused;

    private void Awake()
    {
        _factory = GetComponent<ObstaclesFactory>();
    }

    private void FixedUpdate()
    {
        if (_factory.Obstacles.Count == 0 || _isPaused)
            return;

        foreach (var obstacle in _factory.Obstacles.Where(obstacle => obstacle != null))
        {
            obstacle.transform.position -= Vector3.right * Speed;
        }
    }
}