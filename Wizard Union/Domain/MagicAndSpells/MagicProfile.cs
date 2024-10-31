using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Domain.MagicProfile;

public class MagicProfile
{
    public string Mastery;
    public SpellProfileList KnownSpells;

    public MagicProfile(string _mastery, SpellProfileList _knownSpells)
    {
        if (string.IsNullOrWhiteSpace(_mastery)) _mastery = "None";
        
        (Mastery, KnownSpells) = (_mastery, _knownSpells);
    }
}
