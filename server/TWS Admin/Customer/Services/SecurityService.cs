namespace Customer.Services;

public class SecurityService {

    public SecurityService() {
    }

    public static void InitSession() {
        //CriticalOperationResults<dynamic, AccountSet> SearchAccountResult;
        //try {
        //    SearchAccountResult = await AccountsRepository
        //        .Read(I => I.User == Identity.Identity, Foundation.ReadingBehavior.First);
        //} catch (XRecordUnfound<AccountsRepository> X) {
        //    throw new XUnfoundUser(X);
        //}

        //// --> The account was found
        //AccountEntity Account = SearchAccountResult.Successes[0];
        //if (!Account.Password.SequenceEqual(Identity.Password))
        //    throw new XWrongPassword(Account, Identity.Password);

        //SessionModel SessionInited = SessionsManager.InitSession(Account);
        //return SessionInited.GeneratePublicDerivation();
    }
}
