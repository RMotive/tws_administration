import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/constants/twsa_colors.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_main/view/widgets/tws_input_text.dart';
import 'package:tws_main/view/widgets/tws_list_tile.dart';

/// [TWSAutoCompleteField] Custom component for TWS enviroment.
/// This component stores a list of posibles options to select for the user.
/// Performs a input filter based on user input text.
class TWSAutoCompleteField<TSet> extends StatefulWidget {
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
  /// Optional Focus node to manage.
  final FocusNode? focus;
  /// Overlay menu height.
  final double menuHeight;
  /// Assign a default value.
  final TSet? initialValue;
  /// Method that returns the value to show from [TSet] Object.
  final String Function(TSet) displayValue;
  /// Builder that receive a search String and return a list for the query results. 
  /// this results are used for build the suggestions overlay menu.
  final List<TSet> Function(String query) optionsBuilder;
  /// Return the selected item based on the user input.
  final void Function(TSet? selectedItem) onChanged;
  /// Optinal validator method for [TWSInputText] internal component.
  final String? Function(String?)? validator;

  const TWSAutoCompleteField({
    super.key,
    required this.onChanged,
    required this.optionsBuilder,
    required this.displayValue,
    this.isOptional = false,
    this.initialValue,
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
  State<TWSAutoCompleteField<TSet>> createState() => _TWSAutoCompleteFieldState<TSet>();
}

class _TWSAutoCompleteFieldState<TSet> extends State<TWSAutoCompleteField<TSet>> with SingleTickerProviderStateMixin{
  late final TWSAThemeBase theme;
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
  /// Stores the current first suggestion for the user input.
  TSet? selectedOption;
  /// Flag for Overlay Menu first build.
  bool overlayFirstBuild = true;
  /// Flag for Overlay Menu first build.
  bool firstbuild = true;
  /// Original Options list given in builder parameter.
  List<TSet> rawOptionsList = <TSet>[];
  /// UI list. List to display on overlay menu, that shows the suggestions options.
  List<TSet> suggestionsList = <TSet>[];
  /// Flag to indicate if the overlay component is hovered.
  bool isOvelayHovered = false;
  /// Defines if the overlay is showing.
  bool show = false;
  
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
    focus.addListener(focusManager);
    setSelection();
    super.initState();
  }

  @override
  void dispose() {
    focus.dispose();
    scrollController.dispose();
    ctrl.dispose();
    super.dispose();
  }
  @override
  void didUpdateWidget(covariant TWSAutoCompleteField<TSet> oldWidget) {
    super.didUpdateWidget(oldWidget);
    WidgetsBinding.instance.addPostFrameCallback((_) => setSelection());
  }
  /// Method that manage and assign the [initialValue] property.
  void setSelection(){
    if(widget.initialValue != null){
      String value = widget.displayValue(widget.initialValue as TSet);
      _search(value, firstbuild);
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
      if(firstbuild){
        overlayController.show(); 
        firstbuild = false;
      } 
      setState(() => show = true);
    }else{
      setState(() => show = false);
    }
  }
  
  /// Methoth that verify if the [TWSTextField] component has a valid input selection.
  bool _verifySelection() {
    if(selectedOption != null) return true;
    return false;
  }

  /// Method that receive the matching options list from the [OptionBuilder] & the string input.
  void _search(String input, bool notifyChange) {
    setState(() {
      final String query = input.toLowerCase(); 
      selectedOption = null;
      suggestionsList = widget.optionsBuilder(query);
      if(suggestionsList.isNotEmpty && query == widget.displayValue(suggestionsList.first).toLowerCase()){
        selectedOption = suggestionsList.first;
        ctrl.text = input;
      }
      if(notifyChange) widget.onChanged(selectedOption);
    });
  }

  @override
  Widget build(BuildContext context) {
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
            showErrorColor: (selectedOption == null && (!widget.isOptional || ctrl.text.isNotEmpty)) && !firstbuild,
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
                        child: suggestionsList.isNotEmpty? Scrollbar(
                          trackVisibility: true,
                          thumbVisibility: true,
                          controller: scrollController,
                          child: Material(
                            color: Colors.transparent,
                            child: ListView.builder(
                              shrinkWrap: true,
                              itemExtent: tileHeigth,
                              controller: scrollController,
                              itemCount: suggestionsList.length,
                              itemBuilder: (_, int index) {
                                late TSet item = suggestionsList[index];
                                final String displayValue = widget.displayValue(item);
                                // Build the individual option component.
                                return TWSListTile(
                                label: displayValue,
                                onHoverColor: primaryColorTheme.main,
                                onHoverTextColor: pageColorTheme.fore,
                                textColor: pageColorTheme.hightlightAlt ?? TWSAColors.lightDark,
                                onTap: () {
                                  _search(displayValue, true);
                                  setState(() => show = false);
                                },
                              );
                            }),
                          )
                      ) : SizedBox(
                        height: tileHeigth,
                        child: Row(
                          mainAxisSize: MainAxisSize.min,
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: <Widget>[
                            Icon(
                              Icons.error_outline,
                              color:  pageColorTheme.hightlightAlt ?? Colors.black,
                            ),
                            const SizedBox(
                              width: 5,
                            ), 
                            Flexible(
                              child: Text(
                                "Not matches found",
                                style: TextStyle(
                                  overflow: TextOverflow.ellipsis,
                                  color:  pageColorTheme.hightlightAlt ?? Colors.black
                                ),
                              ),
                            ),
                          ],
                        ),
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
