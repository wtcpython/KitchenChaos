using System.Collections;

using TMPro;

using UnityEngine;
public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dotText;
    private float _dotTimer = 0.3f;
    IEnumerator DotAnimation()
    {
        while (true)
        {
            _dotText.text = ".";
            yield return new WaitForSeconds(_dotTimer);
            _dotText.text = "..";
            yield return new WaitForSeconds(_dotTimer);
            _dotText.text = "...";
            yield return new WaitForSeconds(_dotTimer);
            _dotText.text = "....";
            yield return new WaitForSeconds(_dotTimer);
            _dotText.text = ".....";
            yield return new WaitForSeconds(_dotTimer);
            _dotText.text = "......";
            yield return new WaitForSeconds(_dotTimer);

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _ = StartCoroutine(DotAnimation());
    }
}
