part of '../manufacturers_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Manufacturer> {
  const _TableAdapter();

  @override
  Future<MigrationView<Manufacturer>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Manufacturer>> resolver = await Sources.administration.manufacturers.view(options, auth);

    MigrationView<Manufacturer> view = await resolver.act(const MigrationViewDecode<Manufacturer>(ManufacturerDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('manufacturer-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  Widget? composeEditor(Manufacturer set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Manufacturer set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Manufacturer set, BuildContext context) {}
}
