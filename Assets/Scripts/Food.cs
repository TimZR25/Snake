using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private GameObject _vanishParticle;
    private PlayerBehavior _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerBehavior>();
    }
    private void CheckPlayerPosition(Vector3 position, Quaternion rotation)
    {
        if (Vector3.Distance(_player.transform.position, transform.position) == 0)
        {
            EatFood();
        }
    }
    private void OnEnable()
    {
        _player.OnMoved += CheckPlayerPosition;
    }
    private void OnDisable()
    {
        _player.OnMoved -= CheckPlayerPosition;
    }
    public void EatFood()
    {
        GameObject vanishPS = Instantiate(_vanishParticle, transform.position, Quaternion.identity);
        Destroy(vanishPS, 2f);

        GameManager.Instance.Score += 1;
        _player.OnEaten?.Invoke();

        AudioManager.Instance.PlaySound("Pop");

        _player.AddTail();

        Destroy(gameObject);
    }
}
