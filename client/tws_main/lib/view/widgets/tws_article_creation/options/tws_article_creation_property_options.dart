import 'dart:async';

import 'package:tws_main/view/widgets/tws_article_creation/interfaces/tws_article_creation_property_interface.dart';

/// Defines options for a property at the Article Creation form, this indicates the behavior and
/// controls to be shown for this property.
///
/// TValue: !IMPORTANT! This reference only can be of type currently supported:
/// - [String]
/// - [int]
/// - [double]
/// - [DateTime]
///
/// NOTE:
/// -> Note: If you give [int] but don't give [options]/[optionsLoader] will be displayed a [int] selector control
/// but if you set it, will be displayed an options selector box where will be displayed the [String] as
/// label and the [int] will be sent as the value in [reactor]
final class TWSArticleCreationPropertyOptions<TModel, TValue> implements TWSArticleCreationPropertyInterface {
  final String label;
  final bool display;
  final List<(int, String)>? options;
  final FutureOr<List<(int, String)>>? optionsLoader;
  final String Function(TValue value)? validator;
  final TModel Function(TModel model, TValue value) reactor;

  const TWSArticleCreationPropertyOptions({
    this.options,
    this.optionsLoader,
    this.validator,
    this.display = false,
    required this.label,
    required this.reactor,
  }) : assert((options != null) != (optionsLoader != null) || ((options != null) == (optionsLoader != null)), 'Only can be set [options] or [optionsLoader] but not both');
}
