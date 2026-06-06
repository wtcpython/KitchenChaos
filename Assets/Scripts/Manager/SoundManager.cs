using System;

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private const string SOUNDMANAGER_VOLUME_KEY = "SoundManagerVolume";
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private int _volume = 5;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadVolume();
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSucceeded;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickUp += KitchenObjectHolder_OnPickUp;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }
    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.Trash);
    }
    private void KitchenObjectHolder_OnPickUp(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.ObjectPickUp);
    }
    private void KitchenObjectHolder_OnDrop(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSO.ObjectDrop);
    }
    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.Chop);
    }

    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.DeliveryFail);
    }

    private void OrderManager_OnRecipeSucceeded(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.DeliverySuccess);
    }
    public void PlayStepSound(float volume = .1f)
    {
        PlaySound(_audioClipRefsSO.Footstep, Camera.main.transform.position, volume);
    }
    private void PlaySound(AudioClip[] clips)
    {
        PlaySound(clips, Camera.main.transform.position);
    }
    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = .1f)
    {
        if (_volume == 0)
        {
            return;
        }
        int index = UnityEngine.Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volume * (_volume / 10f));
    }

    public void ChangeVolume()
    {
        _volume++;
        if (_volume > 10)
        {
            _volume = 0;
        }
        SaveVolume();
    }

    public int GetVolume()
    {
        return _volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetInt(SOUNDMANAGER_VOLUME_KEY, _volume);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(SOUNDMANAGER_VOLUME_KEY))
        {
            _volume = PlayerPrefs.GetInt(SOUNDMANAGER_VOLUME_KEY, _volume);
        }
    }
}
