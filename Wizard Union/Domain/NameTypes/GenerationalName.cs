namespace WizardUnion.Names;

public class GenerationalName : IName
{
    protected IName Name;
    protected uint Generation;

    public GenerationalName(IName _name, uint _generation) => (Name, Generation) = (_name, _generation);

    public string Get()
    {
        string genSuffix = Generation > 9 && Generation < 20 ? "th" : // is a teenth
            Generation % 10 == 1 ? "st" : // is a first
            Generation % 10 == 2 ? "nd" : // is a second
            Generation % 10 == 3 ? "rd" : // is a third
            "th";
        return $"{Name.Get()} the {Generation}{genSuffix}";
    }
    public override string ToString() => Get();
}
