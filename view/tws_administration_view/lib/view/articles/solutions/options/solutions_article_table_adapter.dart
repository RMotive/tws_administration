part of '../solutions_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;
final SolutionsServiceBase _solutionsService = Sources.foundationSource.solutions;

final class _TableAdapter extends TWSArticleTableAdapter<Solution> {
  const _TableAdapter();

  @override
  Future<SetViewOut<Solution>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    final SetViewOptions<Solution> options = SetViewOptions<Solution>(false, range, page,null, orderings, <SetViewFilterNodeInterface<Solution>>[]);
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Solution>> resolver = await Sources.foundationSource.solutions.view(options, auth);

    SetViewOut<Solution> view = await resolver.act((JObject json) => SetViewOut<Solution>.des(json, Solution.des)).catchError(
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
  TWSArticleTableEditor? composeEditor(Solution set, Function closeReinvoke, BuildContext context) {
    return TWSArticleTableEditor(
      onCancel: closeReinvoke,
      onSave: () {
        showDialog(
          context: context,
          useRootNavigator: true,
          barrierDismissible: false,
          builder: (BuildContext context) {
            return TWSConfirmationDialog(
              accept: 'Update',
              title: 'Solution update confirmation',
              statement: Text.rich(
                TextSpan(
                  text: 'Are you sure you want to update solution ',
                  children: <InlineSpan>[
                    TextSpan(
                      text: '(${set.sign}):',
                      style: const TextStyle(
                        fontWeight: FontWeight.w800,
                      ),
                    ),
                    const TextSpan(
                      text: '\n',
                    ),
                    const TextSpan(
                      text: '\n\u2022 Description:',
                      style: TextStyle(
                        fontWeight: FontWeight.w800,
                      ),
                    ),
                    WidgetSpan(
                      baseline: TextBaseline.alphabetic,
                      alignment: PlaceholderAlignment.bottom,
                      child: Padding(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 20,
                        ),
                        child: Text('\n${set.description}'),
                      ),
                    ),
                  ],
                ),
              ),
              onAccept: () async {
                final String auth = _sessionStorage.getTokenStrict();
                MainResolver<RecordUpdateOut<Solution>> resolverUpdateOut = await _solutionsService.update(set, auth);
                try {
                  resolverUpdateOut.act((JObject json) => RecordUpdateOut<Solution>.des(json, Solution.des)).then(
                    (RecordUpdateOut<Solution> updateOut) {
                      const CSMAdvisor('solution-update').success('succesf', info: updateOut.updated.encode());
                      CSMRouter.i.pop();
                    },
                  );
                } catch (x) {
                  debugPrint(x.toString());
                }
              },
            );
          },
        );
      },
      form: CSMSpacingColumn(
        spacing: 16,
        children: <Widget>[
          TWSInputText(
            label: 'Sign',
            isEnabled: false,
            maxLength: 5,
            controller: TextEditingController(
              text: set.sign,
            ),
          ),
          TWSInputText(
            label: 'Name',
            isEnabled: false,
            controller: TextEditingController(
              text: set.name,
            ),
          ),
          TWSInputText(
            label: 'Description',
            height: 150,
            controller: TextEditingController(
              text: set.description,
            ),
            onChanged: (String text) {
              set.description = text.isEmpty ? null : text;
            },
            maxLines: null,
          ),
        ],
      ),
    );
  }
}
