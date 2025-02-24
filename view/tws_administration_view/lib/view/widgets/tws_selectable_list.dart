import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/constants/twsa_colors.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field/tws_autocomplete_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_display_flat.dart';
import 'package:tws_administration_view/view/widgets/tws_list_tile.dart';
import 'package:tws_administration_view/view/widgets/tws_section.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

/// Header state class.
final class _HeaderState extends CSMStateBase{}

/// [TwsSelectableList] Display a list of selectable items getted from a [CSMConsumer] widget. 
class TwsSelectableList<T> extends StatefulWidget {
  /// Section title.
  final String title;
  /// Method to get the title from the [T] type object.
  final String Function(T set) tileTitle;
  /// List heigth.
  final double? heigth;
  /// Items padding
  final EdgeInsetsGeometry padding;
  /// Item background color.
  final Color? backgroundColor;
  /// Item text color.
  final Color? textColor;
  /// Title text alignment.
  final TextAlign titleAlignment;
  /// Subtitle text alignment.
  final TextAlign subtitleAlignment;
  /// Text to show when list content is empty.
  final String emptyContentMessage;
  /// Async data consume adapter.
  final TWSViewConsumeAdapter adapter;
  /// Preselected list values. This list is compared with consume list result and the coincidenses are marked has selected.
  final List<T>? initialValues;
  /// Trigger method on tile selection.
  final Function(bool selected, T item) onSelect;
  /// Custom header implementation.
  final Widget? customHeader;
  /// Custom comparation for [T] objects in [T] lists. If this field is empty, then the .compare list method will be used.
  final bool Function(T item1, T item2)? isEqual;
  /// Interaction status flag.
  final bool enabled;
  const TwsSelectableList({
    super.key,
    required this.title,
    required this.tileTitle,
    required this.adapter,
    required this.onSelect,
    this.heigth,
    this.customHeader,
    this.emptyContentMessage = "Empty content",
    this.padding = const EdgeInsets.all(5),
    this.textColor,
    this.backgroundColor,
    this.titleAlignment = TextAlign.left,
    this.subtitleAlignment = TextAlign.right,
    this.initialValues,
    this.isEqual,
    this.enabled = true,
  });

  @override
  State<TwsSelectableList<T>> createState() => _TwsSelectableListState<T>();
}

class _TwsSelectableListState<T> extends State<TwsSelectableList<T>> {
  /// Text color.
  late Color tcolor;
  /// Background color.
  late Color bcolor;
  /// Selected items list.
  late List<T> selectedItems;
  /// Data result in [CSMConsumer].
  late List<T> fetchedList;
  /// Color theme scheme.
  late TWSAThemeBase theme;
  /// Declaration for header state.
  late _HeaderState headerState;
  /// Header state effect holder for use outside the [CSMDynamicWidget].
  late void Function() headerEffect;
  /// Color pallet for the component.
  late CSMColorThemeOptions primaryColorTheme;
  late CSMColorThemeOptions pageColorTheme;

  // Theme method handler.
  void themeUpdateListener() {
    setState(() {
      theme = getTheme();
      primaryColorTheme = theme.primaryControlColor;
    });
  }

  @override
  void initState() {
    selectedItems = widget.initialValues ?? <T>[];
    headerState = _HeaderState();
    headerEffect = (){};

    theme = getTheme( 
      updateEfect: themeUpdateListener,
    );

    pageColorTheme = theme.page;
    primaryColorTheme = theme.primaryControlColor;
    tcolor = widget.textColor ?? pageColorTheme.fore;
    bcolor = widget.backgroundColor ?? pageColorTheme.main;
    super.initState();
  }

  @override
  void didUpdateWidget(covariant TwsSelectableList<T> oldWidget) {
    if(selectedItems != widget.initialValues && widget.enabled){
      selectedItems = widget.initialValues ?? selectedItems;
    }else if(!widget.enabled){
      selectedItems = <T>[];
    }
    super.didUpdateWidget(oldWidget);
  }
   
  @override
  Widget build(BuildContext context) {
    tcolor = widget.enabled? widget.textColor ?? pageColorTheme.fore : widget.textColor?.withValues(alpha: 50) ?? pageColorTheme.fore.withAlpha(50);
    return TWSSection(
      title: widget.title, 
      content: CSMConsumer<List<SetViewOut<dynamic>>>(
        consume: () => widget.adapter.consume(1, 9999, <SetViewOrderOptions>[], ""), 
        loadingBuilder: (BuildContext ctx) {
          return Center(
            child: CircularProgressIndicator(
              color: pageColorTheme.fore,
            ),
          );
        },
        errorBuilder:(__, Object? error, _) {
          return const TWSDisplayFlat(
            color: TWSAColors.oceanBlue,
            foreColor: TWSAColors.warmWhite,
            display: "Something go wrong",
          );
        },
        successBuilder:(BuildContext ctx, List<SetViewOut<dynamic>> data) {
          fetchedList = data.first.records as List<T>;
          return CSMSpacingColumn(
            spacing: 5,
            children: <Widget>[
              widget.customHeader != null
                  ? widget.customHeader!
                  : CSMDynamicWidget<_HeaderState>(
                      state: headerState,
                      designer: (BuildContext ctx, _HeaderState state) {
                        headerEffect = state.effect;
                        return CSMSpacingRow(
                          spacing: 10,
                          mainAlignment: MainAxisAlignment.spaceBetween,
                          children: <Widget>[
                            Text(
                              "Selected: ${selectedItems.length}",
                              style: TextStyle(
                                color: tcolor,
                              ),
                            ),
                            Text(
                              "${widget.title}: ${fetchedList.length.toString()}",
                              style: TextStyle(
                                color: tcolor,
                              ),
                            ),
                          ],
                        );
                      },
                    ),
              const Divider(),
              fetchedList.isNotEmpty? 
                SizedBox(
                  height: widget.heigth,
                  child: SingleChildScrollView(
                    child: Column(
                      children: List<Widget>.generate(fetchedList.length, (int index){
                        T item = fetchedList[index];
                        String title = widget.tileTitle(item);
                        return TwsListTile(
                          enabled: widget.enabled,
                          width: double.maxFinite,
                          label: title,
                          textColor: tcolor,
                          onHoverColor: pageColorTheme.highlight,
                          onHoverTextColor: pageColorTheme.fore,
                          onTap: (bool selected) {
                            if(selected){
                              selectedItems.add(item);
                            }else{
                              selectedItems.remove(item);
                            }
                            widget.onSelect(selected, item);
                            headerEffect();
                          },
                          evaluateSelection: () {
                            if(widget.isEqual != null){
                              bool founded = false;
                              for(T selectedItem in selectedItems){
                                if(widget.isEqual!(selectedItem, item)){
                                  founded = true;
                                  break;
                                }
                              }
                              return founded;
                            }
                            return selectedItems.contains(item);
                          },
                        );
                      }),
                    ),
                  ),
                )
              : TWSDisplayFlat(
                color: TWSAColors.oceanBlue,
                foreColor: TWSAColors.warmWhite,
                display: widget.emptyContentMessage,
              ),
            ],
          );
        },
      ),
    );
  }
}
