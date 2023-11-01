using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private Wall _wallPrefab;
    [SerializeField] private float _limitX;
    [SerializeField] private float _limitY;
    private List<Wall> _walls = new List<Wall>();

    private void Start()
    {
        for (float i = -_limitX; i <= _limitX; i += 0.5f)
        {
            SpawnWall(i, _limitY);
            SpawnWall(i, -_limitY);
        }
        for (float i = -_limitY; i <= _limitY; i += 0.5f)
        {
            SpawnWall(-_limitX, i);
            SpawnWall(_limitX, i);
        }
    }
    private void SpawnWall(float x, float y)
    {
        Wall wall = Instantiate(_wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
        wall.transform.SetParent(transform);
    }
}
