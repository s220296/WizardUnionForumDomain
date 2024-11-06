using WizardUnion;

namespace WU_Test;

public sealed class UserProfile
{
    public WizardMessager Messager { get; private set; }
    public Wizard Wizard => m_wizardID.Item;

    private IDItem<Wizard> m_wizardID;

    private UserProfile() { throw new ArgumentNullException($"Please initialize item: {nameof(UserProfile)} {typeof(UserProfile)}."); }

    public UserProfile(IDItem<Wizard> _wizard, WizardMessager _messager)
    {
        (m_wizardID, Messager) = (_wizard, _messager);
    }
}
