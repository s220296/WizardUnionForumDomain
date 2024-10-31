using System.Collections.ObjectModel;

namespace WizardUnion.Unions;

public class Union
{
    private IUnionMembershipConditions m_conditions;
    private List<Wizard> m_members;
    public ReadOnlyCollection<Wizard> Members => m_members.AsReadOnly();

    public Union(IUnionMembershipConditions _conditions)
    {
        m_conditions = _conditions;
        m_members = new List<Wizard>();
    }

    internal UnionJoinAttemptInfo TryJoin(Wizard _wizard)
    {
        return m_conditions.IsSatisfiedBy(_wizard);
    }
}
