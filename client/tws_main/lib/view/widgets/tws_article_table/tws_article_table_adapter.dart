import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table.dart';

abstract interface class TWSArticleTableAdapter<TSet extends CSMEncodeInterface> {
  Future<MigrationView<TSet>> consume(int page, int range, List<MigrationViewOrderOptions> orderings);

  TWSArticleTableEditor? composeEditor(TSet set, void Function() closeReinvoke, BuildContext context);
  Widget composeViewer(TSet set, BuildContext context);
  void onRemoveRequest(TSet set, BuildContext context);
}
