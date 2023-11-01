using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private List<Tail> _tails;
    [SerializeField] private Tail _tailPrefab;
    [Min(0f)][SerializeField] protected float _timeRate;
    [Min(0.05f)][SerializeField] private float _directionDelay; 
    public float TimeRate
    {
        get { return _timeRate; }
        set
        {
            if (value > 0)
            {
                _timeRate = value;
            }
        }
    }
    public float DirectionDelay
    {
        get { return _directionDelay; }
        set
        {
            if (value >= 0.05)
            {
                _directionDelay = value;
            }
        }
    }

    public Action<Vector3, Quaternion> OnMoved;
    public Action OnEaten;
    public Action OnDead;

    private Vector3 _offset;
    private Vector3 _direction;

    private enum Direction { Right, Left, Up, Down}
    private Direction _currentDirection;
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _offset = spriteRenderer.bounds.size;

        _direction = Vector3.right * _offset.x;

        StartCoroutine(Move());
        StartCoroutine(SetDirection());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddTail();
        }
    }
    public void Dead()
    {
        OnDead?.Invoke();
        AudioManager.Instance.PlaySound("Death");
        Destroy(transform.parent.gameObject);
    }
    private IEnumerator Move()
    {
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation;
        transform.position += GetNextPosition(_currentDirection);

        OnMoved?.Invoke(previousPosition, previousRotation);
        yield return new WaitForSeconds(TimeRate);
        StartCoroutine(Move());
    }
    public void AddTail()
    {
        Tail tail = Instantiate(_tailPrefab, new Vector3(100, 100, 0),Quaternion.identity);
        tail.transform.SetParent(transform.parent, false);
        tail.SetPreviousTail(_tails[^1]);
        _tails.Add(tail);
    }
    private Vector3 GetNextPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                return Vector3.right * _offset.x;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                return Vector3.left * _offset.x;
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                return Vector3.up * _offset.y;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                return Vector3.down * _offset.y;
            default:
                throw new ArgumentException("Trouble with direction");
        }
    }
    private Direction GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0)
        {
            if (horizontal == 1)
            {
                if (_currentDirection != Direction.Left)
                    return Direction.Right;
            }
            else
            {
                if (_currentDirection != Direction.Right)
                    return Direction.Left;
            }
        }
        else if (vertical != 0)
        {
            if (vertical == 1)
            {
                if (_currentDirection != Direction.Down)
                    return Direction.Up;
            }
            else
            {
                if (_currentDirection != Direction.Up)
                    return Direction.Down;
            }
        }
        return _currentDirection;
    }
    private IEnumerator SetDirection()
    {
        _currentDirection = GetDirection();
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(SetDirection());
    }
}
