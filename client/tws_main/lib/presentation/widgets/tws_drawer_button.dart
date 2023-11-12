import 'package:cosmos_foundation/extensions/int_extension.dart';
import 'package:cosmos_foundation/helpers/responsive.dart';
import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/options/responsive_property_options.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/constants/config/theme/theme_base.dart';

// --> Helpers
/// Internal [Responsive] helper reference.
final Responsive _responsive = Responsive.i;

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

  /// The displayed right-side text in the button, indicating the
  /// routed page name or identifier.
  final String label;

  /// The event that will fire when the button is clicked.
  final void Function()? action;

  const TWSDrawerButton({
    super.key,
    required this.icon,
    required this.label,
    this.selected = false,
    this.action,
  });

  @override
  State<TWSDrawerButton> createState() => _TWSDrawerButtonState();
}

class _TWSDrawerButtonState extends State<TWSDrawerButton> {
  // --> Resources
  static const double _buttonRadius = 45;

  // --> State
  late bool hovered;
  late ThemeBase theme;

  @override
  void initState() {
    super.initState();
    hovered = false;
    theme = getTheme(
      updateEfect: updateThemeEffect,
    );
  }

  @override
  void dispose() {
    disposeGetTheme(updateThemeEffect);
    super.dispose();
  }

  /// Update use effect for theming changes on this state compoennt.
  void updateThemeEffect() {
    setState(() {
      theme = getTheme();
    });
  }

  double getButtonSize() {
    final double screenSize = MediaQuery.sizeOf(context).width;
    return screenSize * .1;
  }

  @override
  Widget build(BuildContext context) {
    final ColorBundle selectedButtonBundle = theme.primaryColor;
    final ColorBundle regularButtonBundle = theme.onPrimaryColorSecondControlColor;

    return MouseRegion(
      cursor: widget.selected ? SystemMouseCursors.basic : SystemMouseCursors.click,
      onEnter: (_) => setState(() => hovered = true),
      onExit: (_) => setState(() => hovered = false),
      hitTestBehavior: HitTestBehavior.opaque,
      child: GestureDetector(
        onTap: () => widget.selected ? null : widget.action?.call(),
        child: AnimatedContainer(
          constraints: const BoxConstraints(
            maxWidth: 200,
          ),
          duration: 200.miliseconds,
          decoration: BoxDecoration(
            color: widget.selected ? selectedButtonBundle.mainColor : regularButtonBundle.mainColor,
            borderRadius: BorderRadius.all(
              Radius.circular(
                _responsive.propertyFromDefault(
                  const ResponsivePropertyOptions<double>(100, 100, 0),
                ),
              ),
            ),
            border: Border.fromBorderSide(
              BorderSide(
                width: 1,
                strokeAlign: BorderSide.strokeAlignInside,
                color: (hovered && !widget.selected) ? theme.primaryColor.mainColor : Colors.transparent,
              ),
            ),
          ),
          width: _responsive.propertyFromDefault(
            ResponsivePropertyOptions<double>(_buttonRadius + 2, _buttonRadius + 2, getButtonSize()),
          ),
          child: Row(
            children: <Widget>[
              SizedBox.square(
                dimension: _buttonRadius,
                child: Icon(
                  widget.icon,
                  color: theme.onPrimaryColorSecondControlColor.textColor,
                ),
              ),
              if (_responsive.isLargeDevice)
                Expanded(
                  child: Padding(
                    padding: const EdgeInsets.only(
                      right: 12,
                    ),
                    child: Center(
                      child: Text(
                        widget.label,
                        maxLines: 2,
                        style: TextStyle(
                          fontSize: 12,
                          color: widget.selected ? selectedButtonBundle.textColor : regularButtonBundle.textColor,
                          overflow: TextOverflow.fade,
                        ),
                      ),
                    ),
                  ),
                ),
            ],
          ),
        ),
      ),
    );
  }
}
