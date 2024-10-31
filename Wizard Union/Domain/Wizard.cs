using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Names;
using WizardUnion.Places;
using WizardUnion.Birth;
using WizardUnion.Domain.MagicProfile;

namespace WizardUnion;

public class Wizard
{
    // union membership
    public MagicProfile MagicProfile { get; protected set; }
    public BirthDetails BirthDetails { get; protected set; }
    public IName Name { get; protected set; }
 
    public Wizard(IName _name, BirthDetails _birthDetails, MagicProfile _magicProfile) => 
        (Name, BirthDetails, MagicProfile) = (_name, _birthDetails, _magicProfile);

    public override string ToString()
    {
        return $"I am {Name.Get()} from {BirthDetails.PlaceOfBirth}.";
    }
}