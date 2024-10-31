namespace WizardUnion.MagicAndSpells;

public class SpellProfile
{
    public SpellMastery Mastery { get; protected set; }
    public ISpellAttributes Attributes { get; protected set; }

    public SpellProfile(SpellMastery _mastery, ISpellAttributes _attributes)
    {
        (Mastery, Attributes) = (_mastery, _attributes);
    }
}
