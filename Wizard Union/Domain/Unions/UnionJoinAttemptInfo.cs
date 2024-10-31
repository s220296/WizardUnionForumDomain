namespace WizardUnion.Unions;

public enum UnionJoinAttemptInfo : int
{
    ERROR = -3000,

    ALREADY_JOINED = 0,

    DECLINED = 1,
    REQUESTED = 2,
    JOINED = 3,
}
