namespace WizardUnion.Unions;

public interface IUnionRole
{
    UnionRoleInfo GetRoleProperty(string _name);
    int GetAccessLevel();
}
