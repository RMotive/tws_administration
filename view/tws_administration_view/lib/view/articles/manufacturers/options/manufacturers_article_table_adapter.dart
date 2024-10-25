part of '../manufacturers_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Manufacturer> {
  const _TableAdapter();

  @override
  Future<SetViewOut<Manufacturer>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<Manufacturer> options = SetViewOptions<Manufacturer>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Manufacturer>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Manufacturer>> resolver = await Sources.administration.manufacturers.view(options, auth);

    SetViewOut<Manufacturer> view = await resolver.act((JObject json) => SetViewOut<Manufacturer>.des(json, Manufacturer.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('manufacturer-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Manufacturer set, Function closeReinvoke, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Manufacturer set, BuildContext context) {
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Manufacturer set, BuildContext context) {}
}
