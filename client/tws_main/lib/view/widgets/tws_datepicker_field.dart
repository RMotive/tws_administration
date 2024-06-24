
import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/extension/datetime.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';
/// [TWSDatepicker] Custom component for TWS Environment.
/// This component shows a datepicker dialog to select a date.
class TWSDatepicker extends StatefulWidget {
  /// [width] Text field width.
  final double? width;
  /// [height] Text field heigth.
  final double? height;
  /// [title] Text field title.
  final String? title;
  /// [hintText] Text field hintext.
  final String? hintText;
  /// [focusNode] Optional focus node.
  final FocusNode? focusNode;
  /// [controller] Optional Text controller.
  final TextEditingController? controller;
  /// [onChanged] Callback that return the selected options in the datepicker dialog.
  final void Function(String text)? onChanged;
  /// [validator] Validator for the text input.
  final String? Function(String? text)? validator;

  const TWSDatepicker({super.key,
    this.width,
    this.height,
    this.title,
    this.hintText,
    this.focusNode,
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
    ctrl = widget.controller ?? TextEditingController();
    fNode = widget.focusNode ?? FocusNode();
    theme = getTheme();
    colorStruct = theme.primaryControlColor;
    disabledColorStruct = theme.primaryDisabledControlColor;
    errorColorStruct = theme.primaryErrorControlColor;
    super.initState();
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
        onChanged:widget.onChanged,
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
          labelText: widget.title,
          hintText: widget.hintText,
          isDense: true,
            labelStyle: TextStyle(
              color: colorStruct.foreAlt,
            ),
            errorStyle: TextStyle(
              color: errorColorStruct.fore,
            ),
            hintStyle: TextStyle(
              color: colorStruct.fore,
            ),
            enabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: colorStruct.highlight.withOpacity(.6),
                width: borderWidth,
              ),
            ),
            errorBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: errorColorStruct.highlight.withOpacity(.7),
                width: borderWidth,
              ),
            ),
            focusedErrorBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: errorColorStruct.highlight,
                width: borderWidth,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: colorStruct.highlight,
                width: borderWidth,
              ),
            ),
          ),
        ),
      );
  }
  /// [_showPicker] Method that build the showpicker dialog.
  Future<void> _showPicker() async {
    DateTime? date = await showDatePicker(
      context: context,
      initialDatePickerMode: DatePickerMode.year,
      initialDate: DateTime.now(),
      firstDate: DateTime(2000), 
      lastDate: DateTime(2030),
      builder: (BuildContext context, Widget? child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: ColorScheme.dark(
              surface: colorStruct.main, //Background color
              primary: colorStruct.fore, // header background color
              onPrimary: colorStruct.foreAlt ?? Colors.white, // header text color
              onSurface: colorStruct.foreAlt ?? Colors.white, // body text color
            ),
            textButtonTheme: TextButtonThemeData(
              style: TextButton.styleFrom(
                foregroundColor: colorStruct.foreAlt ?? Colors.white, // button text color
              ),
            ),
          ),
          child: child!,
        );
      },
    );
    if(date != null) {
      setState(() {
        ctrl.text = date.getStringDate();
      });
    }
  }
}