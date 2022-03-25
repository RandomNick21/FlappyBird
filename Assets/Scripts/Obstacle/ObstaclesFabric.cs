using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesFabric : MonoBehaviour, IPauseHandler
{
    [HideInInspector] public List<Transform> Obstacles;
    
    [SerializeField] private float SpawnRate = 1f;
    [SerializeField] private GameObject PrefabObstacle;
    
    private Camera _camera;
    private Coroutine _spawnCoroutine;
    
    private bool IsPaused => PauseManager.Instance.IsPaused;

    private void Awake()
    {
        _camera = Camera.main;
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnEnable()
    {
        PauseManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        PauseManager.Instance.UnRegister(this);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (IsPaused)
                yield return new WaitForSeconds(1 / SpawnRate);
            
            var rightBorder = _camera.ViewportToWorldPoint(Vector3.right).x;

            var obstacle = Instantiate(PrefabObstacle, Vector3.right * rightBorder,
                Quaternion.identity, transform).transform;

            Obstacles.Add(obstacle);

            yield return new WaitForSeconds(1 / SpawnRate);
        }
    }

    private void LateUpdate()
    {
        Obstacles = Obstacles.Where(x => x != null).ToList();
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
            StopCoroutine(_spawnCoroutine);
        else
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }
}
