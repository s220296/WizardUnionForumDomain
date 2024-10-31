namespace WizardUnion.Places;

public class Place
{
    public string Name { get; protected set; }
    public double CyclesPerEon { get; protected set; }
    public Place? Parent { get; protected set; }

    // Main Constructor
    public Place(double _cyclesPerEon, string _name, Place? _parent = null)
    {
        _cyclesPerEon = Math.Clamp(_cyclesPerEon, 0, double.MaxValue);

        (CyclesPerEon, Name, Parent) =
        (_cyclesPerEon, _name, _parent);
    }

    public Place? SetChildOf(Place? _parent) { Parent = _parent; return Parent; }
    public bool IsChildOf(Place _parent)
    {
        Place? iterator = Parent;
        while (iterator != null)
        {
            if (iterator == _parent) return true;
            iterator = iterator.Parent;
        }

        return false;
    }

    public double CyclesSinceCycle(double _cycle) => AgeInCycles() - _cycle;

    public double AgeInCycles() => Universe.CurrentEons * CyclesPerEon;

    public double CyclesPerCycleOf(Place _other) => CyclesPerEon / _other.CyclesPerEon;

    public override string ToString()
    {
        return Name;
    }
}