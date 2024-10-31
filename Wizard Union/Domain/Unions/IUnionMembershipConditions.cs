using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardUnion.Unions;

public interface IUnionMembershipConditions
{
    UnionJoinAttemptInfo IsSatisfiedBy(Wizard _wizard);
}
