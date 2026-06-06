using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    [SerializeField] private Player _player;
    private State _state;

    private float _waitingToStartTimer = 1;
    private float _countDownToStartTimer = 3;
    private float _gamePlayingTimer = 20;

    private bool _isGamePause = false;

    void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        TurnToWaitingToStart();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }
    void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer <= 0)
                {
                    TurnToCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                _countDownToStartTimer -= Time.deltaTime;
                if (_countDownToStartTimer <= 0)
                {
                    TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer <= 0)
                {
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }
    private void TurnToWaitingToStart()
    {
        _state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToCountDownToStart()
    {
        _state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGamePlaying()
    {
        _state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGameOver()
    {
        _state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DisablePlayer()
    {
        _player.enabled = false;
    }
    private void EnablePlayer()
    {
        _player.enabled = true;
    }
    public bool IsCountDownState()
    {
        return _state == State.CountDownToStart;
    }
    public bool IsGamePlayingState()
    {
        return _state == State.GamePlaying;
    }

    public bool IsGameOverState()
    {

        return _state == State.GameOver;
    }
    public float GetCountDownTimer()
    {
        return _countDownToStartTimer;
    }

    public void ToggleGame()
    {
        _isGamePause = !_isGamePause;
        if (_isGamePause)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
