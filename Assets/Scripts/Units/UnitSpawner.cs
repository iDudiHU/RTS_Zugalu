using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that spawns units
public class UnitSpawner : MonoBehaviour
{
    [Tooltip("List of spawnable units")]
    [SerializeField]
    private List<SelectableUnit> _spawnableUnits;
    [Tooltip("Transform location on where the unit appears")]
    [SerializeField]
    private Transform _spawnLocationTransform;
    [Tooltip("Max number of units to spawn")]
    [SerializeField]
    private int _numberOfUnitsToSpawn = 10;
    [Tooltip("Current number of units")]
    [SerializeField]
    private int _CurrentNumberOfUnits = 0;
    [Tooltip("Time between unit spawn")]
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
        if (_spawnableUnits != null || _spawnLocationTransform != null)
            Instantiate(_spawnableUnits[index].gameObject, _spawnLocationTransform.position, _spawnLocationTransform.rotation);
    }

    public void UnitDestroyed()
	{
        _CurrentNumberOfUnits--;
    }
}
