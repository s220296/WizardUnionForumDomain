namespace WizardUnion.Names;

public class NoName : IName
{
    public string Get() => "nameless";
    public override string ToString() => Get();
}
