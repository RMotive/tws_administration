import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/theme_base.dart';

/// TWS Business dedicated component
///
/// This component builds a TWS Design opinioned component for a text input control.
///
/// TWS Theme Base, this component uses primaryControlColorStruct
class TWSInputText extends StatelessWidget {
  final String? label;
  final String? hint;
  final double? width;
  final double? height;
  final String? errorText;
  final TextEditingController? controller;

  const TWSInputText({
    super.key,
    this.label,
    this.hint,
    this.errorText,
    this.width,
    this.height = 50,
    this.controller,
  });

  @override
  Widget build(BuildContext context) {
    final TextEditingController ctrl = controller ?? TextEditingController();
    final ThemeColorStruct colorStruct = getTheme<ThemeBase>().primaryControlColorStruct;

    return Material(
      color: Colors.transparent,
      child: SizedBox(
        height: height,
        width: width,
        child: TextField(
          autofocus: true,
          controller: ctrl,
          cursorOpacityAnimates: true,
          cursorWidth: 3,
          cursorColor: colorStruct.onColorAlt,
          style: TextStyle(
            color: colorStruct.onColorAlt,
          ),
          decoration: InputDecoration(
            hintText: hint,
            labelText: label,
            errorText: errorText,
            isDense: true,
            labelStyle: TextStyle(
              color: colorStruct.onColorAlt,
            ),
            hintStyle: TextStyle(
              color: colorStruct.onColor,
            ),
            enabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: colorStruct.hlightColor.withOpacity(.6),
                width: 2,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: colorStruct.hlightColor,
                width: 2,
              ),
            ),
          ),
        ),
      ),
    );
  }
}
