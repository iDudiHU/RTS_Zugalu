using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionInput : MonoBehaviour
{
    [Tooltip("Main Camera")]
    [SerializeField]
    private Camera Camera;
    [Tooltip("Selection Box rect on selection canvas")]
    [SerializeField]
    private RectTransform SelectionBox;
    [Tooltip("Selectable unit layers")]
    [SerializeField]
    private LayerMask UnitLayers;
    [Tooltip("Floor layers")]
    [SerializeField]
    private LayerMask FloorLayers;
    [Tooltip("Time to recognise it is a drag")]
    [SerializeField]
    private float DragDelay = 0.1f;

    private RTSControl _inputActions;

    private float MouseDownTime;
    private Vector2 StartMousePosition;

    private HashSet<SelectableUnit> newlySelectedUnits = new HashSet<SelectableUnit>();
    private HashSet<SelectableUnit> deselectedUnits = new HashSet<SelectableUnit>();

	private void Awake()
	{
        _inputActions = new RTSControl();
    }
    private void OnEnable()
    {
        _inputActions.GameplayActionMap.Enable();
    }

    private void OnDisable()
    {
        _inputActions.GameplayActionMap.Disable();
    }

    private void Update()
	{
		HandleSelectionInputs();
		HandleMovementInputs();
	}

	private void HandleMovementInputs()
    {
        if (Mouse.current.rightButton.wasReleasedThisFrame && SelectionManager.Instance.SelectedUnits.Count > 0)
        {
            //If clicked on a layer where unit can move to
            if (Physics.Raycast(Camera.ScreenPointToRay(_inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>()), out RaycastHit Hit, 5000000f, FloorLayers))
            {
                foreach (SelectableUnit unit in SelectionManager.Instance.SelectedUnits)
                {
                    unit.MoveTo(Hit.point);
                }
            }
        }
    }

    private void HandleSelectionInputs()
    {   //Normal select
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = _inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>();
            MouseDownTime = Time.time;
        }
        //Drag Select
        else if (Mouse.current.leftButton.isPressed && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            foreach (SelectableUnit newUnit in newlySelectedUnits)
            {
				if (newUnit != null)
				{
                    SelectionManager.Instance.Select(newUnit);
				}
                
            }
            foreach (SelectableUnit deselectedUnit in deselectedUnits)
            {
                if (deselectedUnit != null)
                {
                    SelectionManager.Instance.Deselect(deselectedUnit);
                }
                
            }

            newlySelectedUnits.Clear();
            deselectedUnits.Clear();
            //Shift Select
            if (Physics.Raycast(Camera.ScreenPointToRay(_inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>()), out RaycastHit hit, 5000000f, UnitLayers)
                && hit.collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {
                if (_inputActions.GameplayActionMap.SelectModifier.IsPressed() && unit != null)
                {
                    if (SelectionManager.Instance.IsSelected(unit))
                    {
                        SelectionManager.Instance.Deselect(unit);
                    }
                    else
                    {
                        SelectionManager.Instance.Select(unit);
                    }
                }
                else
                {
                    SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                SelectionManager.Instance.DeselectAll();
            }

            MouseDownTime = 0;
        }
    }

    private void ResizeSelectionBox()
    {
        //Vector magic to resize selection box
        float width = _inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>().x - StartMousePosition.x;
        float height = _inputActions.GameplayActionMap.MousePosition.ReadValue<Vector2>().y - StartMousePosition.y;

        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, height / 2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < SelectionManager.Instance.AvailableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(SelectionManager.Instance.AvailableUnits[i].transform.position), bounds))
            {
                if (!SelectionManager.Instance.IsSelected(SelectionManager.Instance.AvailableUnits[i]))
                {
                    newlySelectedUnits.Add(SelectionManager.Instance.AvailableUnits[i]);
                }
                deselectedUnits.Remove(SelectionManager.Instance.AvailableUnits[i]);
            }
            else
            {
                deselectedUnits.Add(SelectionManager.Instance.AvailableUnits[i]);
                newlySelectedUnits.Remove(SelectionManager.Instance.AvailableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x < Bounds.max.x
            && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}