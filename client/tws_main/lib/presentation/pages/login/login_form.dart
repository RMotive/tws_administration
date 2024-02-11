part of './login_page.dart';

class _LoginForm extends StatefulWidget {
  const _LoginForm();

  @override
  State<_LoginForm> createState() => _LoginFormState();
}

class _LoginFormState extends State<_LoginForm> {
  final String kInputErrorDisplay = "Can`t be empty";
  final String kServerErrorDisplay = "Can't reach server 🛠️";
  final String kRequestFailureDisplay = "Wrong server answer, please contact support. 🛠️";

  late final GlobalKey<FormState> formKey;
  late final FocusNode ityFocusNode;
  late final TextEditingController ityCtrl;
  late final TextEditingController pwdCtrl;
  late final TWSASecurityServiceBase service;

  // --> States
  late bool communicating;
  late String? errorCard;

  @override
  void initState() {
    super.initState();
    ityCtrl = TextEditingController();
    pwdCtrl = TextEditingController();
    ityFocusNode = FocusNode();
    communicating = false;
    errorCard = null;
    formKey = GlobalKey<FormState>();
    service = twsaRepo.securityService;
  }

  @override
  void dispose() {
    ityFocusNode.dispose();
    ityCtrl.dispose();
    pwdCtrl.dispose();
    super.dispose();
  }

  String? validator(String? value) {
    if (value == null || value.isEmpty) return kInputErrorDisplay;
    return null;
  }

  void initSession() async {
    if (!formKey.currentState!.validate()) return;
    setState(() {
      communicating = true;
      errorCard = null;
    });

    Uint8List password = Uint8List.fromList(pwdCtrl.text.codeUnits);
    AccountIdentityModel account = AccountIdentityModel(ityCtrl.text, password);
    ServiceResult<ForeignSessionModel> sResult = await service.initSession(account);
    sResult.resolve(
      (ForeignSessionModel success) {},
      (JObject failure, int statusCode) {
        setState(() {
          errorCard = kRequestFailureDisplay;
        });
      },
      (Object exception, StackTrace trace) {
        setState(() {
          errorCard = kServerErrorDisplay;
        });
        Focuser.focus(ityFocusNode);
      },
    );
    setState(() {
      communicating = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    const double controlsWidth = 275;

    return Form(
      key: formKey,
      child: CosmosSeparatedColumn(
        crossAxisAlignment: CrossAxisAlignment.center,
        spacing: 24,
        children: <Widget>[
          Visibility(
            visible: errorCard != null,
            child: TwsDisplayFlat(
              width: controlsWidth - 32,
              maxHeight: 100,
              display: errorCard ?? '',
            ),
          ),
          TWSInputText(
            label: 'Identity',
            hint: 'Your solution identity 🧑‍⚕️',
            isEnabled: !communicating,
            controller: ityCtrl,
            focusNode: ityFocusNode,
            validator: validator,
            width: controlsWidth,
          ),
          TWSInputText(
            label: 'Password',
            hint: 'Your secret word 🔐',
            isPrivate: true,
            controller: pwdCtrl,
            isEnabled: !communicating,
            validator: validator,
            width: controlsWidth,
          ),
          TWSButtonFlat(
            width: controlsWidth,
            showLoading: communicating,
            onTap: initSession,
          ),
        ],
      ),
    );
  }
}
