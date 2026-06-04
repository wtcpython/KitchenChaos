using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayOpen()
    {
        _animator.SetTrigger("OpenClose");
    }
}
