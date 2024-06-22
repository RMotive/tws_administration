import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';

abstract interface class TWSArticleTableAdapter<TSet extends CSMEncodeInterface> {
  Future<MigrationView<TSet>> consume(int page, int range, List<MigrationViewOrderOptions> orderings);

  Widget? composeEditor(TSet set, BuildContext context);
  Widget composeViewer(TSet set, BuildContext context);
  void onRemoveRequest(TSet set, BuildContext context);
}
