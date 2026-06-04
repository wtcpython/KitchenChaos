using System.Collections.Generic;

using UnityEngine;

public class PlantsCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _plateSO;
    [SerializeField] private float _spawnRate = 3;
    [SerializeField] private int _plateCountMax = 5;

    private List<KitchenObject> _platesList = new List<KitchenObject>();

    private float _timer = 0;

    private void Update()
    {
        if (_platesList.Count < _plateCountMax)
        {
            _timer += Time.deltaTime;
        }

        if (_timer > _spawnRate)
        {
            _timer = 0;
            SpawnPlate();
        }
    }

    public override void Interact(Player player)
    {

        if (_platesList.Count > 0)
        {
            player.AddKitchenObject(_platesList[^1]);
            _platesList.RemoveAt(_platesList.Count - 1);
        }
    }

    public void SpawnPlate()
    {
        if (_platesList.Count >= _plateCountMax)
        {
            _timer = 0;
            return;
        }
        GameObject obj = GameObject.Instantiate(_plateSO.Prefab, HoldPoint);
        KitchenObject kitchenObject = obj.GetComponent<KitchenObject>();
        KitchenObject = kitchenObject;

        kitchenObject.transform.localPosition = Vector3.zero + (Vector3.up * 0.1f * _platesList.Count);
        _platesList.Add(kitchenObject);

    }
}
