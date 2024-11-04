import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

/// [TWSArticleTable] adapter abstract interface, ensures a contract to determine an specific adapter behavior.
///
/// [TSet] the entity type the table will handle.
abstract class TWSArticleTableAdapter<TSet extends CSMEncodeInterface> {
  /// [TWSArticleTableAdapter] object constructor.
  const TWSArticleTableAdapter();

  /// method used for the table to reload and calculate its displayed data.
  ///
  /// [page] the current selected page.
  ///
  /// [range] the current selected records per page range.
  ///
  /// [orderings] self-handled table orderings.
  Future<SetViewOut<TSet>> consume(int page, int range, List<SetViewOrderOptions> orderings);

  /// method contract used to determine if the selected table row can display and build a detailed view section.
  ///
  /// [set] the [TSet] entity selected at the row.
  ///
  /// [context] table [BuildContext].
  Widget? composeViewer(TSet set, BuildContext context);

  /// method contract used to determine if the selected table row detailed view display can contain an editable section.
  ///
  /// [set] the [TSet] entity selected at the row.
  ///
  /// [closeReinvoke] function to invoke to close the detailed view section.
  ///
  /// [context] table [BuildContext]
  TWSArticleTableEditor? composeEditor(TSet set, void Function() closeReinvoke, BuildContext context);

  /// method contract used to determine if the selected table row detaied view display can contain a remotion section.
  ///
  /// [set] the [TSet] entity selected at the row.
  ///
  /// [context] table [BuildContext].
  bool onRemoveRequest(TSet set, BuildContext context) {
    return false;
  }
}
