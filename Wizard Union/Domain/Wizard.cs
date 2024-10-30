using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Names;
using WizardUnion.Places;
using WizardUnion.Birth;

namespace WizardUnion;

public class Wizard
{
    // union membership
    // school of magic
    // known spells
    public BirthDetails BirthDetails { get; protected set; }
    public IName Name { get; protected set; }
 
    public Wizard(IName _name, BirthDetails _birthDetails) => 
        (Name, BirthDetails) = (_name, _birthDetails);

    public override string ToString()
    {
        return $"I am {Name.Get()} from {BirthDetails.PlaceOfBirth}.";
    }
}