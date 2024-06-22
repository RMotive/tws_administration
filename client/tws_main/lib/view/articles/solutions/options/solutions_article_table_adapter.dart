part of '../solutions_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _TableAdapter implements TWSArticleTableAdapter<Solution> {
  const _TableAdapter();

  @override
  Future<MigrationView<Solution>> consume(int page, int range, List<MigrationViewOrderOptions> orderings) async {
    final MigrationViewOptions options = MigrationViewOptions(null, orderings, page, range, false);
    String auth = _sessionStorage.session!.token;
    MainResolver<MigrationView<Solution>> resolver = await Sources.administration.solutions.view(options, auth);

    MigrationView<Solution> view = await resolver.act(const MigrationViewDecode<Solution>(SolutionDecoder())).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('solution-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }

  @override
  Widget composeViewer(Solution set, BuildContext context) {
    return SizedBox.expand(
      child: CSMSpacingColumn(
        spacing: 12,
        crossAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          TWSPropertyViewer(
            label: 'Sign',
            value: set.sign,
          ),
          TWSPropertyViewer(
            label: 'Name',
            value: set.name,
          ),
          TWSPropertyViewer(
            label: 'Description',
            value: set.description,
          ),
        ],
      ),
    );
  }

  @override
  Widget? composeEditor(Solution set, BuildContext context) => null;

  @override
  void onRemoveRequest(Solution set, BuildContext context) {}
}
