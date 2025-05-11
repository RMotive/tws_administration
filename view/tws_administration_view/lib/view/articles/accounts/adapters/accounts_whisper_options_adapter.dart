part of '../whispers/accounts_create_whisper.dart';
/// Consumer adapter for fetch [Permit] view records.
final class _PermitsListAdapter implements TWSViewConsumeAdapter{

  @override
  Future<List<SetViewOut<Permit>>> consume(int page, int range, List<SetViewOrderOptions> orderings, String input) async {
    String auth = _sessionStorage.session!.token;
    List<SetViewOut<Permit>> permits = <SetViewOut<Permit>>[];
    final SetViewOptions<Permit> options = SetViewOptions<Permit>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Permit>>[]);

    MainResolver<SetViewOut<Permit>> resolver = await Sources.foundationSource.permits.view(options, auth);

    SetViewOut<Permit> view = await resolver.act((JObject json) => SetViewOut<Permit>.des(json, Permit.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('permit-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    permits.add(view);
    return permits;
  }
}
/// Consumer adapter for fetch [Profile] view records.
final class _ProfileListAdapter implements TWSViewConsumeAdapter{

  @override
  Future<List<SetViewOut<Profile>>> consume(int page, int range, List<SetViewOrderOptions> orderings, String input) async {
    String auth = _sessionStorage.session!.token;
    List<SetViewOut<Profile>> permits = <SetViewOut<Profile>>[];
    final SetViewOptions<Profile> options = SetViewOptions<Profile>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Profile>>[]);

    MainResolver<SetViewOut<Profile>> resolver = await Sources.foundationSource.profiles.view(options, auth);

    SetViewOut<Profile> view = await resolver.act((JObject json) => SetViewOut<Profile>.des(json, Profile.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('profile-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    permits.add(view);
    return permits;
  }

}