part of '../situations_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Situation> {
  const _TableAdapter();

  @override
  Future<MigrationView<Situation>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Situation>> resolver = await Sources.administration.situations.view(options, auth);

    MigrationView<Situation> view = await resolver.act(const MigrationViewDecode<Situation>(SituationDecoder())).catchError(
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
