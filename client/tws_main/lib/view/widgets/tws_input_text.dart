import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

/// TWS Business dedicated component
///
/// This component builds a TWS Design opinioned component for a text input control.
///
/// TWS Theme Base, this component uses primaryControlColorStruct
class TWSInputText extends StatefulWidget {
  final String? label;
  final String? hint;
  final double? width;
  final double? height;
  final String? errorText;
  final bool isPrivate;
  final bool isEnabled;
  final int? maxLength;
  final int? maxLines;
  final void Function()? OnTap;
  final FocusNode? focusNode;
  final TextEditingController? controller;
  final String? Function(String? text)? validator;
  final void Function(String text)? onChanged;
  final Function(PointerDownEvent)? onTapOutside;
  const TWSInputText({
    super.key,
    this.label,
    this.hint,
    this.errorText,
    this.width,
    this.validator,
    this.height,
    this.maxLength,
    this.controller,
    this.focusNode,
    this.onChanged,
    this.OnTap,
    this.onTapOutside,
    this.maxLines = 1,
    this.isEnabled = true,
    this.isPrivate = false,
  });

  @override
  State<TWSInputText> createState() => _TWSInputTextState();
}

class _TWSInputTextState extends State<TWSInputText> {
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
  void didUpdateWidget(covariant TWSInputText oldWidget) {
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
    return Material(
      color: Colors.transparent,
      child: SizedBox(
        height: widget.height,
        width: widget.width,
        child: TextFormField(
          autofocus: true,
          validator: widget.validator,
          obscureText: widget.isPrivate,
          controller: ctrl,
          focusNode: fNode,
          cursorOpacityAnimates: true,
          cursorWidth: 3,
          cursorColor: colorStruct.foreAlt,
          enabled: widget.isEnabled,
          onChanged: widget.onChanged,
          onTap: widget.OnTap,
          onTapOutside: widget.onTapOutside,
          maxLength: widget.maxLength,
          maxLines: widget.maxLines,
          style: TextStyle(
            color: colorStruct.foreAlt?.withOpacity(.7),
          ),
          decoration: InputDecoration(
            hintText: widget.hint,
            labelText: widget.label,
            errorText: widget.errorText,
            isDense: true,
            counterStyle: TextStyle(
              color: (ctrl.text.length < (widget.maxLength ?? 0)) ? errorColorStruct.fore : Colors.green,
            ),
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
            disabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: disabledColorStruct.highlight,
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
      ),
    );
  }
}
