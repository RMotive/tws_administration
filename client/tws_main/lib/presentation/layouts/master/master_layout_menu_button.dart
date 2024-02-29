part of 'master_layout.dart';

class _MasterLayoutMenuButton extends StatefulWidget {
  final String display;
  final bool isCurrent;
  const _MasterLayoutMenuButton({
    required this.display,
    this.isCurrent = true,
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
  Widget build(BuildContext context) {
    final double foregroundSize = stateTheme.textStyle?.fontSize ?? 16;
    
    return MouseRegion(
      onEnter: (_) => changeState(CosmosControlStates.hovered),
      onExit: (_) => changeState(widget.isCurrent ? CosmosControlStates.selected : CosmosControlStates.none),
      cursor: widget.isCurrent ? MouseCursor.defer : SystemMouseCursors.click,
      child: ColoredBox(
        color: stateTheme.background as Color,
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Center(
            child: Row(
              children: <Widget>[
                Icon(
                  Icons.dashboard,
                  color: stateTheme.iconColor,
                  size: foregroundSize * 2,
                ),
                Padding(
                  padding: const EdgeInsets.only(
                    left: 10,
                  ),
                  child: Text(
                    widget.display,
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
    );
  }
}
