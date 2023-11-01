using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Quaternion _previousRotation;
    private PlayerBehavior _playerBehavior;
    private Tail _previousTail;
    public Action<Vector3, Quaternion> OnMoved;
    private void Awake()
    {
        _playerBehavior = FindObjectOfType<PlayerBehavior>().GetComponent<PlayerBehavior>();
    }
    private void Move(Vector3 previousPosition, Quaternion previousRotation)
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;

        transform.position = previousPosition;
        transform.rotation = previousRotation;

        OnMoved?.Invoke(_previousPosition, _previousRotation);
    }
    public void SetPreviousTail(Tail previousTail)
    {
        _previousTail = previousTail;
        _playerBehavior.OnMoved -= Move;
        _previousTail.OnMoved += Move;
    }
    private void OnEnable()
    {
        if (_previousTail != null)
            return;

        _playerBehavior.OnMoved += Move;
        _playerBehavior.OnMoved += CheckPlayerPosition;
    }
    private void OnDisable()
    {
        if (_previousTail != null)
        {
            _previousTail.OnMoved -= Move;
            return;
        }
        _playerBehavior.OnMoved -= Move;
        _playerBehavior.OnMoved -= CheckPlayerPosition;
    }
    private void CheckPlayerPosition(Vector3 position, Quaternion rotation)
    {
        if (Vector3.Distance(_playerBehavior.transform.position, transform.position) == 0)
        {
            _playerBehavior.Dead();
        }
    }
}
