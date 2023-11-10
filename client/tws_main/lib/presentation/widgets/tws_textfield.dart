import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

class TWSTextField extends StatefulWidget {
  final Size? controlSize;
  final EdgeInsets? padding;
  final String? textfieldLabel;
  final bool isSecret;
  final String toolTip;
  final List<String>? autofillHints;
  final TextEditingController? editorController;
  final String? errorHint;
  final void Function()? onWritting;

  const TWSTextField({
    super.key,
    this.controlSize,
    this.padding,
    this.textfieldLabel,
    this.autofillHints,
    this.editorController,
    this.errorHint,
    this.onWritting,
    this.toolTip = "",
    this.isSecret = false,
  });

  @override
  State<TWSTextField> createState() => _TWSTextFieldState();
}

class _TWSTextFieldState extends State<TWSTextField> {
  // --> Init resources
  late ThemeBase theme;
  late final FocusNode focusHandler;

  @override
  void initState() {
    super.initState();
    theme = getTheme();
    listenTheme.addListener(onThemeUpdate);
    focusHandler = FocusNode();
    focusHandler.addListener(
      () {
        if (focusHandler.hasFocus) widget.onWritting?.call();
      },
    );
  }

  @override
  void dispose() {
    focusHandler.dispose();
    listenTheme.removeListener(onThemeUpdate);
    super.dispose();
  }

  void onThemeUpdate() {
    if (mounted) setState(() => theme = getTheme());
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: widget.padding ?? const EdgeInsets.all(0),
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          maxWidth: 250,
        ),
        child: SizedBox(
          width: widget.controlSize?.width,
          height: widget.controlSize?.height,
          child: Material(
            color: Colors.transparent,
            child: Tooltip(
              message: widget.toolTip,
              child: TextField(
                cursorOpacityAnimates: true,
                cursorColor: Colors.black,
                cursorWidth: 3,
                textAlign: TextAlign.center,
                obscureText: widget.isSecret,
                autofillHints: widget.autofillHints,
                style: const TextStyle(),
                focusNode: focusHandler,
                controller: widget.editorController,
                decoration: InputDecoration(
                  filled: true,
                  fillColor: theme.onPrimaryColorFirstControlColor.mainColor,
                  labelText: widget.textfieldLabel,
                  contentPadding: const EdgeInsets.symmetric(
                    horizontal: 8,
                    vertical: 4,
                  ),
                  labelStyle: const TextStyle(),
                  errorText: widget.errorHint,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
