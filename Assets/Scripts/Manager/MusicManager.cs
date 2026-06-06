using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private const string MUSICMANAGER_VOLUME_KEY = "MusicManagerVolume";
    private AudioSource _audioSource;
    private float _originalVolume;
    private int _volume = 5;

    private void Awake()
    {
        Instance = this;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _originalVolume = _audioSource.volume;

        LoadVolume();
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if (_volume == 0)
        {
            _audioSource.enabled = false;
        }
        else
        {
            _audioSource.enabled = true;
            _audioSource.volume = _originalVolume * _volume / 10f;
        }
    }

    public void ChangeVolume()
    {
        _volume++;
        if (_volume > 10)
        {
            _volume = 0;
        }
        UpdateVolume();
        SaveVolume();
    }

    public int GetVolume()
    {
        return _volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetInt(MUSICMANAGER_VOLUME_KEY, _volume);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MUSICMANAGER_VOLUME_KEY))
        {
            _volume = PlayerPrefs.GetInt(MUSICMANAGER_VOLUME_KEY, _volume);
        }
    }
}
