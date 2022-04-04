using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<SelectableUnit> _spawnableUnits;
    [SerializeField]
    private Transform _spawnTransform;
    [SerializeField]
    private int _numberOfUnitsToSpawn = 10;
    [SerializeField]
    private int _CurrentNumberOfUnits = 0;
    [SerializeField]
    private float _spawnDealy = 1f;
    
    void Start()
    {
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        WaitForSeconds Wait = new WaitForSeconds(_spawnDealy);

        while (_CurrentNumberOfUnits < _numberOfUnitsToSpawn)
        {
            SpawnRandomUnit();
            _CurrentNumberOfUnits++;

            yield return Wait;
        }
    }

    private void SpawnRandomUnit()
    {
        Spawn(Random.Range(0, _spawnableUnits.Count));
    }


    void Spawn(int index)
    {
        if (_spawnableUnits != null || _spawnTransform != null)
            Instantiate(_spawnableUnits[index].gameObject, _spawnTransform.position, _spawnTransform.rotation);
    }

    public void UnitDestroyed()
	{
        _CurrentNumberOfUnits--;
    }
}
