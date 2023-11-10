import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

class ToogleRoundedButton extends StatelessWidget {
  final double _controlReferenceSize;
  final IconData icon;
  final void Function() onFire;
  final String toolTip;

  const ToogleRoundedButton({
    super.key,
    required this.onFire,
    required this.icon,
    this.toolTip = '',
  }) : _controlReferenceSize = 45;

  @override
  Widget build(BuildContext context) {
    ThemeBase theme = getTheme();

    return Tooltip(
      message: toolTip,
      child: MouseRegion(
        cursor: SystemMouseCursors.click,
        child: GestureDetector(
          onTap: onFire,
          child: ClipRRect(
            borderRadius: BorderRadius.circular(_controlReferenceSize),
            clipBehavior: Clip.hardEdge,
            child: ColoredBox(
              color: theme.onPrimaryColorFirstControlColor.mainColor,
              child: SizedBox.square(
                dimension: _controlReferenceSize,
                child: Icon(
                  icon,
                  color: theme.onPrimaryColorFirstControlColor.textColor,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
