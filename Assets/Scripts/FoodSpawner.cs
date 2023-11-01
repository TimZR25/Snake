using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Food _foodPrefab;
    private List<float> _coordinatesX = new List<float>();
    private List<float> _coordinatesY = new List<float>();
    private PlayerBehavior _playerBehavior;

    private void Awake()
    {
        _playerBehavior = FindObjectOfType<PlayerBehavior>();
    }

    private void Start()
    {
        for (int i = -33; i <= 33; i +=2)
        {
            _coordinatesX.Add(i * 0.25f);
        }
        for (int i = -17; i <= 17; i += 2)
        {
            _coordinatesY.Add(i * 0.25f);
        }
        SpawnFood();
    }

    private void SpawnFood()
    {
        float x = _coordinatesX[Random.Range(0, _coordinatesX.Count)];
        float y = _coordinatesY[Random.Range(0, _coordinatesY.Count)];
        Vector3 spawnPosition = new Vector3(x, y, 0);
        Food food = Instantiate(_foodPrefab, spawnPosition, Quaternion.identity);
        food.transform.SetParent(transform);
    }

    private void OnEnable()
    {
        _playerBehavior.OnEaten += SpawnFood;
    }

    private void OnDisable()
    {
        _playerBehavior.OnEaten -= SpawnFood;
    }
}
