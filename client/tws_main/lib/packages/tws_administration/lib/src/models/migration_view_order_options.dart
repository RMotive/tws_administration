import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/enums/migration_view_order_behaviors.dart';

final class MigrationViewOrderOptions implements CSMEncodeInterface {
  final String property;
  final MigrationViewOrderBehaviors behavior;

  const MigrationViewOrderOptions(this.behavior, this.property);

  @override
  JObject encode() {
    return <String, dynamic>{
      'property': property,
      'behavior': behavior,
    };
  }
}
