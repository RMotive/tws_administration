import 'package:tws_main/view/widgets/tws_article_creation/interfaces/tws_article_creation_property_interface.dart';

/// Defines options to indicate a property group at the Article Creation Form this works for several purposes
/// for example in the form will be displayed a section group indicating the properties belongs to a specific grouup,
/// same on the display items section, will display a charm around the group properties.
final class TWSArticleCreationPropertyGroupOptions implements TWSArticleCreationPropertyInterface {
  final String title;
  final List<TWSArticleCreationPropertyInterface> properties;

  const TWSArticleCreationPropertyGroupOptions({
    required this.title,
    required this.properties,
  }) : assert(properties.length > 0, 'A property group must contain at least a property');
}
