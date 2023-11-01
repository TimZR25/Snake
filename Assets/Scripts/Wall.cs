using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private PlayerBehavior _playerBehavior;
    private void Awake()
    {
        _playerBehavior = FindObjectOfType<PlayerBehavior>();
    }
    private void CheckPlayerPosition(Vector3 position, Quaternion rotation)
    {
        if (Vector3.Distance(_playerBehavior.transform.position, transform.position) == 0)
        {
            _playerBehavior.Dead();
        }
    }
    private void OnEnable()
    {
        _playerBehavior.OnMoved += CheckPlayerPosition;
    }
    private void OnDisable()
    {
        _playerBehavior.OnMoved -= CheckPlayerPosition;
    }
}
