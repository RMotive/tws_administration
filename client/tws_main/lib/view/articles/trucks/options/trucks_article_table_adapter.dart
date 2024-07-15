part of '../trucks_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Truck> {
  const _TableAdapter();

  @override
  Future<MigrationView<Truck>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Truck>> resolver = await Sources.administration.trucks.view(options, auth);

    MigrationView<Truck> view = await resolver.act(const MigrationViewDecode<Truck>(TruckDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('truck-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Truck set, Function closeReinvoke, BuildContext context) {
    // TODO: implement composeEditor
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Truck set, BuildContext context) {
    // TODO: implement composeViewer
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Truck set, BuildContext context) {
    // TODO: implement onRemoveRequest
  }
}
