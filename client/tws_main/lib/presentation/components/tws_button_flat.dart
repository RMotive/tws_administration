import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/theme_base.dart';

typedef StatesSet = Set<MaterialState>;
typedef MStates = MaterialState;

class TWSButtonFlat extends StatelessWidget {
  final double? width;
  final double? height;
  final String label;
  final Function() onTap;

  const TWSButtonFlat({
    super.key,
    this.width,
    this.height = 40,
    this.label = 'Hello!',
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final ThemeColorStruct colorStruct = getTheme<ThemeBase>().primaryControlColorStruct;

    Color bgStateColorize(StatesSet currentStates) {
      final Color hlightColor = colorStruct.hlightColor;
      final Color reducedColor = hlightColor.withOpacity(.6);
      return switch (currentStates) {
        (StatesSet state) when state.contains(MStates.hovered) => hlightColor,
        _ => reducedColor,
      };
    }

    Color olStateColorize(StatesSet currentStates) {
      final Color hlightColor = colorStruct.hlightColorAlt ?? Colors.blue.shade900;
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
          backgroundColor: MaterialStateColor.resolveWith(bgStateColorize),
          overlayColor: MaterialStateColor.resolveWith(olStateColorize),
          shape: MaterialStateProperty.all(const LinearBorder()),
        ),
        onPressed: onTap,
        child: Text(
          label,
          style: TextStyle(color: colorStruct.onColorAlt),
        ),
      ),
    );
  }
}
