using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Domain.MagicAndSpells;

namespace WizardUnion.Domain.MagicProfile;

public class SpellProfile
{
    public SpellMastery Mastery { get; protected set; }
    public ISpellAttributes Attributes { get; protected set; }

    public SpellProfile(SpellMastery _mastery, ISpellAttributes _attributes)
    {
        (Mastery, Attributes) = (_mastery, _attributes);
    }
}
