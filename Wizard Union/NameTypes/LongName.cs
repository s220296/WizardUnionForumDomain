using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Names;

public class LongName : IName
{
    protected IEnumerable<string> Names;

    public LongName(IEnumerable<string> _names) => Names = _names;
    public LongName(params string[] _names) => Names = _names;

    public string Get()
    {
        string result = string.Empty;
        foreach (string name in Names) { result += name; result += " "; }
        result = result.TrimEnd();
        return result;
    }
    public override string ToString() => Get();
}
