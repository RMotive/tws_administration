part of '../situations_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Situation> {
  const _TableAdapter();

  @override
  Future<SetViewOut<Situation>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<Situation> options = SetViewOptions<Situation>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Situation>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Situation>> resolver = await Sources.administration.situations.view(options, auth);

    SetViewOut<Situation> view = await resolver.act((JObject json) => SetViewOut<Situation>.des(json, Situation.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('solution-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(Situation set, Function closeReinvoke, BuildContext context) {
    // TODO: implement composeEditor
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(Situation set, BuildContext context) {
    // TODO: implement composeViewer
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(Situation set, BuildContext context) {
    // TODO: implement onRemoveRequest
  }
}
