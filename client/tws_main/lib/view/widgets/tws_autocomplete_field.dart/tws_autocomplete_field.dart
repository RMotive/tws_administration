import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

/// [TWSAutoCompleteField] Custom component for TWS enviroment.
/// This component stores a list of posibles options to select for the user.
/// Performs a input filter based on user input text. 
class TWSAutoCompleteField extends StatefulWidget {
  /// [width] Button width on non-selected state.
  final double width;
  /// [height] Button heigth.
  final double height;
  /// [menuWidth] Overlay menu Width. Main button will expand to this size when is selected.
  final double menuWidth;
  /// [menuHeight] Overlay menu height.
  final double menuHeight;
  /// [optionsBuilder] Iterarive builder that recieve an [TWSAutocompleteItemProperties] object to build the list options 
  /// components for the suggestions overlay menu.
  final TWSAutocompleteItemProperties Function(int) optionsBuilder;
  /// [listLength] length for the expected [TWSAutocompleteItemProperties] list.
  final int listLength;
  /// [onChanged] Return the input text and the item property for last suggested item available based on the user input.
  final void Function(String input, TWSAutocompleteItemProperties? selectedItem) onChanged;
  /// [validator] Optinal validator method for [TWSInputText] internal component.
  final String? Function(String?)? validator;

  const TWSAutoCompleteField({super.key,
    required this.onChanged,
    required this.optionsBuilder,
    required this.listLength,
    this.width = 80,
    this.height = 40,
    this.menuWidth = 220,
    this.menuHeight = 200,
    this.validator,
  });

  @override
  State<TWSAutoCompleteField> createState() => _TWSAutoCompleteFieldState();
}

class _TWSAutoCompleteFieldState extends State<TWSAutoCompleteField> {
  final TWSAThemeBase theme = getTheme();
  late CSMColorThemeOptions primaryColorTheme;
  // Link to attach ovelay component UI to the to the TWSInputText.
  final LayerLink link = LayerLink();
  late OverlayPortalController overlayController;
  // Main controller for the TWSInputText.
  late TextEditingController ctrl;
  // Stores the current first suggestion for the user input.
  TWSAutocompleteItemProperties? firstSuggestion;
  // Flag for Overlay Menu first build.
  bool firstbuild = true;
  // Original Options list given in builder parameter.
  List<TWSAutocompleteItemProperties> rawOptionsList =
      <TWSAutocompleteItemProperties>[];
  // UI list. List to display on overlay menu, that shows the suggestions options.
  List<TWSAutocompleteItemProperties> suggestionsList =
      <TWSAutocompleteItemProperties>[];
  // Flag to indicate if the component is hovered.
  bool isHovered = false;

  ///[_verifyQuery] methoth that verify if the [TWSTextField]
  bool _verifyQuery(String suggest) {
    if (ctrl.text == suggest && ctrl.text.isNotEmpty) return true;
    return false;
  }

  /// [_search] Method to filter the Original options list based on user input.
  void _search(String input) {
    setState(() {
      if (input.isNotEmpty) {
        // Do a serching for the method input variable.
        suggestionsList =
            rawOptionsList.where((TWSAutocompleteItemProperties properties) {
          return properties.showValue.contains(input);
        }).toList();
        // Validate if the searching results is not empty.
        if (suggestionsList.isNotEmpty) {
          firstSuggestion = suggestionsList.first;
        } else {
          firstSuggestion = null;
        }
      } else {
        // if the input is empty, then the default suggestions list is the original options list.
        suggestionsList = rawOptionsList;
      }
      // Return on callback method, the search first result and the input text.
      widget.onChanged(input, firstSuggestion);
    });
  }

  @override
  void initState() {
    primaryColorTheme = theme.primaryControlColor;
    ctrl = TextEditingController();
    overlayController = OverlayPortalController();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: widget.width,
      child: MouseRegion(
        onEnter: (PointerEnterEvent event) => isHovered = true,
        onExit: (PointerExitEvent event) => isHovered = false,
        // Overlay Menu
        child: OverlayPortal(
          overlayChildBuilder: (_) {
            // Getting the visual boundries for this component.
            final RenderBox renderBox = context.findRenderObject() as RenderBox;
            final Size renderSize = renderBox.size;
            return Positioned(
              width: renderSize.width,
              child: CompositedTransformFollower(
                showWhenUnlinked: false,
                offset: Offset(0, renderSize.height),
                link: link,
                child: TapRegion(
                  onTapOutside: (_) {
                    // check if the tap event occurs outside the widget component.
                    if (!isHovered) {
                      setState(() {
                        overlayController.hide();
                      });
                    }
                  },
                  child: ConstrainedBox(
                    constraints: const BoxConstraints(
                      maxHeight: 200,
                    ),
                    child: ClipRRect(
                      child: DecoratedBox(
                        decoration: BoxDecoration(
                          borderRadius: const BorderRadius.vertical(bottom: Radius.circular(5)),
                          color: primaryColorTheme.main
                        ),
                        child: Material(
                          color: Colors.transparent,
                          child: ListView.builder(
                              shrinkWrap: true,
                              itemCount: firstbuild
                                  ? widget.listLength
                                  : suggestionsList.length,
                              itemBuilder: (_, int index) {
                                late TWSAutocompleteItemProperties properties;
                                //Only do the builder callback once.
                                //Stores the properties result to avoid unnecesary callbacks on rebuild.
                                if (firstbuild) {
                                  properties = widget.optionsBuilder(index);
                                  rawOptionsList.add(properties);
                                } else {
                                  properties = suggestionsList[index];
                                }
                                //Set the firstbuild to false and assign the default suggestion list value.
                                if (index >= widget.listLength - 1 && firstbuild) {
                                  firstbuild = false;
                                  suggestionsList = rawOptionsList;
                                }
                                // Return the individual option component.
                                return ListTile(
                                  hoverColor: Colors.blue,
                                  dense: true,
                                  title: Tooltip(
                                    message: properties.showValue,
                                    child: Text(
                                      style: TextStyle(
                                        color: primaryColorTheme.hightlightAlt ?? Colors.white
                                      ),
                                      overflow: TextOverflow.ellipsis,
                                      properties.showValue
                                    ),
                                  ),
                                  onTap: () {
                                    // Set the controller text to selected item on overlay menu.
                                    ctrl.text = properties.showValue;
                                    // Do a search query because the controller.text not trigger the OnChange callback.
                                    _search(properties.showValue);
                                    overlayController.hide();
                                  },
                                );
                              }),
                        ),
                      ),
                    ),
                  ),
                ),
              ),
            );
          },
          controller: overlayController,
          // [TapRegion] Catch header user button [OnTap] events.
          child: TapRegion(
              onTapInside: (_) {
                if (!overlayController.isShowing) {
                  setState(() {
                    overlayController.show();
                  });
                }
              },
              child: CompositedTransformTarget(
                  link: link,
                  child: TWSInputText(
                      validator: (String? text) {
                        if (_verifyQuery(firstSuggestion?.showValue ?? "")) return "Not exist an item with this value.";
                        if (widget.validator != null) return widget.validator!(text);
                        return null;
                      },
                      onChanged: (String text) => _search(text),
                      onTapOutside: (_) {},
                      controller: ctrl,
                      width: widget.width,
                      height: widget.height))),
        ),
      ),
    );
  }
}
