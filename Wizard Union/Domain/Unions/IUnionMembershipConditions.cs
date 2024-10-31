namespace WizardUnion.Unions;

public interface IUnionMembershipConditions
{
    UnionJoinAttemptInfo IsSatisfiedBy(Wizard _wizard);
}
