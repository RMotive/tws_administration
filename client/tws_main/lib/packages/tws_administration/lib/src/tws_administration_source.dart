import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/services/bases/solutions_service_base.dart';
import 'package:tws_administration/src/services/solutions_service.dart';

/// Source that exposes the configured services dependencies for each
/// requirement, can be configured but if not, will use the default ones.
final class TWSAdministrationSource extends CSMSourceBase {
  // --> Services

  /// Solutions service.
  late final SolutionsServiceBase solutions;

  /// Generates a new data source building its internal
  /// services.
  TWSAdministrationSource(
    bool debug, {
    Client? client,
    SolutionsServiceBase? solutions,
  }) : super(
          debug,
          const CSMUri(
            '192.168.100.35',
            '',
            port: 5196,
            protocol: CSMProtocols.http,
          ),
        ) {
    this.solutions = solutions ?? SolutionsService(host, client: client);
  }
}
