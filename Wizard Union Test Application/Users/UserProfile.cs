using WizardUnion;

namespace WU_Test;

public sealed class UserProfile
{
    public WizardMessager Messager { get; private set; }
    public IDItem<Wizard> Wizard { get; private set; }

    private UserProfile() { throw new ArgumentNullException($"Please initialize item: {nameof(UserProfile)} {typeof(UserProfile)}."); }

    public UserProfile(IDItem<Wizard> _wizard, WizardMessager _messager)
    {
        (Wizard, Messager) = (_wizard, _messager);
    }
}
