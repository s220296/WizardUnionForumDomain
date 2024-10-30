using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Domain.MagicProfile;

public class MagicProfile
{
    public string Mastery;

    public MagicProfile(string _mastery)
    {
        if (string.IsNullOrWhiteSpace(_mastery)) _mastery = "None";
        
        Mastery = _mastery;
    }
}
