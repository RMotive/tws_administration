part of '../article_frame.dart';

class _ArticleFrameActionsButton extends StatefulWidget {
  final CSMStateThemeOptions struct;
  final ArticleFrameActionsOptions options;
  final double size;
  const _ArticleFrameActionsButton({
    required this.size,
    required this.struct,
    required this.options,
  });

  @override
  State<_ArticleFrameActionsButton> createState() => _ArticleFrameActionsButtonState();
}

class _ArticleFrameActionsButtonState extends State<_ArticleFrameActionsButton> {
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
  void didUpdateWidget(covariant _ArticleFrameActionsButton oldWidget) {
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
      message: widget.options.tooltip,
      child: CSMPointerHandler(
        cursor: controlState == CSMStates.hovered ? SystemMouseCursors.click : MouseCursor.defer,
        onHover: (bool hover) {
          changeState(controlState = hover ? CSMStates.hovered : CSMStates.none);
        },
        onClick: widget.options.action,
        child: CSMColorBox(
          size: Size.square(widget.size),
          background: background,
          child: Center(
            child: SizedBox(
              height: widget.size,
              child: widget.options.decorator(widget.size, foreground),
            ),
          ),
        ),
      ),
    );
  }
}
