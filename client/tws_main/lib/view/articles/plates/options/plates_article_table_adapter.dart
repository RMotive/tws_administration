part of '../plates_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableDataAdapter<Plate> {
  const _TableAdapter();

  @override
  Future<MigrationView<Plate>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Plate>> resolver = await administration.plates.view(options, auth);

    MigrationView<Plate> view = await resolver.act(const MigrationViewDecode<Plate>(PlateDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('Plate-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
}
