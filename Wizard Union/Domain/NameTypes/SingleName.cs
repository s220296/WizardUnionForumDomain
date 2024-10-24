﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Names;

public class SingleName : IName
{
    protected string Name;

    public SingleName(string _name) => Name = _name;

    public string Get() => Name;
    public override string ToString() => Get();
}