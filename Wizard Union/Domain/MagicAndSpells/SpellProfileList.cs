using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public SpellProfileList GetSpellsOfMastery(string _mastery)
    {
        
        // TODO: Create a SpellMastery struct that contains a string, this struct will handle all of the validation/sanitisation

        SpellProfileList result = new SpellProfileList(this);
        result.RemoveAll
    }

    public bool IsEmpty() => Count == 0;
}
