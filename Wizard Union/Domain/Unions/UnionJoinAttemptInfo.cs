using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Unions;

public enum UnionJoinAttemptInfo : int
{
    ERROR = -3000,

    DECLINED = 1,
    REQUESTED = 2,
    JOINED = 3,
}
