import 'package:tws_administration_service/tws_administration_service.dart';

abstract interface class TWSArticleTableDataAdapter<TSet extends CSMEncodeInterface> {
  Future<MigrationView<TSet>> consume(int page, int range, List<MigrationViewOrderOptions> orderings);
}
