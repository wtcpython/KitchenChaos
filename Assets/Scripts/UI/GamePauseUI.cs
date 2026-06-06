using System;

using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiParent;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _settingsButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        _resumeButton.onClick.AddListener(() => GameManager.Instance.ToggleGame());
        _menuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameMenuScene));
        _settingsButton.onClick.AddListener(() => SettingsUI.Instance.Show());
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }
    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }
    private void Show()
    {
        _uiParent.SetActive(true);
    }

    private void Hide()
    {
        _uiParent.SetActive(false);
    }
}
