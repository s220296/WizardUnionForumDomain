namespace WizardUnion.Places;

public static class PlaceSystemExtensions
{
    public static Place AddChildren(this Place _parent, params Place[] _children)
    {
        foreach (Place child in _children)
        {
            child.SetChildOf(_parent);
        }
        return _parent;
    }

    public static void WriteLineage(this Place _place)
    {
        List<Place> placesInReverse = new List<Place> { _place };

        Place? iterator = _place.Parent;
        while (iterator != null)
        {
            placesInReverse.Add(iterator);
            iterator = iterator.Parent;
        }

        placesInReverse.Reverse();
        foreach (Place place in placesInReverse)
        {
            Console.WriteLine(place.Name);
            if (place != placesInReverse.Last()) Console.WriteLine("  |  ");
        }
    }
}
