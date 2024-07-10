
import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/extension/datetime.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
/// [TWSDatepicker] Custom component for TWS Environment.
/// This component shows a datepicker dialog to select a date.
class TWSDatepicker extends StatefulWidget {
  /// First selectable date.
  final DateTime firstDate;
  /// Last selectable date.
  final DateTime lastDate;
  /// Text field width.
  final double width;
  /// Text field heigth.
  final double height;
  /// Text field title.
  final String? label;
  /// Text field hintext.
  final String? hintText;
  /// Optional focus node.
  final FocusNode? focusNode;
  /// Default pre-selected Date.
  final DateTime? initialDate;
  /// Optional Text controller.
  final TextEditingController? controller;
  /// Defines if the user can interact with the widget.
  final bool isEnabled;
  /// Callback that return the selected options in the datepicker dialog.
  final void Function(String text)? onChanged;
  /// Validator for the text input.
  final String? Function(String? text)? validator;

  const TWSDatepicker({super.key,
    required this.firstDate,
    required this.lastDate,
    this.initialDate,
    this.width = 200,
    this.height = 40,
    this.label,
    this.hintText,
    this.focusNode,
    this.isEnabled = true,
    this.controller,
    this.onChanged,
    this.validator
  });
  
  @override
  State<TWSDatepicker> createState() => _TWSDatepickerState();
}

class _TWSDatepickerState extends State<TWSDatepicker> {
  final double borderWidth = 2;
  late TextEditingController ctrl;
  late final FocusNode fNode;
  late final TWSAThemeBase theme;
  late final CSMColorThemeOptions colorStruct;
  late final CSMColorThemeOptions disabledColorStruct;
  late final CSMColorThemeOptions errorColorStruct;

  @override
  void initState() {
    super.initState();
    ctrl = widget.controller ?? TextEditingController();
    fNode = widget.focusNode ?? FocusNode();
    theme = getTheme(
      updateEfect: themeUpdateListener,
    );
    initializeThemes();
    ctrl.addListener(() => setState(() {}));
  }
  
  @override
  void didUpdateWidget(covariant TWSDatepicker oldWidget) {
    super.didUpdateWidget(oldWidget);

    if (widget.controller != oldWidget.controller) {
      ctrl = widget.controller ?? TextEditingController();
    }

    ctrl.addListener(() => setState(() {}));
  }
  void initializeThemes() {
    colorStruct = theme.primaryControlColor;
    disabledColorStruct = theme.primaryDisabledControlColor;
    errorColorStruct = theme.primaryErrorControlColor;
  }

  void themeUpdateListener() {
    setState(() {
      theme = getTheme();
      initializeThemes();
    });
  }
  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: widget.height,
      width: widget.width,
      child: TextFormField(
        autofocus: true,
        readOnly: true,
        validator: widget.validator,
        controller: ctrl,
        focusNode: fNode,
        cursorOpacityAnimates: true,
        cursorWidth: 3,
        cursorColor: colorStruct.foreAlt,
        style: TextStyle(
          color: colorStruct.foreAlt?.withOpacity(.7),
        ),
        onTap: () => _showPicker(),
        decoration: InputDecoration(
          suffixIcon: const Icon(Icons.calendar_month),
          suffixIconColor: colorStruct.main,
          prefixIcon: ctrl.text.isNotEmpty? IconButton(
            tooltip: "Delete selection",
            icon:Icon(
              color: errorColorStruct.highlight,
              Icons.cancel
            ), 
            onPressed: () { 
              // update the widget state to hide the delete button.
              setState(() {
                ctrl.text = "";
              });
            },
          ): null,
          labelText: widget.label,
          hintText: widget.hintText,
          isDense: true,
          labelStyle: TextStyle(
            color: colorStruct.foreAlt
          ),
          errorStyle: TextStyle(
            color: errorColorStruct.fore
          ),
          hintStyle: TextStyle(
            color: colorStruct.fore
          ),
          enabledBorder: OutlineInputBorder(
            borderSide: BorderSide(
              color: colorStruct.highlight.withOpacity(.6),
              width: borderWidth
            ),
          ),
          errorBorder: OutlineInputBorder(
            borderSide: BorderSide(
              color: errorColorStruct.highlight.withOpacity(.7),
              width: borderWidth
            ),
          ),
          focusedErrorBorder: OutlineInputBorder(
            borderSide: BorderSide(
              color: errorColorStruct.highlight,
              width: borderWidth
            ),
          ),
          focusedBorder: OutlineInputBorder(
            borderSide: BorderSide(
              color: colorStruct.highlight,
              width: borderWidth
            )
          )
        )
      )
    );
  }
  /// [_showPicker] Method that build the showpicker dialog.
  Future<void> _showPicker() async {
    DateTime? date = await showDatePicker(
      context: context,
      initialDatePickerMode: DatePickerMode.year,
      initialDate: widget.initialDate,
      firstDate: widget.firstDate, 
      lastDate: widget.lastDate,
      builder: (BuildContext context, Widget? child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: ColorScheme.dark(
              surface: colorStruct.main, //Background color
              primary: colorStruct.fore, // header background color
              onPrimary: colorStruct.foreAlt ?? Colors.white, // header text color
              onSurface: colorStruct.foreAlt ?? Colors.white // body text color
            ),
            textButtonTheme: TextButtonThemeData(
              style: TextButton.styleFrom(
                foregroundColor: colorStruct.foreAlt ?? Colors.white // button text color
              )
            )
          ),
          child: child!
        );
      }
    );
    if(date != null) {
      setState(() {
        ctrl.text = date.dateOnlyString;
        if(widget.onChanged != null) widget.onChanged!(ctrl.text);
      });
    }
  }
}