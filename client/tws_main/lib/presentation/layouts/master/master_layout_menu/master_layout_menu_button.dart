part of '../master_layout.dart';

final RouteDriver _routeDriver = RouteDriver.i;

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
  late StateControlThemeStruct theme;
  late StandardThemeStruct stateTheme;

  // --> State management
  CosmosControlStates controlState = CosmosControlStates.none;

  void listenTheme() {
    theme = getTheme<ThemeBase>().masterLayoutMenuButtonStruct;
    assert(theme.hoverStruct != null, 'Uses hover state');
    assert(theme.selectStruct != null, 'Uses select state');
    stateTheme = evaluateThemeState(controlState, theme);
  }

  void changeState(CosmosControlStates state) {
    setState(() {
      controlState = state;
      stateTheme = evaluateThemeState(controlState, theme);
    });
  }

  @override
  void initState() {
    super.initState();
    theme = getTheme<ThemeBase>(
      updateEfect: listenTheme,
    ).masterLayoutMenuButtonStruct;
    assert(theme.hoverStruct != null, 'Uses hover state');
    assert(theme.selectStruct != null, 'Uses select state');

    if (widget.isCurrent) {
      controlState = CosmosControlStates.selected;
    }
    stateTheme = evaluateThemeState(controlState, theme);
  }

  @override
  void dispose() {
    disposeGetTheme(listenTheme);
    super.dispose();
  }

  @override
  void didUpdateWidget(covariant _MasterLayoutMenuButton oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.isCurrent != widget.isCurrent && !widget.isCurrent) {
      changeState(CosmosControlStates.none);
    }
  }

  @override
  Widget build(BuildContext context) {
    final double foregroundSize = stateTheme.textStyle?.fontSize ?? 16;

    return MouseRegion(
      onEnter: (_) => changeState(CosmosControlStates.hovered),
      onExit: (_) => changeState(widget.isCurrent ? CosmosControlStates.selected : CosmosControlStates.none),
      cursor: widget.isCurrent ? MouseCursor.defer : SystemMouseCursors.click,
      child: GestureDetector(
        onTap: widget.isCurrent
            ? null
            : () {
                _routeDriver.driveTo(widget.options.route);
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
