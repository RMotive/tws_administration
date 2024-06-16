import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';
import 'package:tws_main/view/frames/whisper/whisper_frame.dart';
import 'package:tws_main/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item.dart';
import 'package:tws_main/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item_property.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_agent.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_main/view/widgets/tws_article_creation/tws_article_creator.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

final SolutionsServiceBase _solutionsService = Sources.administration.solutions;
final SessionStorage _sessionStorage = SessionStorage.i;

final class SolutionsCreateWhisper extends CSMPageBase {
  const SolutionsCreateWhisper({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    final TWSArticleCreatorAgent<Solution> creatorAgent = TWSArticleCreatorAgent<Solution>();

    return WhisperFrame(
      title: 'Create solutions',
      trigger: creatorAgent.create,
      child: TWSArticleCreator<Solution>(
        agent: creatorAgent,
        factory: () => Solution.a(),
        modelValidator: (Solution model) => model.evaluate().isEmpty,
        onCreate: (List<Solution> records) async {
          final String currentToken = _sessionStorage.getTokenStrict();
          (await _solutionsService.create(records, currentToken)).resolve(
            onConnectionFailure: () {
              print('connection error');
            },
            onFailure: (FailureFrame failure, int status) {
              print(failure.estela.advise);
            },
            onSuccess: (SuccessFrame<MigrationTransactionResult<Solution>> success) {
              print('creation successful');
            },
          );
          return null;
        },
        formDesigner: (TWSArticleCreatorItemState<Solution>? itemState) {
          final bool formDisabled = !(itemState == null);

          return Padding(
            padding: const EdgeInsets.all(20),
            child: CSMSpacingColumn(
              spacing: 12,
              children: <Widget>[
                CSMSpacingRow(
                  spacing: 12,
                  crossAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Expanded(
                      child: TWSInputText(
                        label: 'Name',
                        controller: TextEditingController(text: itemState?.model.name),
                        onChanged: (String text) {
                          Solution model = itemState!.model;
                          itemState.updateModelRedrawing(
                            model.clone(
                              name: text,
                            ),
                          );
                        },
                        isEnabled: formDisabled,
                      ),
                    ),
                    Expanded(
                      child: TWSInputText(
                        label: 'Sign',
                        controller: TextEditingController(text: itemState?.model.sign),
                        onChanged: (String text) {
                          Solution model = itemState!.model;
                          itemState.updateModelRedrawing(
                            model.clone(
                              sign: text,
                            ),
                          );
                        },
                        maxLength: 5,
                        isEnabled: formDisabled,
                      ),
                    ),
                  ],
                ),
                TWSInputText(
                  label: 'Description',
                  height: 150,
                  controller: TextEditingController(text: itemState?.model.description),
                  onChanged: (String text) {
                    Solution model = itemState!.model;
                    itemState.updateModelRedrawing(
                      model.clone(
                        description: text.isEmpty ? null : text,
                      ),
                    );
                  },
                  isEnabled: formDisabled,
                  maxLines: null,
                ),
              ],
            ),
          );
        },
        itemDesigner: (Solution actualModel, bool selected, bool valid) {
          return TWSArticleCreationStackItem(
            selected: selected,
            valid: valid,
            properties: <TwsArticleCreationStackItemProperty>[
              TwsArticleCreationStackItemProperty(
                label: 'Name',
                minWidth: 150,
                value: actualModel.name,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Sign',
                minWidth: 150,
                value: actualModel.sign,
              ),
              TwsArticleCreationStackItemProperty(
                label: 'Description',
                value: actualModel.description,
              ),
            ],
          );
        },
      ),
    );
  }
}
