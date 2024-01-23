part of './login_page.dart';

class _LoginForm extends StatefulWidget {
  const _LoginForm();

  @override
  State<_LoginForm> createState() => _LoginFormState();
}

class _LoginFormState extends State<_LoginForm> {
  late final TWSASecurityServiceBase service;

  @override
  void initState() {
    super.initState();
    service = twsaRepo.securityService;
  }

  void logIn() {
    print('service loaded ${service.endpoint.generateUri()}');
  }

  @override
  Widget build(BuildContext context) {
    const double controlsWidth = 275;

    return CosmosSeparatedColumn(
      crossAxisAlignment: CrossAxisAlignment.center,
      spacing: 24,
      children: <Widget>[
        const TWSInputText(
          label: 'Identity',
          hint: 'Your solution identity 🧑‍⚕️',
          width: controlsWidth,
        ),
        const TWSInputText(
          label: 'Password',
          hint: 'Your secret word 🔐',
          width: controlsWidth,
        ),
        TWSButtonFlat(
          width: controlsWidth,
          onTap: logIn,
        ),
      ],
    );
  }
}
