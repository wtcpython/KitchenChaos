using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player _player;
    private float _stepSoundRate = .15f;
    private float _stepSoundTimer = 0;

    void Start()
    {
        _player = GetComponent<Player>();
    }
    void Update()
    {

        _stepSoundTimer += Time.deltaTime;
        if (_stepSoundTimer >= _stepSoundRate)
        {
            _stepSoundTimer = 0;
            if (_player.IsWalking)
            {
                float volume = .1f;
                SoundManager.Instance.PlayStepSound(volume);
            }
        }
    }
}
