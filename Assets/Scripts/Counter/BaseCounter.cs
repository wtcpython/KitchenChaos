using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject _selectedCounter;

    public virtual void Interact(Player player)
    {
        Debug.LogWarning("BaseCounter Interact was called! This should be overridden in the child class!");
    }

    public virtual void Operate(Player player)
    {

    }

    public void SelectCounter()
    {
        _selectedCounter.SetActive(true);
    }

    public void DeselectCounter()
    {
        _selectedCounter.SetActive(false);
    }
}
