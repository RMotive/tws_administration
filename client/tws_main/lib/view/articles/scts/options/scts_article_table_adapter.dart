part of '../scts_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<SCT> {
  const _TableAdapter();

  @override
  Future<MigrationView<SCT>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<SCT>> resolver = await Sources.administration.scts.view(options, auth);

    MigrationView<SCT> view = await resolver.act(const MigrationViewDecode<SCT>(SCTDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('sct-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }
  
  @override
  TWSArticleTableEditor? composeEditor(SCT set, Function clsoeReinvoke, BuildContext context) {
    // TODO: implement composeEditor
    throw UnimplementedError();
  }

  @override
  Widget composeViewer(SCT set, BuildContext context) {
    // TODO: implement composeViewer
    throw UnimplementedError();
  }

  @override
  void onRemoveRequest(SCT set, BuildContext context) {
    // TODO: implement onRemoveRequest
  }
}
