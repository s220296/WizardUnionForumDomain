using WizardUnion.Birth;
using WizardUnion.MagicAndSpells;
using WizardUnion.Names;
using WizardUnion.Unions;

namespace WizardUnion;

public class Wizard
{
    public UnionMembershipList UnionMemberships { get; protected set; }
    public MagicProfile MagicProfile { get; protected set; }
    public BirthDetails BirthDetails { get; protected set; }
    public IName Name { get; protected set; }

    public Wizard(IName _name, BirthDetails _birthDetails, MagicProfile _magicProfile)
    {
        (Name, BirthDetails, MagicProfile) = (_name, _birthDetails, _magicProfile);
        UnionMemberships = new UnionMembershipList(this);
    }

    public override string ToString()
    {
        return $"I am {Name.Get()} from {BirthDetails.PlaceOfBirth}.";
    }
}