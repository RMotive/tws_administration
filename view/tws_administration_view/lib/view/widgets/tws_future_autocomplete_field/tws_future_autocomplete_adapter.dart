import 'package:tws_foundation_client/tws_foundation_client.dart';

abstract interface class TWSFutureAutocompleteAdapter<TSet extends CSMEncodeInterface>{
  Future<MigrationView<TSet>> consume(int page, int range, List<MigrationViewOrderOptions> orderings);
}