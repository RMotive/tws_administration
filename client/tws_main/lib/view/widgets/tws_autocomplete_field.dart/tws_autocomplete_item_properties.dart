
/// [TWSAutocompleteItemProperties] Class for [TWSAutoCompleteField]  Component.
/// This class stores the relevant data for the options in the suggestions overlay menu.
final class TWSAutocompleteItemProperties{
  /// [showValue] value to display on UI component.
  final String showValue;
  /// [returnValue] Stores relevant values for data processing.
  final String returnValue;

  TWSAutocompleteItemProperties({
    required this.showValue, 
    required this.returnValue
  });
}