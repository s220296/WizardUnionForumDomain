namespace WizardUnion.Unions;

public enum UnionJoinAttemptInfo : int
{
    ERROR = -3000,

    ALREADY_JOINED = 0,

    DECLINED = 1000,

    REQUESTED = 2000,

    JOINED = 3000,
}
