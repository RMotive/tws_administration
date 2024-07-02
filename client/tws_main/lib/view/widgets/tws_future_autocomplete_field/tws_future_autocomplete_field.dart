import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_article_table/tws_article_table_data_adapter.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

/// [TWSFutureAutoCompleteField] Custom component for TWS enviroment.
/// This component stores a list of posibles options to select for the user.
/// Performs a input filter based on user input text.
class TWSFutureAutoCompleteField<TSet extends CSMEncodeInterface> extends StatefulWidget {
  final double width;
  final double height;
  final String? label;
  final String? hint;
  final bool isEnabled;
  ///
  final Future<MigrationView<TSet>> consume;
  // final TWSArticleTableDataAdapter<TSet> adapter;
  /// Overlay menu Width. Main button will expand to this size when is selected.
  final double menuWidth;
  /// [menuHeight] Overlay menu height.
  final double menuHeight;

  /// Iterarive builder that recieve an [TWSFautocompleteItemProperties] object to build the list options
  /// components for the suggestions overlay menu.
  final TWSAutocompleteItemProperties<TSet> Function(int, TSet) optionsBuilder;

  /// Return the input text and the item property for last suggested item available based on the user input.
  final void Function(String input, TWSAutocompleteItemProperties<TSet>? selectedItem) onChanged;

  /// Optinal validator method for [TWSInputText] internal component.
  final String? Function(String?)? validator;

  const TWSFutureAutoCompleteField({
    super.key,
    required this.onChanged,
    required this.optionsBuilder,
    required this.consume,
    this.label,
    this.hint,
    this.width = 80,
    this.height = 40,
    this.menuWidth = 220,
    this.menuHeight = 200,
    this.validator,
    this.isEnabled = true
  });

  @override
  State<TWSFutureAutoCompleteField<TSet>> createState() => _TWSAutoCompleteFieldState<TSet>();
}

class _TWSAutoCompleteFieldState<TSet extends CSMEncodeInterface> extends State<TWSFutureAutoCompleteField<TSet>> with SingleTickerProviderStateMixin{
  late final TWSAThemeBase theme;
  late final Future<MigrationView<TSet>> Function() consume;
  late final TWSArticleTableDataAdapter<TSet> adapter;

  late CSMColorThemeOptions primaryColorTheme;
  // Link to attach ovelay component UI to the to the TWSInputText.
  final LayerLink link = LayerLink();
  late OverlayPortalController overlayController;
  // Main controller for the TWSInputText.
  late TextEditingController ctrl;
  // Stores the current first suggestion for the user input.
  TWSAutocompleteItemProperties<TSet>? firstSuggestion;
  // Flag for Overlay Menu first build.
  bool firstbuild = true;
  // Original Options list given in builder parameter.
  List<TWSAutocompleteItemProperties<TSet>> rawOptionsList = <TWSAutocompleteItemProperties<TSet>>[];
  // UI list. List to display on overlay menu, that shows the suggestions options.
  List<TWSAutocompleteItemProperties<TSet>> suggestionsList = <TWSAutocompleteItemProperties<TSet>>[];
  // Flag to indicate if the component is hovered.
  bool isHovered = false;

  /// methoth that verify if the [TWSTextField]
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
            rawOptionsList.where((TWSAutocompleteItemProperties<TSet> properties) {
          return properties.label.contains(input);
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
    theme = getTheme(
      updateEfect: themeUpdateListener,
    );
    primaryColorTheme = theme.primaryControlColor;
    ctrl = TextEditingController();
    overlayController = OverlayPortalController();
    super.initState();
  }

  void themeUpdateListener() {
    setState(() {
      theme = getTheme();
      primaryColorTheme = theme.primaryControlColor;
    });
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
                      maxHeight: 200
                    ),
                    child: ClipRRect(
                      child: DecoratedBox(
                        decoration: BoxDecoration(
                          borderRadius: const BorderRadius.vertical(
                            bottom: Radius.circular(5)
                          ),
                          color: primaryColorTheme.main
                        ),
                        child: Material(
                          color: Colors.transparent,
                          child: CSMConsumer<MigrationView<TSet>>(
                            consume: consume,
                            successBuilder: (BuildContext ctx, MigrationView<TSet> data) {  
                              return ListView.builder(
                              shrinkWrap: true,
                              itemCount: firstbuild
                                ? data.sets.length
                                : suggestionsList.length,
                              itemBuilder: (_, int index) {
                                late TWSAutocompleteItemProperties<TSet> properties;
                                //Only do the builder callback once.
                                //Stores the properties result to avoid unnecesary callbacks on rebuild.
                                if (firstbuild) {
                                  properties = widget.optionsBuilder(index, data.sets[index]);
                                  rawOptionsList.add(properties);
                                } else {
                                  properties = suggestionsList[index];
                                }
                                //Set the firstbuild to false and assign the default suggestion list value.
                                if (index >= data.sets.length - 1 && firstbuild) {
                                  firstbuild = false;
                                  suggestionsList = rawOptionsList;
                                }
                                // Return the individual option component.
                                return ListTile(
                                  hoverColor: primaryColorTheme.main,
                                  dense: true,
                                  title: Tooltip(
                                    message: properties.label,
                                    child: Text(
                                      style: TextStyle(
                                        color: primaryColorTheme .hightlightAlt ?? Colors.white
                                      ),
                                      overflow: TextOverflow.ellipsis,
                                      properties.label
                                    )
                                  ),
                                  onTap: () {
                                    // Set the controller text to selected item on overlay menu.
                                    ctrl.text = properties.label;
                                    // Do a search query because the controller.text not trigger the OnChange callback.
                                    _search(properties.label);
                                    overlayController.hide();
                                  }
                                );
                              }
                            );
                            },
                            
                          )
                        )
                      )
                    )
                  )
                )
              )
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
                label: widget.label,
                hint: widget.hint,
                isEnabled: widget.isEnabled,
                validator: (String? text) {
                  if (_verifyQuery(firstSuggestion?.label ?? "")) return "Not exist an item with this value.";
                  if (widget.validator != null) return widget.validator!(text);
                  return null;
                },
                onChanged: (String text) => _search(text),
                controller: ctrl,
                width: widget.width,
                height: widget.height
              )
            )
          )
        )
      )
    );
  }
}
