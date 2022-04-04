using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Damageable
{
    private Transform target;
    [SerializeField] private float range = 15f;
    [SerializeField] private float speedYaw = 60.0f;
    [SerializeField] private float speedPitch = 5.0f;
    [SerializeField] private float _shootAngle = 0.8f;
    [SerializeField] private Transform _turretHelper;
    [SerializeField] private TurretWeaponController _turretWeapon;
    [SerializeField] private DrawCircle _drawRangeCircle;
    [SerializeField] private string TurretTargetString;

    public Transform yawBase;
    public Transform pitchBase;


	void Awake()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        _drawRangeCircle.RadiousToDraw = range;
    }

    void Update()
    {
        if (target == null) return;
        _turretHelper.LookAt(target);

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;

        yawBase.rotation = Quaternion.RotateTowards(yawBase.rotation, Quaternion.Euler(0f, rotation.y, 0f), speedYaw * Time.deltaTime);
        pitchBase.localRotation = Quaternion.RotateTowards(pitchBase.localRotation, Quaternion.Euler(_turretHelper.localRotation.eulerAngles.x, pitchBase.localRotation.y, pitchBase.localRotation.z), speedPitch * Time.deltaTime);

        if (TargetVisible())
        {
            _turretWeapon.Fire();
        }
    }

    void UpdateTarget()
	{
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestUnit = null;

		foreach (GameObject unit in units)
		{
            float distanceToUnit = Vector3.Distance(transform.position, unit.transform.position);
			if (distanceToUnit < shortestDistance)
			{
                shortestDistance = distanceToUnit;
                nearestUnit = unit;
			}
		}

		if (nearestUnit != null && shortestDistance <= range)
		{
            target = nearestUnit.gameObject.transform.Find(TurretTargetString).transform;
		}
		else
		{
            target = null;
		}
	}

    public bool TargetVisible()
	{
        Vector3 dirToTarget = Vector3.Normalize(pitchBase.position - target.position);
        var Dot = Vector3.Dot(pitchBase.forward, dirToTarget) * -1;

		if (Dot >= _shootAngle)
		{
            return true;
		}
        else return false;
	}

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
