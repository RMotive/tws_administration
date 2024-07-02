part of '../maintenences_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Maintenance> {
  const _TableAdapter();

  @override
  Future<MigrationView<Maintenance>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Maintenance>> resolver = await Sources.administration.maintenances.view(options, auth);

    MigrationView<Maintenance> view = await resolver.act(const MigrationViewDecode<Maintenance>(MaintenanceDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('maintenance-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Maintenance set, Function closeReinvoke, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Maintenance set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Maintenance set, BuildContext context) {}
}
