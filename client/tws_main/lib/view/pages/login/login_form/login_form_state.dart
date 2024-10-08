part of '../login_page.dart';

/// A class that handles and notifies state changes when is needed.
///
/// State classes handles all behaviors and elements that are used by te UI but aren't explicit
/// UI.
///
/// Handles state for [_LoginForm]
class _LoginFormState extends CSMStateBase {
  //* --> UI Resources -->
  /// Specifies the max amount of width that all controls in the form will take.
  final double maxControlsWidth = 275;

  /// Service to handle local session storage.
  //final SessionStorage _sessionStorage = SessionStorage.instance;

  /// Service to handle route driving.
  // final CSMRouter _router = CSMRouter.i;

  /// Service to handle [Security] operations with the business network services provider.
  //final TWSASecurityServiceBase _service = twsaSource.security;

  /// Specific key for handling the state of the form.
  final GlobalKey<FormState> formKey = GlobalKey();

  /// Stores a control controllers struct to manage [Identity] control behavior
  final CSMInputHandlerOptions identityControl = CSMInputHandlerOptions.auto('login-form-identity-control');

  /// Stores a control controllers struct to manage [Password] control behavior
  final CSMInputHandlerOptions passwordControl = CSMInputHandlerOptions.auto('login-form-password-control');

  //* --> UI States <-- Properties used to indicate states
  /// Stores wheter the form is requesting to remote service.
  bool _isRequesting = false;

  /// Indicates if the service is requesting on remote service.
  bool get isRequesting => _isRequesting;

  /// Stores the general failure display
  ///
  /// This is showed up above the inputs in a red rectangle.
  String _failureDisplay = "";

  /// Stores the current failure to display if there is.
  String get failureDisplay => _failureDisplay;

  /// Stores an identity failure related to the entity input control, this is retrieved after the service returns a failure
  /// with the status code 1, is retrieved from the [Situation] structure in the property [display]
  String? _identityFailure;

  /// Stores the current failure related to the identity input control.
  String? get identityFailure => _identityFailure;

  /// Stores a password failure related to the entity input control, this is retrieved after the service returns a failure
  /// with the status code 2, is retrieved from the [Situation] structure in the property [display]
  String? _passwordFailure;

  /// Stores the current failure related to the password input control
  String? get passwordFailure => _passwordFailure;

  //* --> State management methods. --> Methods use to handle the form state.
  /// (Render) This method changes the state to start a remote service request.
  ///
  /// It sets the [_isRequesting] and cleans up all the user feedback (failures displays).
  void _startRequest() {
    _isRequesting = true;
    _failureDisplay = "";
    _identityFailure = null;
    _passwordFailure = null;
    effect();
  }

  /// (Render) Indicates the UI that the request performed was finished and renders the state changes.
  void _finishRequest() {
    _isRequesting = false;
    effect();
  }

  /// (Render) Indicates the UI to display a failure related with state management and ends the request.
  void _wrongStateFailure() {
    _isRequesting = false;
    _failureDisplay = TWSAMessages.kStateManagementErrorDisplay;
    effect();
  }

  //* --> UI METHODS --> Methods used directly by the UI.
  /// UI Method.
  /// Used to validate if the identity input is valid
  String? validateIdentityInput(String? content) {
    content ??= "";
    if (content.isEmpty) return TWSAMessages.kEmptyInputDisplay;
    return null;
  }

  /// UI Method.
  /// Used to validate if the password input.
  String? validatePasswordInput(String? content) {
    content ??= "";
    if (content.isEmpty) return TWSAMessages.kEmptyInputDisplay;
    return null;
  }

  /// UI Method.
  /// This UI method is triggered when the user taps to confirm and send the [_LoginForm].
  /// Communicates with the [Security] network business server service and returns feedback to the user.
  void initSession() async {
    _startRequest(); // <-- Indicating UI to feedback user of a remote service call
    final bool? formValidationResult = formKey.currentState?.validate(); // <-- Validating the form.
    if (formValidationResult == null) return _wrongStateFailure();
    if (!formValidationResult) return _finishRequest();

    _finishRequest();
  }
}
