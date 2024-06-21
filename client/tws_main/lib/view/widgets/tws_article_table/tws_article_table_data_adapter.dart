import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';

abstract interface class TWSArticleTableDataAdapter<TSet extends CSMEncodeInterface> {
  Future<MigrationView<TSet>> consume(int page, int range, List<MigrationViewOrderOptions> orderings);

  Widget? composeViewer(TSet set, BuildContext context) => null;
  Widget? composeEditor(TSet set, BuildContext context) => null;
  void onRemoveRequest(TSet set, BuildContext context) {}
}
