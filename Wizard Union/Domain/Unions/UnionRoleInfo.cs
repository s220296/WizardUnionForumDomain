namespace WizardUnion.Unions;

public struct UnionRoleInfo
{
    public string Name { get; private set; }
    public object Property { get; private set; }

    public UnionRoleInfo(string _name, object _property)
    {
        (Name, Property) = (_name, _property);
    }
}
