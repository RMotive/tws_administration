part of '../main_screen.dart';

/// UI Page Private Component to handle [MainScreen] UI Screen small behavior.
///
/// This private component draws a beside buttons drawer to navigate along application pages wrapped in [MainScreen].
class _MainScreenMenuDrawer extends StatefulWidget {
  /// Options for each button that will be drawn in the beside drawer.
  final List<_MSMDButtonOption> buttons;

  const _MainScreenMenuDrawer({
    required this.buttons,
  });

  @override
  State<_MainScreenMenuDrawer> createState() => _MainScreenMenuDrawerState();
}

class _MainScreenMenuDrawerState extends State<_MainScreenMenuDrawer> {
  // Resources
  late Size screenSize;
  late ThemeBase theme;
  // State
  late int currentSelection;

  @override
  void initState() {
    super.initState();
    theme = getTheme();
    currentSelection = 0;
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    screenSize = MediaQuery.sizeOf(context);
  }

  @override
  Widget build(BuildContext context) {
    return ConstrainedBox(
      constraints: BoxConstraints(
        minHeight: screenSize.height,
      ),
      child: ClipRRect(
        borderRadius: const BorderRadius.horizontal(
          right: Radius.circular(35),
        ),
        child: ColoredBox(
          color: theme.primaryColor.counterColor ?? theme.primaryColor.textColor,
          child: Padding(
            padding: const EdgeInsets.all(10),
            child: SeparatorColumn(
              spacing: 8,
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                for (_MSMDButtonOption button in widget.buttons)
                  TWSDrawerButton(
                    icon: button.icon,
                    selected: widget.buttons.indexOf(button) == currentSelection,
                    action: () {
                      if (button.route != null) _router.driveTo(button.route!);
                    },
                  ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
