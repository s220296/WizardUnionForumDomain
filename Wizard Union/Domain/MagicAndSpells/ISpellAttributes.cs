using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Domain.MagicAndSpells;

public interface ISpellAttributes
{
    public bool HasAttribute(int _ID);
    public SpellAttribute GetAttribute(int _ID);
}
