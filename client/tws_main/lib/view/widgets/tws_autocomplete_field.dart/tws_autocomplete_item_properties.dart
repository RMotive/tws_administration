
/// [TWSAutocompleteItemProperties] Class for [TWSAutoCompleteField]  Component.
/// This class stores the relevant data for the options in the suggestions overlay menu.
final class TWSAutocompleteItemProperties<TValue>{
  /// value to display on UI component.
  final String label;
  /// Stores relevant values for data processing.
  final TValue value;

  TWSAutocompleteItemProperties({
    required this.label, 
    required this.value
  });
}