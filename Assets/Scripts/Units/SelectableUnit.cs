using UnityEngine;
using UnityEngine.AI;

//Base selectable unit class
[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : Damageable
{
    private NavMeshAgent _agent;
    [Tooltip("Transform of the highlite gamobject")]
    [SerializeField]
    private Transform _highlite;

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        _agent = GetComponent<NavMeshAgent>();
    }

	public void OnEnable()
	{
		_agent.SetDestination(transform.position + transform.forward * 5);
        
    }

	public void MoveTo(Vector3 Position)
    {
        _agent.SetDestination(Position);
    }

    public void OnSelected()
    {
        _highlite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        _highlite.gameObject.SetActive(false);
    }

	public void OnDestroy()
	{
        SelectionManager.Instance.RemoveDestroyedUnit(this);
    }
}