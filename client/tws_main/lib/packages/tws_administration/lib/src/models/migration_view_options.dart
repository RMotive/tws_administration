import 'package:csm_foundation_services/csm_foundation_services.dart';
import 'package:tws_administration/src/models/models_module.dart';
import 'package:tws_administration/tws_administration.dart';

final class MigrationViewOptions implements CSMEncodeInterface {
  final bool retroactive;
  final int range;
  final int page;
  final DateTime? creation;
  final List<MigrationViewOrderOptions> orderings;

  const MigrationViewOptions(this.creation, this.orderings, this.page, this.range, this.retroactive);

  @override
  JObject encode() {
    return <String, dynamic>{
      'retroactive': retroactive,
      'range': range,
      'page': page,
      'creation': creation,
      'orderings': orderings.map((MigrationViewOrderOptions i) => i.encode()).toList(),
    };
  }
}
