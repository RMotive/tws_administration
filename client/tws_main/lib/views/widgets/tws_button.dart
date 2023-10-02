import 'package:cosmos_foundation/widgets/hooks/themed_widget.dart';
import 'package:flutter/material.dart';

class TWSButton extends StatelessWidget {
  final Size? buttonSize;
  final EdgeInsets? padding;
  final String? label;
  final void Function()? action;

  const TWSButton({
    super.key,
    this.buttonSize,
    this.padding,
    this.label,
    this.action,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: padding ?? const EdgeInsets.all(0),
      child: Tooltip(
        message: 'Button to ${label ?? 'tap'}',
        child: ThemedWidget(builder: (BuildContext context, ThemeData theme) {
          return MouseRegion(
            cursor: SystemMouseCursors.click,
            child: GestureDetector(
              onTap: () => action,
              child: DecoratedBox(
                decoration: BoxDecoration(
                  color: theme.colorScheme.secondary,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: SizedBox(
                  width: buttonSize?.width,
                  height: buttonSize?.height,
                  child: Center(
                    child: Text(
                      label ?? 'Tap',
                      style: TextStyle(
                        color: theme.colorScheme.onSecondary,
                        fontStyle: FontStyle.italic,
                      ),
                    ),
                  ),
                ),
              ),
            ),
          );
        }),
      ),
    );
  }
}
