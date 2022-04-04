using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : Damageable
{
    private NavMeshAgent Agent;
    [SerializeField]
    private Transform highlite;

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 Position)
    {
        Agent.SetDestination(Position);
    }

    public void OnSelected()
    {
        highlite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        highlite.gameObject.SetActive(false);
    }

	public void OnDestroy()
	{
        SelectionManager.Instance.RemoveDestroyedUnit(this);
    }
}