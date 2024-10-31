using System.Collections.ObjectModel;

namespace WizardUnion.Unions;

public class Union
{
    public string Name { get; protected set; }
    public IUnionRole DefaultRole { get; protected set; }
    
    private IUnionMembershipConditions m_conditions;
    private List<Wizard> m_members;
    
    public ReadOnlyCollection<Wizard> Members => m_members.AsReadOnly();

    public Union(string _name, IUnionMembershipConditions _conditions, IUnionRole _defaultRole)
    {
        (Name, DefaultRole) = (_name, _defaultRole);

        m_conditions = _conditions;
        m_members = new List<Wizard>();
    }

    internal UnionJoinAttemptInfo TryJoin(Wizard _wizard)
    {
        if (m_members.Contains(_wizard)) 
            return UnionJoinAttemptInfo.ALREADY_JOINED;

        UnionJoinAttemptInfo info = m_conditions.IsSatisfiedBy(_wizard);

        if (info == UnionJoinAttemptInfo.JOINED)
            m_members.Add(_wizard);

        return info;
    }

    internal void Leave(Wizard _wizard)
    {
        if (m_members.Contains(_wizard))
            m_members.Remove(_wizard);
    }
}
