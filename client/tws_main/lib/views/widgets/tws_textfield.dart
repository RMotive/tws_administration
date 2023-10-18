import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

class TWSTextField extends StatelessWidget {
  final Size? controlSize;
  final EdgeInsets? padding;
  final String? textfieldLabel;
  final bool isSecret;
  final List<String>? autofillHints;
  final TextEditingController? editorController;

  const TWSTextField({
    super.key,
    this.controlSize,
    this.padding,
    this.textfieldLabel,
    this.autofillHints,
    this.editorController,
    this.isSecret = false,
  });

  @override
  Widget build(BuildContext context) {
    ThemeBase theme = getTheme();

    return Padding(
      padding: padding ?? const EdgeInsets.all(0),
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          maxWidth: 250,
        ),
        child: SizedBox(
          width: controlSize?.width,
          height: controlSize?.height,
          child: Material(
            color: Colors.transparent,
            child: Tooltip(
              message: 'Textfield to input $textfieldLabel',
              child: TextField(
                cursorColor: Colors.black,
                cursorWidth: 3,
                textAlign: TextAlign.center,
                obscureText: isSecret,
                autofillHints: autofillHints,
                style: const TextStyle(),
                controller: editorController,
                decoration: InputDecoration(
                  filled: true,
                  fillColor: theme.onPrimaryColorFirstControlColor,
                  labelText: textfieldLabel,
                  contentPadding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                  labelStyle: const TextStyle(),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
