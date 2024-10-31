using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Domain.MagicAndSpells;

namespace WizardUnion.Domain.MagicProfile;

public class SpellProfileList : List<SpellProfile>
{
    public static SpellProfileList Empty = new SpellProfileList();

    public SpellProfileList() : base()
    { }

    public SpellProfileList(int _capacity) : base(_capacity) 
    { }

    public SpellProfileList(IEnumerable<SpellProfile> _collection) : base(_collection) 
    { }

    public SpellProfileList SortedBy(IComparer<SpellProfile> _comparer)
    {
        SpellProfileList result = new SpellProfileList(this);
        result.Sort(_comparer);
        return result;
    }

    public SpellProfileList GetSpellsOfMastery(SpellMastery _mastery)
    {
        SpellProfileList result = new SpellProfileList(this);
        result.RemoveAll((spell) => spell.Mastery != _mastery);
        return result;
    }

    public SpellProfileList GetSpellsWithAttribute(SpellAttribute _attribute)
    {
        SpellProfileList result = new SpellProfileList(this);
        result.RemoveAll((spell) => !spell.Attributes.HasAttribute(_attribute.ID));
        return result;
    }

    public SpellProfileList GetSpellsWithoutAttribute(SpellAttribute _attribute)
    {
        SpellProfileList result = new SpellProfileList(this);
        result.RemoveAll((spell) => spell.Attributes.HasAttribute(_attribute.ID));  
        return result;
    }

    public bool IsEmpty() => Count == 0;
}
