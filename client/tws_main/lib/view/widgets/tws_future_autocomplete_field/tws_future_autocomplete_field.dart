import 'dart:async';

import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_service/tws_administration_service.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_display_flat.dart';
import 'package:tws_main/view/widgets/tws_future_autocomplete_field/tws_future_autocomplete_adapter.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';
import 'package:tws_main/view/widgets/tws_list_tile.dart';

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
  /// Set an initial value. Use ONLY for forms state management.
  final TSet? initialValue;
  /// Defines if the user can interact with the widget.
  final bool isEnabled;
  /// Set has non-required field, changing the border color when input is empty.
  final bool isOptional;
  /// Set the text to show when [IsOptional] is true.
  final String? isOptionalLabel;
  /// Optional Focus node to manage
  final FocusNode? focus;
  /// Variable that stores a [TWSFutureAutocompleteAdapter] class to consume the data. 
  final TWSFutureAutocompleteAdapter<TSet> adapter;
   /// Method that returns the value to show from [TSet] Object.
  final String Function(TSet) displayValue;
  /// Overlay menu height.
  final double menuHeight;
  /// Return the input text and the item property for last suggested item available based on the user input.
  final void Function(TSet? selection)  onChanged;
  /// Optinal validator method for [TWSInputText] internal component.
  final String? Function(String?)? validator;

  const TWSFutureAutoCompleteField({
    super.key,
    required this.onChanged,
    required this.displayValue,
    required this.adapter,
    this.isOptionalLabel,
    this.initialValue,
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
  late final CSMColorThemeOptions pageColorTheme;
  /// focus Node declaration.
  late final FocusNode focus;
  /// Link to attach ovelay component UI to the to the TWSInputText.
  final LayerLink link = LayerLink();
  /// Overlay controller.
  late OverlayPortalController overlayController;
  /// Main controller for the TWSInputText.
  late TextEditingController ctrl;
  /// Stores the current selected item.
  TSet? selectedOption;
  /// 
  TSet? previousSelection;
  /// Flag for Overlay Menu first build.
  bool overlayFirstBuild = true;
  /// Flag for componet first build.
  bool firstbuild = true;
  /// Original Options list given in builder parameter.
  List<TSet> rawOptionsList = <TSet>[];
  /// UI list. List to display on overlay menu, that shows the suggestions options.
  List<TSet> suggestionsList = <TSet>[];
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
  void _search(String input, bool notifyChange) {
    selectedOption = null;
    String query = input.toLowerCase();
    if(query.isNotEmpty){
      // Do a serching for the method input variable.
      suggestionsList = rawOptionsList.where((TSet set) {
        return widget.displayValue(set).toLowerCase().contains(query);
      }).toList();

      if(!firstbuild && suggestionsList.isNotEmpty && widget.displayValue(suggestionsList.first).toLowerCase() == query){
        selectedOption = suggestionsList.first;
        ctrl.text = input;
      }
    } else {
      // if the input is empty, then the default suggestions list is the original options list.
      suggestionsList = rawOptionsList;

    }
    print(previousSelection != selectedOption);
    if(!firstbuild && !overlayFirstBuild) {
      setState(() {
        if(previousSelection != selectedOption) widget.onChanged(selectedOption);
      });
    } 
    previousSelection = selectedOption;
    
  }

  @override
  void initState() {
    theme = getTheme(
      updateEfect: themeUpdateListener,
    );
    scrollController = ScrollController();
    primaryColorTheme = theme.primaryControlColor;
    pageColorTheme = theme.page;
    ctrl = TextEditingController();
    focus = widget.focus ?? FocusNode();
    overlayController = OverlayPortalController();
    consume = () => widget.adapter.consume(1,99999, <MigrationViewOrderOptions>[]);
    focus.addListener(focusManager);
    super.initState();
  }
  @override
  void didUpdateWidget(covariant TWSFutureAutoCompleteField<TSet> oldWidget) {
    super.didUpdateWidget(oldWidget);
    overlayFirstBuild = true;
    if(selectedOption != widget.initialValue) {
      setSelection();
    }
  }
  
  @override
  void dispose() {
    focus.dispose();
    scrollController.dispose();
    ctrl.dispose();
    super.dispose();
  }
  /// Method that manage and assign the [initialValue] property.
  void setSelection(){
    if(widget.initialValue != null){
      String value = widget.displayValue(widget.initialValue as TSet);
      _search(value, true);
    }else{
      ctrl.text = "";
      _search("", false);
    }
    
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
      overlayFirstBuild =false;
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
            suffixLabel: widget.isOptionalLabel,
            showErrorColor: (selectedOption == null && (!widget.isOptional || ctrl.text.isNotEmpty)) && !overlayFirstBuild,
            onChanged: (String text) => _search(text, true),
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
                        decoration: const BoxDecoration(
                          color: TWSAColors.ligthGrey,
                          borderRadius: BorderRadius.vertical(
                            bottom: Radius.circular(5)
                          ),
                          border: Border(
                            right: BorderSide(width: 2, color: TWSAColors.oceanBlue),
                            left: BorderSide(width: 2, color: TWSAColors.oceanBlue),
                            bottom: BorderSide(width: 2, color: TWSAColors.oceanBlue)
                          )
                        ),
                        child: CSMConsumer<MigrationView<TSet>>(
                          consume: consume,
                          delay: const Duration(milliseconds: 1000),
                          emptyCheck: (MigrationView<TSet> data) => data.sets.isEmpty,
                          loadingBuilder: (_) {
                            return Padding(
                              padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 30),
                              child: CircularProgressIndicator(
                                backgroundColor: TWSAColors.darkGrey,
                                color: highContrastColor,
                                strokeWidth: 4,
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
                              rawOptionsList = data.sets;
                              _search(ctrl.text, false);
                              firstbuild = false;
                              overlayFirstBuild = false;
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
                                    final TSet currentItem = suggestionsList[index];
                                    final String label = widget.displayValue(currentItem);
                                    // Build the individual option component.
                                    return TWSListTile(
                                      label: label,
                                      onHoverColor: primaryColorTheme.main,
                                      onHoverTextColor: pageColorTheme.fore,
                                      textColor: pageColorTheme.hightlightAlt ?? Colors.black,
                                      onTap: () {
                                        setState(() {
                                          _search(label, true);
                                          show = false;
                                        });
                                      },
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
                                const SizedBox(width: 5), 
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
