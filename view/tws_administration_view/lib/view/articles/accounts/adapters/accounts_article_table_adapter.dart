part of '../accounts_article.dart';

final SessionStorage _sessionStorage = SessionStorage.i;

final class _DialogState extends CSMStateBase {}
final _DialogState _dialogState = _DialogState();
void Function() _dialogEffect = (){};

/// Flag for record details visibility.
bool _isShowing = false;
///Agent for the "Efective Permtis" list showed in record details.
final CSMConsumerAgent consumerAgent = CSMConsumerAgent(); 

final class _AccountState {
  // The user input text for the selected account record.
  // This is the legible text typed by the user. When the update query is sended, this value is encoded to base64.
  String userPasswordInput = "";
}
/// Initilize an class instance for [_AccountState].
_AccountState _accountState = _AccountState();

final class _PermitsListAdapter implements TWSViewConsumeAdapter{

  @override
  Future<List<SetViewOut<Permit>>> consume(int page, int range, List<SetViewOrderOptions> orderings, String input) async {
    String auth = _sessionStorage.session!.token;
    List<SetViewOut<Permit>> permits = <SetViewOut<Permit>>[];
    final SetViewOptions<Permit> options = SetViewOptions<Permit>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Permit>>[]);

    MainResolver<SetViewOut<Permit>> resolver = await Sources.foundationSource.permits.view(options, auth);

    SetViewOut<Permit> view = await resolver.act((JObject json) => SetViewOut<Permit>.des(json, Permit.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('permit-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    permits.add(view);
    return permits;
  }
}

final class _ProfileListAdapter implements TWSViewConsumeAdapter{

  @override
  Future<List<SetViewOut<Profile>>> consume(int page, int range, List<SetViewOrderOptions> orderings, String input) async {
    String auth = _sessionStorage.session!.token;
    List<SetViewOut<Profile>> permits = <SetViewOut<Profile>>[];
    final SetViewOptions<Profile> options = SetViewOptions<Profile>(false, range, page, null, orderings, <SetViewFilterNodeInterface<Profile>>[]);

    MainResolver<SetViewOut<Profile>> resolver = await Sources.foundationSource.profiles.view(options, auth);

    SetViewOut<Profile> view = await resolver.act((JObject json) => SetViewOut<Profile>.des(json, Profile.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('profile-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    permits.add(view);
    return permits;
  }

}

final class _TableAdapter extends TWSArticleTableAdapter<Account> {
  final _AccountArticleState state;
  const _TableAdapter(
    this.state,
  );

  Future<SetViewOut<Permit>> permits(Account account) async {
    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Permit>> resolver = await Sources.foundationSource.accounts.getPermits(account, auth);

    SetViewOut<Permit> view = await resolver.act((JObject json) => SetViewOut<Permit>.des(json, Permit.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('permits-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
  }

  @override
  Future<SetViewOut<Account>> consume(int page, int range, List<SetViewOrderOptions> orderings) async {
    // final SetViewOptions<Account> options = SetViewOptions<Account>(false, range, page, null, orderings, state.driversFilters);
    final SetViewOptions<Account> options = SetViewOptions<Account>(false, range, page, null, orderings, state.accountsFilters);

    String auth = _sessionStorage.session!.token;
    MainResolver<SetViewOut<Account>> resolver = await Sources.foundationSource.accounts.view(options, auth);

    SetViewOut<Account> view = await resolver.act((JObject json) => SetViewOut<Account>.des(json, Account.des)).catchError(
      (Object x, StackTrace s) {
        const CSMAdvisor('account-table-adapter').exception('Exception catched at table view consume', Exception(x), s);
        throw x;
      },
    );
    return view;
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
  TWSArticleTableEditor? composeEditor(Account set, Function closeReinvoke, BuildContext context) {
    bool exceptionFlag = false;
    String xMessage = '---';

     return TWSArticleTableEditor(
      onCancel: closeReinvoke,
      onSave: () async {
        exceptionFlag = false;
        xMessage = '---';
        showDialog(
          context: context,
          useRootNavigator: true,
          barrierDismissible: false,
          builder: (BuildContext context) {
            return CSMDynamicWidget<_DialogState>(
              state: _dialogState, 
              designer:(BuildContext ctx, _DialogState state) {
                _dialogEffect = state.effect;
                return exceptionFlag? TWSConfirmationDialog(
                  showCancelButton: false,
                  accept: 'OK',
                  title: 'Unexpected error on update.',
                  statement: Text.rich(
                    textAlign: TextAlign.center,
                    TextSpan(
                      text: 'Unexpected problem. Please retry the operation or contact your administrator.',
                      children: <InlineSpan>[
                        const TextSpan(
                          text: '\n\nError message:\n\n',
                          style: TextStyle(fontWeight: FontWeight.bold),                        
                        ),
                        TextSpan(
                         text: xMessage
                        ),
                      ],     
                    ),
                  ),
                  onAccept: () {
                    Navigator.of(context).pop();
                  },
                ) : 
                TWSConfirmationDialog(
                  accept: 'Update',
                  title: 'Account update confirmation',
                  statement: Text.rich(
                    textAlign: TextAlign.center,
                    TextSpan(
                        text: 'Are you sure you want to update this account?',
                      children: <InlineSpan>[
                      const TextSpan(
                          text: '\n',
                        ),
                        const TextSpan(
                          text: '\n\u2022 User:',
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
                            child: Text('\n${set.user}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Password:',
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
                            child: Text('\n${_accountState.userPasswordInput}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Name:',
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
                            child: Text('\n${set.contactNavigation?.name}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Lastname:',
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
                            child: Text('\n${set.contactNavigation?.lastName}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Email:',
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
                            child: Text('\n${set.contactNavigation?.email}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Phone number:',
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
                            child: Text('\n${set.contactNavigation?.phone}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Wildcard:',
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
                            child: Text('\n${set.wildcard}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Permits number:',
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
                            child: Text('\n${set.accountPermits.length}'),
                          ),
                        ),
                        const TextSpan(
                          text: '\n\u2022 Profiles number:',
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
                            child: Text('\n${set.accountProfiles.length}'),
                          ),
                        ),
                      ],
                    ),
                  ),
                  onAccept: () async {
                    set.password = base64Encode(utf8.encode(_accountState.userPasswordInput));
                    List<CSMSetValidationResult> evaluation = set.evaluate();
                    if (evaluation.isEmpty) {
                      final String auth = _sessionStorage.getTokenStrict();
                      MainResolver<RecordUpdateOut<Account>> resolverUpdateOut =
                          await Sources.foundationSource.accounts.update(set, auth);
                      try {
                        resolverUpdateOut
                            .act((JObject json) =>
                                RecordUpdateOut<Account>.des(json, Account.des))
                            .then(
                          (RecordUpdateOut<Account> updateOut) {
                            CSMRouter.i.pop();
                          },
                        ).onError(
                          (Object? x, _){
                            exceptionFlag = true;
                            xMessage = x.toString();
                            _dialogEffect();
                          }
                        );
                      } catch (x) {
                        exceptionFlag = true;
                        xMessage = x.toString();
                        _dialogEffect();
                      }
                    } else {
                      // --> Evaluation error dialog
                      CSMRouter.i.pop();
                      showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return TWSConfirmationDialog(
                            showCancelButton: false,
                            accept: 'Ok',
                            title: 'Invalid form data',
                            statement: Text.rich(
                              TextSpan(
                                text: 'Verify the data form:\n\n',
                                children: <InlineSpan>[
                                  for (int i = 0; i < evaluation.length; i++)
                                    TextSpan(
                                      text:
                                          "${i + 1} - ${evaluation[i].property}: ${evaluation[i].reason}\n",
                                      style: const TextStyle(
                                          fontWeight: FontWeight.w600),
                                    ),
                                ],
                              ),
                            ),
                            onAccept: () {
                              Navigator.of(context).pop();
                            },
                          );
                        },
                      );
                    }
                  },
                );
              },
            );
          },
        );
      },
      form: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.symmetric(vertical: 10.0),
          child: CSMSpacingColumn(
            spacing: 10,
            children: <Widget>[
              TWSInputText(
                label: "Identity",
                hint: "Enter an identity",
                maxLength: 50,
                controller: TextEditingController(
                  text: set.user,
                ),
                onChanged: (String text) {
                  set.user = text;
                },
              ),
              TWSInputText(
                label: "Password",
                hint: "Enter an password",
                controller: TextEditingController(
                  text: _accountState.userPasswordInput
                ),
                onChanged: (String text) {
                  _accountState.userPasswordInput = text;
                },
              ),
              TWSInputText(
                label: "Name",
                hint: "Enter a name",
                maxLength: 50,
                controller: TextEditingController(
                  text: set.contactNavigation?.name,
                ),
                onChanged: (String text) {
                  set.contactNavigation?.name = text;
                },
              ),
              TWSInputText(
                label: "Lastname",
                hint: "Enter a lastname",
                maxLength: 50,
                controller: TextEditingController(
                  text: set.contactNavigation?.lastName,
                ),
                onChanged: (String text) {
                  set.contactNavigation?.lastName = text;
                },
              ),
              TWSInputText(
                label: "Email",
                hint: "Enter a email",
                maxLength: 50,
                controller: TextEditingController(
                  text: set.contactNavigation?.email,
                ),
                onChanged: (String text) {
                  set.contactNavigation?.email = text;
                },
              ),
              TWSInputText(
                label: "Phone number",
                hint: "Enter a phone number",
                maxLength: 50,
                controller: TextEditingController(
                  text: set.contactNavigation?.phone,
                ),
                onChanged: (String text) {
                  set.contactNavigation?.phone = text;
                },
              ),
              TWSSection(
                title: "Wildcard", 
                content: TwsOptionSelector<bool>(
                  initialValue: set.wildcard,
                  onSelect: (bool  selection) {
                    set.wildcard = selection;
                    print(set.wildcard);
                  },
                  options: const <TwsOptionSelectorAction<bool>>[
                    TwsOptionSelectorAction<bool>(
                      title: "Disabled",
                      minWidth: double.maxFinite,
                      value: false,
                    ),
                    TwsOptionSelectorAction<bool>(
                      title: "Enabled", 
                      minWidth: double.maxFinite,
                      value: true,
                    ),
                  ], 
                ),
              ),
              TwsSelectableList<Permit>(
                heigth: 300,
                title: "Available permits", 
                adapter: _PermitsListAdapter(), 
                initialValues: _extractPermits(set),
                isEqual: (Permit item1, Permit item2) {
                  return item1.id == item2.id;
                },
                tileTitle:(Permit set) {
                  return "${set.solutionNavigation?.name}: ${set.featureNavigation?.name} - ${set.actionNavigation?.name}";
                }, 
                onSelect:(bool selected, Permit item) {
                  if(selected){
                    set.accountPermits.add(
                      AccountPermit(
                        set.id, 
                        item.id, 
                        null,
                        null
                      ),
                    );
                  } else{
                    set.accountPermits.removeWhere(
                      (AccountPermit accountPermit) =>
                          accountPermit.permit == item.id,
                    );
                  }
                },
              ),
              TwsSelectableList<Profile>(
                heigth: 300,
                title: "Available profiles", 
                adapter: _ProfileListAdapter(), 
                initialValues: _extractProfiles(set),
                tileTitle:(Profile set) => set.name,
                isEqual: (Profile item1, Profile item2) {
                  return item1.id == item2.id;
                },
                onSelect:(bool selected, Profile item) {
                  if(selected){
                    set.accountProfiles.add(
                      AccountProfile(
                        set.id, 
                        item.id, 
                        null,
                        null
                      ),
                    );
                  } else{
                    set.accountProfiles.removeWhere(
                      (AccountProfile accountProfile) =>
                          accountProfile.profile == item.id,
                    );
                  }
                },
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  Widget composeViewer(Account set, BuildContext context) {
    // Decoding the selected account password.
    _accountState.userPasswordInput = utf8.decode(base64Decode(set.password));
    // Prevent duplicated api calls on "Efective permits" component.
    if(_isShowing) consumerAgent.refresh();
    
    return SizedBox.expand(
      child: SingleChildScrollView(
        child: CSMSpacingColumn(
          spacing: 10,
          children: <Widget>[
            TWSPropertyViewer(
              label: "Identity", 
              value: set.user,
            ),
            TWSPropertyViewer(
              label: "Password", 
              value: _accountState.userPasswordInput,
            ),
            TWSPropertyViewer(
              label: "Name", 
              value: set.contactNavigation?.name ?? "---",
            ),
            TWSPropertyViewer(
              label: "Last name", 
              value: set.contactNavigation?.lastName ?? "---",
            ),
            TWSPropertyViewer(
              label: "Email", 
              value: set.contactNavigation?.email ?? "---",
            ),
            TWSPropertyViewer(
              label: "Phone number", 
              value: set.contactNavigation?.phone ?? "---",
            ),
            TWSPropertyViewer(
              label: "Wildcard", 
              value: set.wildcard.toString(),
            ),
            TwsListViewer<AccountPermit>(
              heigth: 250,
              title: "Permits",
              tilesContent: set.accountPermits,
              tileTitle: (AccountPermit set){
                if(set.permitNavigation?.featureNavigation != null){
                  return "${set.permitNavigation?.solutionNavigation?.name}: ${set.permitNavigation?.featureNavigation?.name} - ${set.permitNavigation?.actionNavigation?.name}";
                }
                return "---";
              },
            ),
            TwsListViewer<AccountProfile>(
              heigth: 250,
              title: "Profiles", 
              tilesContent: set.accountProfiles,
              tileTitle: (AccountProfile set){
                return set.profileNavigation?.name ?? "---";
              },
            ),
            TwsListViewer<Permit>(
              agent: consumerAgent,
              consume: () => permits(set),
              heigth: 250,
              title: "Efective Permits", 
              tileTitle: (Permit set){
                return "${set.solutionNavigation?.name}: ${set.featureNavigation?.name} - ${set.actionNavigation?.name}";
              },
            ),
          ],
        ),
      ),
    );
  }
}
