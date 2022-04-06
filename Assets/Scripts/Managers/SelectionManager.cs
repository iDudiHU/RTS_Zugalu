using System.Collections.Generic;
//Class that handles selection of units
public class SelectionManager
{
    private static SelectionManager _instance;
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SelectionManager();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    //hashset used to make sure to not select the same unit twice
    public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
    public List<SelectableUnit> AvailableUnits = new List<SelectableUnit>();

    //create Selection manager withouth attached to a gameobject
    private SelectionManager() { }

    public void Select(SelectableUnit Unit)
    {
        SelectedUnits.Add(Unit);
        Unit.OnSelected();
    }

    public void Deselect(SelectableUnit Unit)
    {
        Unit.OnDeselected();
        SelectedUnits.Remove(Unit);
    }

    public void DeselectAll()
    {
        foreach (SelectableUnit unit in SelectedUnits)
        {
            unit.OnDeselected();
        }
        SelectedUnits.Clear();
    }

    public bool IsSelected(SelectableUnit Unit)
    {
        return SelectedUnits.Contains(Unit);
    }

    public void RemoveDestroyedUnit(SelectableUnit Unit)
	{
        SelectedUnits.Remove(Unit);
        AvailableUnits.Remove(Unit);

    }
}