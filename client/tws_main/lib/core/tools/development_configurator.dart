import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/secrets/development_secrets.dart';
import 'package:tws_main/data/services/sources.dart';
import 'package:tws_main/data/storages/session_storage.dart';

class DevelopmentConfigurator {

  static Future<void> configure() async {
    await _configureAccount();
  }

  static Future<void> _configureAccount() async {
    SessionStorage sessionStorage = SessionStorage.i;
    if (sessionStorage.isSession) return;

    final Credentials credentials = Credentials(DevelopmentSecrets.identity, DevelopmentSecrets.password);
    final MainResolver<Privileges> service = await administration.security.authenticate(credentials).timeout(8.seconds);
    Privileges privileges = await service.act(PrivilegesDecode());
    sessionStorage.storeSession(privileges);
  }
}
