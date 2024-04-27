import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/resolvers/main_resolver.dart';
import 'package:tws_administration/tws_administration.dart';

typedef Effect<TEstela extends CSMEncodeInterface> = Future<MainResolver<TEstela>>;
typedef MResolver<TEstela extends CSMEncodeInterface> = MainResolver<TEstela>;

abstract class SolutionsServiceBase extends CSMServiceBase {
  SolutionsServiceBase(
    super.host,
    super.servicePath, {
    super.client,
  });

  /// Transaction to generate a set view object.
  Effect<MigrationView<Solution>> view(MigrationViewOptions options);
}
