import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/widgets/tws_display_flat.dart';

class TWSIncrementalList<TModel> extends StatefulWidget {  
  /// text in plural to name the record.
  final String title;
  /// Max width component.
  final double width;
  /// Max heigth component.
  final double height;
  /// Build the default data model for each record.
  final TModel Function() modelBuilder;
  /// MEthod that returns a widget to build a new record for the [TModel] list.
  final Widget Function(TModel model, int index) recordBuilder;
  /// set record list to display in record listview, must be handled via [onAdd] and [onRemove] methods.
  final List<TModel> recordList;
  /// On add new record method.
  final void Function(TModel model) onAdd; 
  /// On remove last record method.
  final void Function() onRemove;
  /// set records creation limit. default value is 0 = no records limit.
  final int recordLimit;
  /// set the min records available. When the min value is reached, the delete option will be disable.
  final int recordMin;

  const TWSIncrementalList({
    super.key,
    required this.recordList,
    required this.modelBuilder,
    required this.onRemove,
    required this.onAdd,
    required this.recordBuilder,
    this.title = "Records",
    this.recordMin = 0,
    this.recordLimit = 0,
    this.height = 500,
    this.width = double.maxFinite,
  }): assert(recordLimit >= 0, "Limit property must be >= 0");

  @override
  State<TWSIncrementalList<TModel>> createState() => _TWSIncrementalListState<TModel>();
}

class _TWSIncrementalListState<TModel> extends State<TWSIncrementalList<TModel>> {
  late final TWSAThemeBase theme;
  // Color pallet for the component.
  late final CSMColorThemeOptions pageColorTheme;
  late final CSMColorThemeOptions primaryColorTheme;

  // Stores all the tmodel records.
  // late List<TModel> tModelList;

  void themeUpdateListener() {
    setState(() {
      theme = getTheme();
      pageColorTheme = theme.primaryCriticalControl;
      primaryColorTheme = theme.primaryControlColor;
    });
  }
  @override
  void initState() {
    // tModelList = widget.recordList;
    theme = getTheme(
      updateEfect: themeUpdateListener,
    );
    pageColorTheme = theme.primaryCriticalControl;
    primaryColorTheme = theme.primaryControlColor;
    super.initState();
  }
  

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: widget.width,
      child: CSMSpacingColumn(
        spacing: 1,
        mainSize: MainAxisSize.min,
        children: <Widget>[
          SizedBox(
            height: 50,
            child: CSMSpacingRow(
              spacing: 10,
              mainAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(
                  "${widget.title}: (${widget.recordList.length})",
                  style: TextStyle(
                    color: pageColorTheme.foreAlt
                  ),
                ),
                CSMSpacingRow(
                  spacing: 10,
                  children: <Widget>[
                    // --> Add option
                    CSMPointerHandler(
                      cursor: SystemMouseCursors.click,
                      onClick: (){
                        // check if record limit has been reached.
                        if(widget.recordLimit != 0 &&  widget.recordList.length >=  widget.recordLimit) return;
                        setState(() {
                          widget.onAdd(widget.modelBuilder());
                        });
                      }, 
                      child: Icon(
                        size: 24,
                        Icons.add_circle,
                        color: pageColorTheme.foreAlt,
                      ),
                    ),
                    // --> Remove option
                    CSMPointerHandler(
                      cursor: SystemMouseCursors.click,
                      onClick: (){
                        if(widget.recordList.isEmpty || widget.recordList.length == widget.recordMin) return;
                        setState(() {
                          widget.onRemove();
                        });   
                      }, 
                      child: Icon(
                        size: 24,
                        Icons.remove_circle,
                        color: pageColorTheme.highlight,
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
          // --> show message when tmodelList is empty.
          Visibility(
            visible: widget.recordList.isEmpty,
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: TWSDisplayFlat(
                display: "No ${widget.title} added",
              ),
            ),
          ),
    
          // --> show TModel listview
          Padding(
            padding: const EdgeInsets.only(left: 10, right: 10, bottom: 20),
            child: ConstrainedBox(
              constraints: BoxConstraints(
                maxHeight: widget.height
              ),
              child: ListView.builder(
                shrinkWrap: true,
                prototypeItem: widget.recordBuilder(widget.modelBuilder(), 0),
                itemCount: widget.recordList.length,
                itemBuilder:(BuildContext context, int index) {
                  return widget.recordBuilder(widget.recordList[index], index);
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}