using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject _stoveOnVisual;
    [SerializeField] private GameObject _sizzlingPaticles;

    public void ShowStoveEffect()
    {
        _stoveOnVisual.SetActive(true);
        _sizzlingPaticles.SetActive(true);
    }

    public void HideStoveEffect()
    {
        _stoveOnVisual.SetActive(false);
        _sizzlingPaticles.SetActive(false);
    }
}
