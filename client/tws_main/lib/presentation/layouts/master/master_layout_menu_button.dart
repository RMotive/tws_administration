part of 'master_layout.dart';

class _MasterLayoutMenuButton extends StatefulWidget {
  final String display;
  final bool isCurrent;
  const _MasterLayoutMenuButton({
    required this.display,
    this.isCurrent = false,
  });

  @override
  State<_MasterLayoutMenuButton> createState() => _MasterLayoutMenuButtonState();
}

class _MasterLayoutMenuButtonState extends State<_MasterLayoutMenuButton> {
  // --> Resources
  late StateControlThemeStruct theme;

  // --> State management
  CosmosControlStates controlState = CosmosControlStates.none;

  void listenTheme() {
    theme = getTheme<ThemeBase>().masterLayoutMenuButtonStruct;
  }

  void changeState(CosmosControlStates state) {
    setState(() {
      controlState = state;
    });
  }

  @override
  void initState() {
    super.initState();
    theme = getTheme<ThemeBase>(
      updateEfect: listenTheme,
    ).masterLayoutMenuButtonStruct;
  }

  @override
  void dispose() {
    disposeGetTheme(listenTheme);
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    assert(theme.hoverStruct != null, 'This component requires the theme for hover state');
    assert(theme.selectStruct != null, 'This component required the theme for select state');


    return MouseRegion(
      onEnter: (_) => changeState(CosmosControlStates.hovered),
      onExit: (_) => changeState(CosmosControlStates.none),
      cursor: SystemMouseCursors.click,
      child: ColoredBox(
        color: evaluateControlState(
          controlState,
          onIdle: () => theme.mainStruct.background,
          onHover: () => theme.hoverStruct!.background,
          onSelect: () => theme.selectStruct!.background,
        ),
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Center(
            child: Row(
              children: <Widget>[
                const Icon(
                  Icons.dashboard,
                  color: Colors.transparent,
                  size: 40,
                ),
                Padding(
                  padding: const EdgeInsets.only(
                    left: 4,
                  ),
                  child: Text(
                    widget.display,
                    style: const TextStyle(
                      color: Colors.transparent,
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
