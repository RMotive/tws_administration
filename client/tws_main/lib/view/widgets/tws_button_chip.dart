import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';

class TWSButtonChip extends StatefulWidget {
  final CSMStateThemeOptions struct;
  final double size;
  final String tooltip;
  final Widget Function(double size, Color color) decorator;
  final VoidCallback action;
  
  const TWSButtonChip({
    super.key, 
    required this.size,
    required this.struct,
    required this.tooltip,
    required this.decorator,
    required this.action
  });

  @override
  State<TWSButtonChip> createState() => _TWSButtonChipState();
}

class _TWSButtonChipState extends State<TWSButtonChip> {
  static const Color _kBackground = Colors.blueGrey;
  static const Color _kForeground = Colors.white70;

  // --> State resources
  late Color background;
  late Color foreground;
  CSMStates controlState = CSMStates.none;

  void changeState(CSMStates state) {
    setState(() {
      controlState = state;
      evaluateTheme();
    });
  }

  void evaluateTheme() {
    CSMGenericThemeOptions evalStruct = controlState.evaluateTheme(widget.struct);
    background = evalStruct.background ?? _kBackground;
    foreground = evalStruct.foreground ?? _kForeground;
  }

  @override
  void initState() {
    super.initState();
    evaluateTheme();
  }

  @override
  void didUpdateWidget(covariant TWSButtonChip oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (widget.struct != oldWidget.struct) {
      setState(() {
        evaluateTheme();
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Tooltip(
      message: widget.tooltip,
      child: CSMPointerHandler(
        cursor: controlState == CSMStates.hovered ? SystemMouseCursors.click : MouseCursor.defer,
        onHover: (bool hover) {
          changeState(controlState = hover ? CSMStates.hovered : CSMStates.none);
        },
        onClick: widget.action,
        child: CSMColorBox(
          size: Size.square(widget.size),
          background: background,
          child: Center(
            child: SizedBox(
              height: widget.size,
              child: widget.decorator(widget.size, foreground),
            ),
          ),
        ),
      ),
    );
  }
}
