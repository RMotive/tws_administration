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
  CosmosControlStates controlState = CosmosControlStates.none;
  late ThemeBase theme;

  void listenTheme() {
    theme = getTheme();
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
    );
  }

  @override
  void dispose() {
    disposeGetTheme(listenTheme);
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return MouseRegion(
      onEnter: (_) => changeState(CosmosControlStates.hovered),
      onExit: (_) => changeState(CosmosControlStates.none),
      cursor: SystemMouseCursors.click,
      child: ColoredBox(
        color: evaluateControlState(
          controlState,
          onIdle: () => Colors.transparent,
        ),
        child: SizedBox(
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Center(
              child: Row(
                children: <Widget>[
                  Icon(
                    Icons.dashboard,
                    color: theme.pageColorStruct.onColor,
                    size: 40,
                  ),
                  Padding(
                    padding: const EdgeInsets.only(
                      left: 4,
                    ),
                    child: Text(
                      widget.display,
                      style: TextStyle(
                        color: theme.pageColorStruct.onColor,
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
