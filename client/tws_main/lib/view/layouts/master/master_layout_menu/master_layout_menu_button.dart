part of '../master_layout.dart';

final CSMRouter _routeDriver = CSMRouter.i;

class _MasterLayoutMenuButton extends StatefulWidget {
  final _MasterLayoutMenuButtonOptions options;
  final bool isCurrent;
  const _MasterLayoutMenuButton({
    required this.options,
    this.isCurrent = false,
  });

  @override
  State<_MasterLayoutMenuButton> createState() => _MasterLayoutMenuButtonState();
}

class _MasterLayoutMenuButtonState extends State<_MasterLayoutMenuButton> {
  // --> Resources
  late CSMStateThemeOptions theme;
  late CSMGenericThemeOptions stateTheme;

  // --> State management
  CSMStates controlState = CSMStates.none;

  void listenTheme() {
    theme = getTheme<TWSAThemeBase>().masterLayoutMenuButtonStruct;
    assert(theme.hovered != null, 'Uses hover state');
    assert(theme.selected != null, 'Uses select state');
    stateTheme = controlState.evaluateTheme(theme);
  }

  void changeState(CSMStates state) {
    setState(() {
      controlState = state;
      stateTheme = state.evaluateTheme(theme);
    });
  }

  @override
  void initState() {
    super.initState();
    theme = getTheme<TWSAThemeBase>(
      updateEfect: listenTheme,
    ).masterLayoutMenuButtonStruct;
    assert(theme.hovered != null, 'Uses hover state');
    assert(theme.selected != null, 'Uses select state');

    if (widget.isCurrent) {
      controlState = CSMStates.selected;
    }
    stateTheme = controlState.evaluateTheme(theme);
  }

  @override
  void dispose() {
    disposeEffect(listenTheme);
    super.dispose();
  }

  @override
  void didUpdateWidget(covariant _MasterLayoutMenuButton oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.isCurrent != widget.isCurrent && !widget.isCurrent) {
      changeState(CSMStates.none);
    }
  }

  @override
  Widget build(BuildContext context) {
    final double foregroundSize = stateTheme.textStyle?.fontSize ?? 16;

    return MouseRegion(
      onEnter: (_) => changeState(CSMStates.hovered),
      onExit: (_) => changeState(widget.isCurrent ? CSMStates.selected : CSMStates.none),
      cursor: widget.isCurrent ? MouseCursor.defer : SystemMouseCursors.click,
      child: GestureDetector(
        onTap: widget.isCurrent
            ? null
            : () {
                _routeDriver.drive(widget.options.route);
              },
        child: ColoredBox(
          color: stateTheme.background as Color,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Center(
              child: Row(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  Icon(
                    widget.options.icon,
                    color: stateTheme.iconColor,
                    size: foregroundSize * 1.75,
                  ),
                  Padding(
                    padding: const EdgeInsets.only(
                      left: 10,
                    ),
                    child: Text(
                      widget.options.label,
                      style: stateTheme.textStyle ??
                          TextStyle(
                            fontSize: foregroundSize,
                            color: stateTheme.foreground,
                          ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
