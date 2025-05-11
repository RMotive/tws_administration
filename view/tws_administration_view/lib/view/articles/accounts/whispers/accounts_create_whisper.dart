import 'dart:convert';

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/articles/accounts/accounts_article.dart';
import 'package:tws_administration_view/view/frames/whisper/whisper_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/records_stack/tws_article_creator_stack_item_property.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_agent.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creation_item_state.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creator.dart';
import 'package:tws_administration_view/view/widgets/tws_article_creation/tws_article_creator_feedback.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field/tws_autocomplete_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_confirmation_dialog.dart';
import 'package:tws_administration_view/view/widgets/tws_input_text.dart';
import 'package:tws_administration_view/view/widgets/tws_option_selector.dart';
import 'package:tws_administration_view/view/widgets/tws_section.dart';
import 'package:tws_administration_view/view/widgets/tws_selectable_list.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part '../adapters/accounts_whisper_options_adapter.dart';
part 'dialogs.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

class AccountsCreateWhisper extends CSMPageBase {
  const AccountsCreateWhisper({super.key});

  Future<List<TWSArticleCreatorFeedback>> _onCreateAccounts(List<Account> records, BuildContext context) async {
    final String token = _sessionStorage.getTokenStrict();
    List<TWSArticleCreatorFeedback> feedback = <TWSArticleCreatorFeedback>[];
    //Encoding string to a valid format.
    for(Account account in records){
      account.password = base64Encode(utf8.encode(account.password));
    }

    // --> Create Accounts.
    MainResolver<SetBatchOut<Account>> resolver = await Sources.foundationSource.accounts.create(records, token);
      resolver.resolve(
        decoder: (JObject json) => SetBatchOut<Account>.des(json, Account.des),
        onConnectionFailure: () {
          feedback.add(const TWSArticleCreatorFeedback(TWSArticleCreatorFeedbackTypes.error));
          _conectionDialog(context,"Accounts");
        },
        onException: (Object exception, StackTrace trace) {
          feedback.add(const TWSArticleCreatorFeedback(TWSArticleCreatorFeedbackTypes.error));
          _exceptionDialog(context, "Accounts");
        },
        onFailure: (FailureFrame failure, int status) {},
        onSuccess: (SuccessFrame<SetBatchOut<Account>> success) {
          if (success.estela.failed) {
            //Convert the previous base64 content to string.
            for(Account account in records){
              account.password = utf8.decode(base64Decode(account.password));
            }
            feedback.add(const TWSArticleCreatorFeedback(TWSArticleCreatorFeedbackTypes.error));
            _failureDialog(context, success.estela.failures.first.system, "Accounts", success.estela.failures);
          } 
        },
      );
    return feedback;
  }

  /// Get the [Permit] objects from [AccountPermit] list in [account] parameter and return the content in a list.
  List<Permit> _extractPermits(Account account){
    List<Permit> permits = <Permit>[];
    for(AccountPermit ap in account.accountPermits){
      if(ap.permitNavigation != null) permits.add(ap.permitNavigation!);
    }
    return permits;
  } 
  
  /// Get the [Profile] objects from [AccountProfile] list in [account] parameter and return the content in a list.
  List<Profile> _extractProfiles(Account account){
    List<Profile> profiles = <Profile>[];
    for(AccountProfile ap in account.accountProfiles){
      if(ap.profileNavigation != null) profiles.add(ap.profileNavigation!);
    }
    return profiles;
  } 
  
  @override
  Widget compose(BuildContext ctx, Size window) {
    final TWSArticleCreatorAgent<Account> creatorAgent = TWSArticleCreatorAgent<Account>();
    return WhisperFrame(
      title: 'Account creation',
      trigger: creatorAgent.create,
      child: TWSArticleCreator<Account>(
        factory: Account.a, 
        agent: creatorAgent,
        afterClose: AccountsArticle.agent.refresh,
        onCreate: (List<Account> records) async => _onCreateAccounts(records, ctx),
        modelValidator: (Account model) {
          return model.evaluate().isEmpty;
        },
        itemDesigner:(Account actualModel, bool selected, bool valid) {
          return TWSArticleCreationStackItem(
            selected: selected,
            valid: valid,
            properties: <TwsArticleCreationStackItemProperty>[
              TwsArticleCreationStackItemProperty(
                label: "Identity", 
                minWidth: 150,
                value: actualModel.user,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Password", 
                minWidth: 150,
                value: actualModel.password,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Name", 
                minWidth: 150,
                value: actualModel.contactNavigation?.name,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Lastname", 
                minWidth: 150,
                value: actualModel.contactNavigation?.lastName,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Email", 
                minWidth: 150,
                value: actualModel.contactNavigation?.email,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Phone", 
                minWidth: 150,
                value: actualModel.contactNavigation?.phone,
              ),
              TwsArticleCreationStackItemProperty(
                label: "Wildcard", 
                minWidth: 150,
                value: actualModel.wildcard.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: "Permits number", 
                minWidth: 150,
                value: actualModel.accountPermits.length.toString(),
              ),
              TwsArticleCreationStackItemProperty(
                label: "Profile number", 
                minWidth: 150,
                value: actualModel.accountProfiles.length.toString(),
              ),
            ],
          );
        }, 
        formDesigner:(TWSArticleCreatorItemState<Account>? itemState) {
          final bool formDisabled = !(itemState == null);
          return SingleChildScrollView(
            child: Padding(
              padding: const EdgeInsetsDirectional.all(5),
              child: CSMSpacingColumn(
                spacing: 10,
                children: <Widget>[
                  TWSSection(
                    padding: const EdgeInsets.symmetric(vertical: 10),
                    title: "Contact", 
                    content: CSMSpacingColumn(
                      spacing: 10,
                      children: <Widget>[
                        CSMSpacingRow(
                          spacing: 10,
                          children: <Widget>[
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                maxLength: 50,
                                label: 'Identity',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.user),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      user: text,
                                    ),
                                  );
                                },
                              ),
                            ),
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                label: 'Password',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.password),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      password: text,
                                    ),
                                  );
                                },
                              ),
                            ),
                          ],
                        ),
                        CSMSpacingRow(
                          spacing: 10,
                          children: <Widget>[
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                maxLength: 50,
                                label: 'Name',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.contactNavigation?.name),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      contactNavigation: model.contactNavigation?.clone(
                                        name: text,
                                      ) ?? Contact.a().clone(
                                        name: text,
                                      ),
                                    ),
                                  );
                                },
                              ),
                            ),
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                maxLength: 50,
                                label: 'Lastname',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.contactNavigation?.lastName),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      contactNavigation: model.contactNavigation?.clone(
                                        lastName: text,
                                      ) ?? Contact.a().clone(
                                        lastName: text,
                                      ),
                                    ),
                                  );
                                },
                              ),
                            ),
                          ],
                        ),
                        CSMSpacingRow(
                          spacing: 10,
                          children: <Widget>[
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                maxLength: 30,
                                label: 'Email',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.contactNavigation?.email),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      contactNavigation: model.contactNavigation?.clone(
                                        email: text,
                                      ) ?? Contact.a().clone(
                                        email: text,
                                      ),
                                    ),
                                  );
                                },
                              ),
                            ),
                            Expanded(
                              child: TWSInputText(
                                isEnabled: formDisabled,
                                maxLength: 14,
                                label: 'Phone',
                                isStrictLength: false,
                                controller: TextEditingController(text: itemState?.model.contactNavigation?.phone),
                                onChanged: (String text) {
                                  Account model = itemState!.model;
                                  itemState.updateModelRedrawing(
                                    model.clone(
                                      contactNavigation: model.contactNavigation?.clone(
                                        phone: text,
                                      ) ?? Contact.a().clone(
                                        phone: text,
                                      ),
                                    ),
                                  );
                                },
                              ),
                            ),
                          ],
                        ),
                      ],
                    ),
                  ),
                  TWSSection(
                    padding: const EdgeInsets.symmetric(vertical: 10),
                    title: "Wildcard",
                    content: TwsOptionSelector<bool>(
                      initialValue: itemState?.model.wildcard,
                      options: const <TwsOptionSelectorAction<bool>>[
                        TwsOptionSelectorAction<bool>(
                          minWidth: 150,
                          title: "Enable", 
                          value: true,
                        ),
                        TwsOptionSelectorAction<bool>(
                          minWidth: 150,
                          title: "Disable", 
                          value: false,
                        ),
                      ], 
                      onSelect:(bool value) {
                        Account model = itemState!.model;
                        itemState.updateModelRedrawing(
                          model.clone(
                            wildcard: value,
                          ),
                        );
                      },
                    ),
                  ),
                  TwsSelectableList<Permit>(
                    enabled: formDisabled,
                    heigth: 300,
                    title: "Available permits", 
                    adapter: _PermitsListAdapter(), 
                    initialValues: itemState != null? _extractPermits(itemState.model) : null,
                    isEqual: (Permit item1, Permit item2) {
                      return item1.id == item2.id;
                    },
                    tileTitle:(Permit set) {
                      return "${set.solutionNavigation?.name}: ${set.featureNavigation?.name} - ${set.actionNavigation?.name}";
                    }, 
                    onSelect:(bool selected, Permit item) {
                      Account model = itemState!.model;
                      List<AccountPermit> permits = model.accountPermits;
                      
                      if(selected){
                        permits.add(
                          AccountPermit(
                            model.id, 
                            item.id,
                            null,
                            item,
                          ),
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            accountPermits: permits,
                          ),
                        );
                      } else {
                        permits.removeWhere(
                          (AccountPermit accountPermit) =>
                              accountPermit.permit == item.id,
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            accountPermits: permits,
                          ),
                        );
                      }
                    },
                  ),
                  TwsSelectableList<Profile>(
                    enabled: formDisabled,
                    heigth: 300,
                    title: "Available profiles", 
                    adapter: _ProfileListAdapter(), 
                    initialValues:itemState != null? _extractProfiles(itemState.model) : null,
                    tileTitle:(Profile set) => set.name,
                    isEqual: (Profile item1, Profile item2) {
                      return item1.id == item2.id;
                    },
                    onSelect:(bool selected, Profile item) {
                      Account model = itemState!.model;
                      List<AccountProfile> profiles = model.accountProfiles;
                      if(selected){
                        profiles.add(
                          AccountProfile(
                            model.id, 
                            item.id,
                            null,
                            item,
                          ),
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            accountProfiles: profiles,
                          ),
                        );
                      } else {
                        profiles.removeWhere(
                          (AccountProfile accountPermit) =>
                              accountPermit.profile == item.id,
                        );
                        itemState.updateModelRedrawing(
                          model.clone(
                            accountProfiles: profiles,
                          ),
                        );
                      }
                    },
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
