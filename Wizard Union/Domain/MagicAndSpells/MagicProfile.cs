﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.MagicAndSpells;

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
