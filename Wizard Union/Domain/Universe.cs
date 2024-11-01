using WizardUnion.Places;

namespace WizardUnion;

public static class Universe
{
    public static Place Place;

    public static double CurrentEons { get; private set; }

    static Universe()
    {
        Place = new Place(1d, "The Universe");
        Place.RemoveParent();
    }

    public static void Begin(double _beginningEon = 0d)
    {
        _beginningEon = Math.Clamp(_beginningEon, 0, double.MaxValue);

        CurrentEons = _beginningEon;
    }
}
