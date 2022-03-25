using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObstaclesFabric))]
public class ObstaclesMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    private ObstaclesFabric _fabric;

    private bool _isPaused => PauseManager.Instance.IsPaused;

    private void Awake()
    {
        _fabric = GetComponent<ObstaclesFabric>();
    }

    private void FixedUpdate()
    {
        if (_fabric.Obstacles.Count == 0 || _isPaused)
            return;

        foreach (var obstacle in _fabric.Obstacles.Where(obstacle => obstacle != null))
        {
            obstacle.transform.position -= Vector3.right * Speed;
        }
    }
}