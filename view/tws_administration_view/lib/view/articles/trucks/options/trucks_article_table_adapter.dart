part of '../trucks_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Truck> {
  const _TableAdapter();

  @override
  Future<SetViewOut<Truck>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<Truck> options = SetViewOptions<Truck>(false, page, range, null, orderings, <SetViewFilterNodeInterface<Truck>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Truck>> resolver = await Sources.administration.trucks.view(options, auth);

    SetViewOut<Truck> view = await resolver.act((JObject json) => SetViewOut<Truck>.des(json, Truck.des)).catchError(
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
