using System.Collections;

using UnityEngine;
using UnityEngine.UI;
public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
        _quitButton.onClick.AddListener(() =>
        {
            print("Quit Button Clicked");
            Application.Quit();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
