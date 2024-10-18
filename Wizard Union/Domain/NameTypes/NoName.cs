using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Names;

public class NoName : IName
{
    public string Get() => "nameless";
    public override string ToString() => Get();
}
