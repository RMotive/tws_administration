import 'package:cosmos_foundation/foundation/hooks/responsive_view.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';

/// Public Widget for TWS Button to Drawers.
///
/// This widget draws a responsive button for besides/top (depending on the device size)
/// drawers that could be used by the TWS Application.
class TWSDrawerButton extends StatefulWidget {
  /// Indicates that this button is selected and draws a slight border radius.
  ///
  /// This border radius drawn when the button is selected, is not being affected by hover behavior.
  final bool selected;

  /// The displayed icon in the button center.
  final IconData icon;

  /// The event that will fire when the button is clicked.
  final void Function()? action;

  const TWSDrawerButton({
    super.key,
    required this.icon,
    this.selected = false,
    this.action,
  });

  @override
  State<TWSDrawerButton> createState() => _TWSDrawerButtonState();
}

class _TWSDrawerButtonState extends State<TWSDrawerButton> {
  // --> Resources
  static const double _buttonRadius = 45;
  final ThemeBase theme = getTheme();
  // --> State
  late bool hovered;

  @override
  void initState() {
    super.initState();
    hovered = false;
  }

  @override
  Widget build(BuildContext context) {
    return MouseRegion(
      cursor: widget.selected ? SystemMouseCursors.basic : SystemMouseCursors.click,
      onEnter: (_) => setState(() => hovered = true),
      onExit: (_) => setState(() => hovered = false),
      hitTestBehavior: HitTestBehavior.opaque,
      child: GestureDetector(
        onTap: () => widget.selected ? null : widget.action?.call(),
        child: ResponsiveView(
          onLarge: const SizedBox(),
          onSmall: const SizedBox(),
          onMedium: Container(
            decoration: BoxDecoration(
              color: theme.onPrimaryColorSecondControlColor.mainColor,
              shape: BoxShape.circle,
              border: widget.selected
                  ? Border.fromBorderSide(
                      BorderSide(
                        color: Colors.purple.shade200,
                        width: 1,
                      ),
                    )
                  : !hovered
                      ? null
                      : Border.fromBorderSide(
                          BorderSide(
                            color: theme.primaryColor.mainColor,
                            strokeAlign: BorderSide.strokeAlignOutside,
                            width: 1,
                          ),
                        ),
            ),
            child: SizedBox.square(
              dimension: _buttonRadius,
              child: Icon(
                widget.icon,
                color: theme.onPrimaryColorSecondControlColor.textColor,
              ),
            ),
          ),
        ),
      ),
    );
  }
}
