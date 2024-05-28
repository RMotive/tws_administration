part of '../scts_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableDataAdapter<SCT> {
  const _TableAdapter();

  @override
  Future<MigrationView<SCT>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<SCT>> resolver = await administration.scts.view(options, auth);

    MigrationView<SCT> view = await resolver.act(const MigrationViewDecode<SCT>(SCTDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('sct-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
}
