namespace WizardUnion.Unions;

public class UnionMembership
{
    public Union Union { get; protected set; }
    public IUnionRole Role { get; protected set; }

    public UnionMembership(Union _union)
    {
        (Union, Role) = (_union, _union.DefaultRole);
    }

    public void SetRole(IUnionRole _newRole) => Role = _newRole;
}
