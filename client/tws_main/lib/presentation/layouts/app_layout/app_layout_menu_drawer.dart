part of 'app_layout.dart';

/// UI Page Private Component to handle [AppLayout] UI Screen small behavior.
///
/// This private component draws a beside buttons drawer to navigate along application pages wrapped in [AppLayout].
class _AppLayoutMenuDrawer extends StatefulWidget {
  /// Options for each button that will be drawn in the beside drawer.
  final List<_MSMDButtonOption> buttons;

  const _AppLayoutMenuDrawer({
    required this.buttons,
  });

  @override
  State<_AppLayoutMenuDrawer> createState() => _AppLayoutMenuDrawerState();
}

class _AppLayoutMenuDrawerState extends State<_AppLayoutMenuDrawer> {
  // Resources
  late Size screenSize;
  late TWSThemeBase theme;
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
    // --> Finding out if the current page belongs to a different button that has the currentSelection set.
    currentSelection = 0;
    try {
      final String currentPath = Uri.base.pathSegments.last;
      for (int p = 0; p < buttons.length; p++) {
        final _MSMDButtonOption button = buttons[p];
        final String buttonPath = button.route?.path ?? '';
        if (buttonPath.contains(currentPath)) {
          currentSelection = p;
        }
      }
    } catch (_) {}
  }

  @override
  void didUpdateWidget(covariant _AppLayoutMenuDrawer oldWidget) {
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
    setState(
      () {
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
          right: Radius.circular(15),
        ),
        child: AnimatedContainer(
          duration: 300.miliseconds,
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
                    label: buttons[buttonPointer].label,
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