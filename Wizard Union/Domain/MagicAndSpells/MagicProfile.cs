namespace WizardUnion.MagicAndSpells;

public class MagicProfile
{
    public SpellMastery Mastery { get; set; }
    public SpellProfileList KnownSpells { get; protected set; }

    public MagicProfile(SpellMastery _mastery, SpellProfileList _knownSpells)
    {
        (Mastery, KnownSpells) = (_mastery, _knownSpells);
    }
}
