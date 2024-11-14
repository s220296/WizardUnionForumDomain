using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Unions;

namespace WU_Test;

public sealed class UnionProfile
{
    public UnionMessager Messager { get; private set; }
    public IDItem<Union> Union { get; private set; }

    private UnionProfile() { throw new ArgumentNullException($"Please initialize item: {nameof(UnionProfile)} {typeof(UnionProfile)}."); }

    public UnionProfile(IDItem<Union> _union, UnionMessager _messager)
    {
        (Union, Messager) = (_union, _messager);
    }
}
