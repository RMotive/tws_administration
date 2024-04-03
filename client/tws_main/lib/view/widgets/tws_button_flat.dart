import 'package:cosmos_foundation/theme/theme_module.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

typedef StatesSet = Set<MaterialState>;
typedef MStates = MaterialState;

class TWSButtonFlat extends StatelessWidget {
  final double? width;
  final double? height;
  final String label;
  final bool showLoading;
  final Function() onTap;

  const TWSButtonFlat({
    super.key,
    this.width,
    this.height = 40,
    this.label = 'Hello!',
    this.showLoading = false,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions colorStruct = getTheme<TWSAThemeBase>().primaryControlColorStruct;

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
          backgroundColor: MaterialStateColor.resolveWith(bgStateColorize),
          overlayColor: MaterialStateColor.resolveWith(olStateColorize),
          shape: MaterialStateProperty.all(const LinearBorder()),
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
