part of '../article_frame.dart';

typedef States = CosmosControlStates;

/// [Private] component.
///
/// Draws and manages the articles layout selector ribbon buttons states and behavior.
class _ArticlesSelectorButton extends StatefulWidget {
  final bool enabled;
  final bool isCurrent;
  final ArticleOptions options;
  final _CStruct disabledStruct;
  final _SCStruct stateStruct;

  const _ArticlesSelectorButton({
    required this.options,
    required this.enabled,
    required this.isCurrent,
    required this.disabledStruct,
    required this.stateStruct,
  });

  @override
  State<_ArticlesSelectorButton> createState() => __ArticlesSelectorButtonState();
}

class __ArticlesSelectorButtonState extends State<_ArticlesSelectorButton> {
  late States currentState;
  Color? background = Colors.transparent;
  Color? foreground = Colors.transparent;

  @override
  void initState() {
    super.initState();
    currentState = widget.isCurrent ? States.selected : States.none;
    gatColors();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    if (widget.isCurrent) {
      currentState = States.selected;
    }
  }

  void gatColors() {
    StandardThemeStruct struct = evaluateThemeState(currentState, widget.stateStruct);
    if (widget.enabled) {
      background = struct.background;
      foreground = struct.foreground;
    } else if (widget.isCurrent) {
      background = struct.background;
      foreground = struct.foreground;
    } else {
      foreground = widget.disabledStruct.onColorAlt;
      background = widget.disabledStruct.mainColor;
    }
  }

  /// Changes the current control state to recalculate its theme.
  void changeState(States state) {
    if (!canInteract()) return;
    setState(() {
      currentState = state;
      gatColors();
    });
  }

  bool canInteract() {
    return (widget.enabled && !widget.isCurrent);
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        if (canInteract() && widget.options.route != null) {
          _routeDriver.drive(widget.options.route!);
        }
      },
      child: MouseRegion(
        cursor: canInteract() ? SystemMouseCursors.click : MouseCursor.defer,
        onEnter: (_) => changeState(States.hovered),
        onExit: (_) => changeState(States.none),
        child: CosmosColorBox(
          size: const Size(75, 75),
          color: background ?? Colors.blueGrey.shade700,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              SizedBox.fromSize(
                size: const Size(30, 30),
                child: widget.options.icon(foreground),
              ),
              Padding(
                padding: const EdgeInsets.only(
                  top: 5,
                ),
                child: Text(
                  widget.options.title,
                  softWrap: true,
                  style: TextStyle(
                    overflow: TextOverflow.fade,
                    color: foreground,
                    fontSize: 14,
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
