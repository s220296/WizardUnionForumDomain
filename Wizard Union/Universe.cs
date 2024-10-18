using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Places;

namespace WizardUnion;

public static class Universe
{
    public static Place Place = new Place(1d, "Universe");

    public static double CurrentEons { get; private set; }

    public static void Begin(double _beginningEon = 0d)
    {
        CurrentEons = _beginningEon;
    }
}
