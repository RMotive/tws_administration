
import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/bases/twsa_theme_base.dart';

///[TWSSwitchButton] Custom widget for TWS environment.
///This widget returns a boolean, based on the its state.
class TWSSwitchButton extends StatefulWidget {
  /// [switchHeight] switch button heigth
  final double switchHeight;
  /// [value] switch state.
  final bool value;
  /// [title] Text to show on top of the component.
  final String title;
  /// [padding] Internal Padding value.
  final EdgeInsetsGeometry padding;
  /// [onChanged] Callback for switch state, returning the state value. 
  final void Function(bool) onChanged;
  const TWSSwitchButton({super.key,
    required this.title,
    required this.onChanged,
    this.switchHeight = 35,
    this.value = false,
    this.padding = const EdgeInsets.all(5.0)
  });

  @override
  State<TWSSwitchButton> createState() => _TWSSwitchButtonState();
}

class _TWSSwitchButtonState extends State<TWSSwitchButton> {
  late final TWSAThemeBase theme;
  late final CSMColorThemeOptions colorStruct;
  bool _value = false;

  @override
  void initState() {
    _value = widget.value;
    theme = getTheme();
    colorStruct = theme.primaryControlColor;
    super.initState();
  }


  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: widget.padding,
      child: Column(
        children: <Widget>[
          Text(
            widget.title,
            style: TextStyle(
              color: colorStruct.foreAlt,
            ),
          ),
          SizedBox(
            height: widget.switchHeight,
            child: FittedBox(
              fit: BoxFit.fill,
              child: Switch(
                value: _value,
                activeColor: colorStruct.foreAlt,
                activeTrackColor: colorStruct.main,
                onChanged: (bool change) {
                  setState(() {
                    _value = change;
                    widget.onChanged(change);
                    print(_value);
                  });
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}
