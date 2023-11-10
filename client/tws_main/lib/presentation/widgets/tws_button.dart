import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

class TWSButton extends StatelessWidget {
  final Size? buttonSize;
  final EdgeInsets? padding;
  final String? label;
  final void Function()? action;
  final String toolTip;

  const TWSButton({
    super.key,
    this.buttonSize,
    this.padding,
    this.label,
    this.action,
    this.toolTip = "",
  });

  @override
  Widget build(BuildContext context) {
    ColorBundle controlColorBundle = getTheme<ThemeBase>().onPrimaryColorSecondControlColor;

    return Padding(
      padding: padding ?? const EdgeInsets.all(0),
      child: Tooltip(
        message: toolTip,
        child: MouseRegion(
          cursor: SystemMouseCursors.click,
          child: GestureDetector(
            onTap: action,
            child: DecoratedBox(
              decoration: BoxDecoration(
                color: controlColorBundle.mainColor,
                borderRadius: BorderRadius.circular(4),
              ),
              child: SizedBox(
                width: buttonSize?.width,
                height: buttonSize?.height,
                child: Center(
                  child: Text(
                    label ?? 'Tap',
                    style: TextStyle(
                      color: controlColorBundle.textColor,
                      fontStyle: FontStyle.italic,
                    ),
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
