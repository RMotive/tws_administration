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
  late List<_MSMDButtonOption> buttons;
  // State
  late int currentSelection;

  @override
  void initState() {
    super.initState();
    buttons = widget.buttons;
    theme = getTheme(
      updateEfect: updateThemeEffect,
    );
    final String currentPath = Uri.base.pathSegments.last;
    currentSelection = 0;
    for (int p = 0; p < buttons.length; p++) {
      final _MSMDButtonOption button = buttons[p];
      final String buttonPath = button.route?.path ?? '';
      if (buttonPath.contains(currentPath)) {
        currentSelection = p;
      }
    }
  }

  @override
  void didUpdateWidget(covariant _MainScreenMenuDrawer oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.buttons != widget.buttons) buttons = widget.buttons;
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    screenSize = MediaQuery.sizeOf(context);
  }

  @override
  void dispose() {
    disposeGetTheme(updateThemeEffect);
    super.dispose();
  }

  /// Update effect for theme changes
  void updateThemeEffect() {
    setState(() {
      theme = getTheme();
      },
    );
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
                for (int buttonPointer = 0; buttonPointer < buttons.length; buttonPointer++)
                  TWSDrawerButton(
                    icon: buttons[buttonPointer].icon,
                    selected: buttonPointer == currentSelection,
                    action: () {
                      RouteOptions? buttonRoute = buttons[buttonPointer].route;
                      if (buttonRoute != null) {
                        setState(() => currentSelection = buttonPointer);
                        _router.driveTo(buttonRoute);
                      }
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
