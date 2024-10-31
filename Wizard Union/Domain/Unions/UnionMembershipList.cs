using System.Collections.ObjectModel;

namespace WizardUnion.Unions;

public class UnionMembershipList
{
    protected Wizard m_owner;
    protected List<UnionMembership> m_unionMembershipList;
    
    public ReadOnlyCollection<UnionMembership> Memberships => m_unionMembershipList.AsReadOnly();

    public UnionMembershipList(Wizard _owner)
    {
        m_unionMembershipList = new List<UnionMembership>();
        m_owner = _owner;
    }

    public UnionJoinAttemptInfo TryJoin(Union _union)
    {
        UnionJoinAttemptInfo info = _union.TryJoin(m_owner);

        if (info == UnionJoinAttemptInfo.JOINED)
            m_unionMembershipList.Add(new UnionMembership(_union));

        return info;
    }

    public void Leave(Union _union)
    {
        foreach (UnionMembership membership in m_unionMembershipList)
        {
            if (membership.Union == _union)
            {
                _union.Leave(m_owner);
                m_unionMembershipList.Remove(membership);
                break;
            }    
        }
    }
}
