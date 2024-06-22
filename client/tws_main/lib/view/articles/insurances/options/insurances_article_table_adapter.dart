part of '../insurances_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Insurance> {
  const _TableAdapter();

  @override
  Future<MigrationView<Insurance>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Insurance>> resolver = await Sources.administration.insurances.view(options, auth);

    MigrationView<Insurance> view = await resolver.act(const MigrationViewDecode<Insurance>(InsuranceDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('solution-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  Widget? composeEditor(Insurance set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Insurance set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Insurance set, BuildContext context) {}
}
