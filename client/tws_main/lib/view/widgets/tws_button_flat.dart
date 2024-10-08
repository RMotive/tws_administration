import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

typedef StatesSet = Set<WidgetState>;
typedef MStates = WidgetState;

class TWSButtonFlat extends StatelessWidget {
  final double? width;
  final double? height;
  final String label;
  final bool showLoading;
  final CSMColorThemeOptions? themeOptions;
  final Function() onTap;

  const TWSButtonFlat({
    super.key,
    this.width,
    this.themeOptions,
    this.height = 40,
    this.label = 'Hello!',
    this.showLoading = false,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions colorStruct = themeOptions ?? getTheme<TWSAThemeBase>().primaryControlColor;

    Color bgStateColorize(StatesSet currentStates) {
      final Color hlightColor = colorStruct.highlight;
      final Color reducedColor = hlightColor.withOpacity(.6);
      return switch (currentStates) {
        (StatesSet state) when state.contains(MStates.hovered) => hlightColor,
        _ => reducedColor,
      };
    }

    Color olStateColorize(StatesSet currentStates) {
      final Color hlightColor = colorStruct.hightlightAlt ?? Colors.blue.shade900;
      return switch (currentStates) {
        (StatesSet state) when state.contains(MStates.pressed) => hlightColor,
        _ => Colors.transparent,
      };
    }

    return SizedBox(
      width: width,
      height: height,
      child: TextButton(
        style: ButtonStyle(
          enableFeedback: true,
          backgroundColor: WidgetStateColor.resolveWith(bgStateColorize),
          overlayColor: WidgetStateColor.resolveWith(olStateColorize),
          shape: WidgetStateProperty.all(const LinearBorder()),
        ),
        onPressed: showLoading ? null : onTap,
        child: Visibility(
          visible: !showLoading,
          replacement: Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: 24,
            ),
            child: LinearProgressIndicator(
              backgroundColor: Colors.transparent,
              color: colorStruct.foreAlt,
            ),
          ),
          child: Text(
            label,
            style: TextStyle(
              color: colorStruct.foreAlt,
            ),
          ),
        ),
      ),
    );
  }
}
