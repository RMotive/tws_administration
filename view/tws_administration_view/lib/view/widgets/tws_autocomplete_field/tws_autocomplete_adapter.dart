import 'package:tws_foundation_client/tws_foundation_client.dart';

abstract interface class TWSAutocompleteAdapter{
  Future<List<SetViewOutput<dynamic>>> consume(int page, int range, List<SetViewOrderOptions> orderings);
}
