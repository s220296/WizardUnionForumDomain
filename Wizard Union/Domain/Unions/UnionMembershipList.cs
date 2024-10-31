namespace WizardUnion.Unions;

public class UnionMembershipList
{
    protected List<UnionMembership> m_unionMembershipList;
    protected Wizard m_owner;

    public UnionMembershipList(Wizard _owner)
    {
        m_unionMembershipList = new List<UnionMembership>();
        m_owner = _owner;
    }

    public UnionJoinAttemptInfo TryJoin(Union _union)
    {
        return _union.TryJoin(m_owner);
    }
}
