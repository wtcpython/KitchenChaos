using System;

using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _counterLayerMask;

    public bool IsWalking { get; private set; } = false;
    private BaseCounter _selectedCounter;
    public BaseCounter SelectedCounter
    {
        private get => _selectedCounter;
        set
        {
            if (value != _selectedCounter)
            {
                _selectedCounter?.DeselectCounter();
                value?.SelectCounter();
                _selectedCounter = value;
            }
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnOperateAction += GameInput_OnOperateAction;
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        SelectedCounter?.Interact(this);
    }

    private void GameInput_OnOperateAction(object sender, EventArgs e)
    {
        SelectedCounter?.Operate(this);
    }

    private void HandleMovement()
    {
        Vector3 direction = _gameInput.GetMovementDirectionNormalized();

        IsWalking = direction != Vector3.zero;

        transform.position += _moveSpeed * Time.fixedDeltaTime * direction;

        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.fixedDeltaTime * _rotateSpeed);
        }
    }

    private void HandleInteraction()
    {
        Vector3 raycastOrigin = transform.position + (Vector3.up * 0.5f);

        SelectedCounter = Physics.Raycast(raycastOrigin, transform.forward, out RaycastHit hitInfo, 2f, _counterLayerMask)
            ? hitInfo.transform.TryGetComponent(out BaseCounter baseCounter) ? baseCounter : null
            : null;
    }
}
