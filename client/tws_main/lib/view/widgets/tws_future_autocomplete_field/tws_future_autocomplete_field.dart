import 'dart:async';

import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_autocomplete_field.dart/tws_autocomplete_item_properties.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';

/// [TWSFutureAutoCompleteField] Custom component for TWS enviroment.
/// This component stores a list of posibles options to select for the user.
/// Performs a input filter based on user input text.
/// The Data is fetched throw Future async methods, ideal to API consume.
class TWSFutureAutoCompleteField<TSet extends CSMEncodeInterface> extends StatefulWidget {
  /// Field width.
  final double width;
  /// Field height.
  final double height;
  /// Main title for the field.
  final String? label;
  /// Placeholder Text.
  final String? hint;
  /// Defines if the user can interact with the widget.
  final bool isEnabled;
  /// Set has non-required field.
  final bool isOptional;
  /// Optional Focus node to manage
  final FocusNode? focus;
  /// Variable that stores a [TWSFutureAutocompleteAdapter] class to consume the data. 
  final TWSFutureAutocompleteAdapter<TSet> adapter;
  /// Overlay menu height.
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
    required this.adapter,
    this.isOptional = false,
    this.focus,
    this.label,
    this.hint,
    this.width = 150,
    this.height = 40,
    this.menuHeight = 200,
    this.validator,
    this.isEnabled = true
  });

  @override
  State<TWSFutureAutoCompleteField<TSet>> createState() => _TWSAutoCompleteFieldState<TSet>();
}

class _TWSAutoCompleteFieldState<TSet extends CSMEncodeInterface> extends State<TWSFutureAutoCompleteField<TSet>> with SingleTickerProviderStateMixin{
  late final TWSAThemeBase theme;
  /// Consume method declaration in [adapter] property.
  late final Future<MigrationView<TSet>> Function() consume;
  /// Internal scroll controller for overlay scrolling.
  late final ScrollController scrollController; 
  /// Color pallet for the component.
  late final CSMColorThemeOptions primaryColorTheme;
  /// focus Node declaration.
  late final FocusNode focus;
  /// Link to attach ovelay component UI to the to the TWSInputText.
  final LayerLink link = LayerLink();
  /// Overlay controller.
  late OverlayPortalController overlayController;
  /// Main controller for the TWSInputText.
  late TextEditingController ctrl;
  /// Stores the current first suggestion for the user input.
  TWSAutocompleteItemProperties<TSet>? selectedOption;
  /// Flag for Overlay Menu first build.
  bool firstbuild = true;
  /// Original Options list given in builder parameter.
  List<TWSAutocompleteItemProperties<TSet>> rawOptionsList = <TWSAutocompleteItemProperties<TSet>>[];
  /// UI list. List to display on overlay menu, that shows the suggestions options.
  List<TWSAutocompleteItemProperties<TSet>> suggestionsList = <TWSAutocompleteItemProperties<TSet>>[];

  /// Flag to indicate if the overlay component is hovered.
  bool isOvelayHovered = false;
  /// Defines if the overlay is showing.
  bool show = false;
  
  /// Methoth that verify if the [TWSTextField] component has a valid input selection.
  bool _verifySelection() {
    if(selectedOption != null) return true;
    return false;
  }

  /// Method to filter the Original options list based on user input.
  void _search(String input) {
    selectedOption = null;
    String query = input.toLowerCase();
    if(query.isNotEmpty) {
      // Do a serching for the method input variable.
      suggestionsList = rawOptionsList.where((TWSAutocompleteItemProperties<TSet> properties) {
        return properties.label.toLowerCase().contains(query);
      }).toList();

      if(!firstbuild && suggestionsList.isNotEmpty && suggestionsList.first.label.toLowerCase() == query){
        selectedOption = suggestionsList.first;
        widget.onChanged(input, selectedOption);
      }
    } else {
      // if the input is empty, then the default suggestions list is the original options list.
      suggestionsList = rawOptionsList;
    }   
    if(!firstbuild) setState(() {});
  }

  @override
  void initState() {
    theme = getTheme(
      updateEfect: themeUpdateListener,
    );
    scrollController = ScrollController();
    primaryColorTheme = theme.primaryControlColor;
    ctrl = TextEditingController();
    focus = widget.focus ?? FocusNode();
    overlayController = OverlayPortalController();
    consume = () => widget.adapter.consume(1,99999, <MigrationViewOrderOptions>[]);
    focus.addListener(focusManager);
    super.initState();
  }

  @override
  void dispose() {
    focus.dispose();
    scrollController.dispose();
    ctrl.dispose();
    super.dispose();
  }
  void initOptions(){
    
  }
  void themeUpdateListener() {
    setState(() {
      theme = getTheme();
      primaryColorTheme = theme.primaryControlColor;
    });
  }
  /// Method that manage the focus events to show or hide the overlay
  void focusManager(){
    if(focus.hasPrimaryFocus){
      if(firstbuild) overlayController.show();
      setState(() => show = true);
    }else{
      setState(() => show = false);
    }
  }
  
  @override
  Widget build(BuildContext context) {
    final Color highContrastColor = primaryColorTheme.hightlightAlt ?? Colors.white;
    const double tileHeigth = 35;
    return SizedBox(
      width: widget.width,
      child: OverlayPortal(
        controller: overlayController,
        child: CompositedTransformTarget(
          link: link,
          child: TWSInputText( 
            controller: ctrl,
            isOptional: widget.isOptional,
            width: widget.width,
            height: widget.height,
            showErrorColor: selectedOption == null && !firstbuild,
            onChanged: (String text) => _search(text),
            onTap: () => setState(() => show = true),
            onTapOutside: (_) {
              if(!isOvelayHovered) setState(() => show = false);
              if(!show) focus.unfocus();
            },
            focusNode: focus,
            label: widget.label,
            hint: widget.hint,
            isEnabled: widget.isEnabled,
            validator: (String? text) {
              if (_verifySelection()) return "Not exist an item with this value.";
              if (widget.validator != null) return widget.validator!(text);
              return null;
            },           
          )
        ),
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
              // Overlay pointer handler
              child: CSMPointerHandler(
                onHover:(bool hover) => isOvelayHovered = hover,
                child: Visibility(
                  visible: show,
                  maintainState: true,
                  child: ConstrainedBox(
                    constraints: BoxConstraints(
                      maxHeight: widget.menuHeight
                    ),
                    child: ClipRRect(
                      child: DecoratedBox(
                        decoration: BoxDecoration(
                          borderRadius: const BorderRadius.vertical(
                            bottom: Radius.circular(5)
                          ),
                          color: primaryColorTheme.main
                        ),
                        child: CSMConsumer<MigrationView<TSet>>(
                          consume: consume,
                          delay: const Duration(milliseconds: 1000),
                          emptyCheck: (MigrationView<TSet> data) => data.sets.isEmpty,
                          loadingBuilder: (_) {
                            return Padding(
                              padding: const EdgeInsets.all(10),
                              child: SizedBox(
                                width: 50,
                                height: 50,
                                child: CircularProgressIndicator(
                                  backgroundColor: primaryColorTheme.main,
                                  color: highContrastColor,
                                  strokeWidth: 3,
                                ),
                              ),
                            );
                          },
                          errorBuilder: (BuildContext ctx, Object? error, MigrationView<TSet>? data) {
                            return const Padding(
                              padding: EdgeInsets.all(10),
                              child: TWSDisplayFlat(
                                display: 'Error loading data',
                              ),
                            );
                          },
                          successBuilder: (BuildContext ctx, MigrationView<TSet> data) {  
                            //Only do the builder callback once.
                            //Stores the properties result to avoid unnecesary callbacks on rebuild.
                            if(firstbuild){
                              for(int i = 0; i < data.sets.length-1; i++){
                                late TWSAutocompleteItemProperties<TSet> properties;
                                properties = widget.optionsBuilder(i, data.sets[i]);
                                rawOptionsList.add(properties);
                              }
                              _search(ctrl.text);
                              firstbuild = false;
                            }

                            return suggestionsList.isNotEmpty? Scrollbar(
                              trackVisibility: true,
                              thumbVisibility: true,
                              controller: scrollController,
                              child: Material(
                                color: Colors.transparent,
                                child: ListView.builder(
                                  shrinkWrap: true,
                                  itemExtent: 35,
                                  controller: scrollController,
                                  itemCount: suggestionsList.length,
                                  itemBuilder: (_, int index) {
                                    final TWSAutocompleteItemProperties<TSet> currentItem = suggestionsList[index];
                                    // Build the individual option component.
                                    return ListTile(
                                      hoverColor: primaryColorTheme.fore,
                                      dense: true,
                                      title: Text(
                                        softWrap: false,
                                        currentItem.label,
                                        style: TextStyle(
                                          color: highContrastColor,
                                        ),
                                      ),
                                      onTap: () {
                                        setState(() {
                                          // Set the controller text to selected item on overlay menu.
                                          ctrl.text = currentItem.label;
                                          // Do a search query because the controller.text not trigger the OnChange callback.
                                          _search(currentItem.label);
                                          show = false;
                                          // return selected option.
                                          selectedOption = currentItem; 
                                          widget.onChanged(ctrl.text, selectedOption);
                                        });
                                       
                                      }
                                    );   
                                  }
                                ),
                              )
                          ) : SizedBox(
                            height: tileHeigth,
                            child: Row(
                              mainAxisSize: MainAxisSize.min,
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: <Widget>[
                                Icon(
                                  Icons.error_outline,
                                  color: highContrastColor,
                                ),
                                const SizedBox(
                                  width: 5,
                                ), 
                                Flexible(
                                  child: Text(
                                    "Not matches found",
                                    style: TextStyle(
                                      overflow: TextOverflow.ellipsis,
                                      color: highContrastColor
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          );
                        }
                      )
                    )
                  )
                ),
              ),
            )
          )
        );
      }
    )
  );
}}
