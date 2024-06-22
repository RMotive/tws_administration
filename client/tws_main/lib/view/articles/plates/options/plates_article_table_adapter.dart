part of '../plates_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Plate> {
  const _TableAdapter();

  @override
  Future<MigrationView<Plate>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Plate>> resolver = await Sources.administration.plates.view(options, auth);

    MigrationView<Plate> view = await resolver.act(const MigrationViewDecode<Plate>(PlateDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('Plate-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  Widget? composeEditor(Plate set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Plate set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Plate set, BuildContext context) {}
}
