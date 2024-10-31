using WizardUnion.Places;

namespace WizardUnion.Birth;

public class BirthDetails
{
    public Place PlaceOfBirth { get; protected set; }
    public double CycleOfBirth { get; protected set; }

    public double CurrentAgeInEons => PlaceOfBirth.CyclesSinceCycle(CycleOfBirth) / PlaceOfBirth.CyclesPerEon;

    public BirthDetails(Place _placeOfBirth, double _cycleOfBirth)
    {
        // Validation
        _cycleOfBirth = Math.Clamp(_cycleOfBirth, 0, _placeOfBirth.AgeInCycles());

        (PlaceOfBirth, CycleOfBirth) = (_placeOfBirth, _cycleOfBirth);
    }

    //public int Age(IAgeFormatter _formatter);

    public override string ToString()
    {
        return $"Born on {PlaceOfBirth} during cycle {Math.Truncate(CycleOfBirth)} of {PlaceOfBirth.AgeInCycles()}. ";
    }
}
