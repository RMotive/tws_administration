import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/models/migration_view.dart';
import 'package:tws_administration/src/models/migration_view_options.dart';
import 'package:tws_administration/src/resolvers/main_resolver.dart';
import 'package:tws_administration/src/services/bases/solutions_service_base.dart';
import 'package:tws_administration/src/sets/solution.dart';

final class SolutionsService extends SolutionsServiceBase {
  SolutionsService(
    CSMUri host, {
    Client? client,
  }) : super(
          host,
          'Solutions',
          client: client,
        );

  @override
  Effect<MigrationView<Solution>> view(MigrationViewOptions options) async {
    CSMActEffect actEffect = await post('view', options);
    return MainResolver<MigrationView<Solution>>(actEffect);
  }
}
