using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that handles the turret
public class Turret : Damageable
{
    private Transform target;
    [Tooltip("Turret ange")]
    [SerializeField] private float _range = 15f;
    [Tooltip("Yaw rotation speed")]
    [SerializeField] private float _speedYaw = 60.0f;
    [Tooltip("Pitch rotation speed")]
    [SerializeField] private float _speedPitch = 5.0f;
    [Tooltip("Angle to be able to shoot (Dot product)")]
    [SerializeField] private float _shootAngle = 0.8f;
    [Tooltip("Turret helper gameobject to point at the target")]
    [SerializeField] private Transform _turretHelper;
    [Tooltip("Weapon script in the prefab")]
    [SerializeField] private TurretWeaponController _turretWeapon;
    [Tooltip("Debug circle script to show range in game/ would use a shadergraph shader instead that is dynamic with the terrain")]
    [SerializeField] private DrawCircle _drawRangeCircle;
    [Tooltip("Target aim gameobjects name")]
    [SerializeField] private string _turretTargetString;

    public Transform yawBase;
    public Transform pitchBase;



	void Awake()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        _drawRangeCircle.RadiousToDraw = _range;
    }

    void Update()
    {
        if (target == null) return;
        //Helps with the dual axis rotation of the turret
        _turretHelper.LookAt(target);

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;

        yawBase.rotation = Quaternion.RotateTowards(yawBase.rotation, Quaternion.Euler(0f, rotation.y, 0f), _speedYaw * Time.deltaTime);
        pitchBase.localRotation = Quaternion.RotateTowards(pitchBase.localRotation, Quaternion.Euler(_turretHelper.localRotation.eulerAngles.x, pitchBase.localRotation.y, pitchBase.localRotation.z), _speedPitch * Time.deltaTime);

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

		if (nearestUnit != null && shortestDistance <= _range)
		{
            target = nearestUnit.gameObject.transform.Find(_turretTargetString).transform;
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
        Gizmos.DrawWireSphere(transform.position, _range);
	}
}
