import 'package:cosmos_foundation/widgets/hooks/themed_widget.dart';
import 'package:flutter/material.dart';

class TWSTextField extends StatelessWidget {
  final Size? controlSize;
  final EdgeInsets? padding;
  final String? textfieldLabel;
  final bool isSecret;
  final List<String>? autofillHints;

  const TWSTextField({
    super.key,
    this.controlSize,
    this.padding,
    this.textfieldLabel,
    this.autofillHints,
    this.isSecret = false,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: padding ?? const EdgeInsets.all(0),
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          maxWidth: 250,
        ),
        child: ThemedWidget(
          builder: (BuildContext context, ThemeData theme) {
            return SizedBox(
              width: controlSize?.width,
              height: controlSize?.height,
              child: Material(
                child: Tooltip(
                  message: 'Textfield to input $textfieldLabel',
                  child: TextField(
                    cursorColor: theme.colorScheme.onSecondary,
                    cursorWidth: 3,
                    textAlign: TextAlign.center,
                    obscureText: isSecret,
                    autofillHints: autofillHints,
                    style: theme.primaryTextTheme.bodyMedium?.copyWith(
                      color: theme.colorScheme.onSecondary,
                      fontWeight: FontWeight.w900,
                    ),
                    decoration: InputDecoration(
                      filled: true,
                      fillColor: theme.colorScheme.secondary,
                      labelText: textfieldLabel,
                      contentPadding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                      labelStyle: theme.primaryTextTheme.bodyMedium?.copyWith(
                        color: theme.colorScheme.onSecondary,
                        fontStyle: FontStyle.italic,
                      ),
                    ),
                  ),
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}
