﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Names;

public class FullName : IName
{
    protected string FirstName;
    protected string LastName;

    public FullName(string _firstName, string _lastName) => (FirstName, LastName) = (_firstName, _lastName);

    public string Get() => $"{FirstName} {LastName}";

    public override string ToString() => Get();
}