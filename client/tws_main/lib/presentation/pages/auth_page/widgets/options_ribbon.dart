part of '../auth_page.dart';

class _OptionsRibbon extends StatefulWidget {
  final void Function(bool isEnable) onTipChange;
  const _OptionsRibbon({
    required this.onTipChange,
  });

  @override
  State<_OptionsRibbon> createState() => _OptionsRibbonState();
}

class _OptionsRibbonState extends State<_OptionsRibbon> {
  // --> State
  late bool currentIsLight;
  late bool tipEnabled;

  @override
  void initState() {
    super.initState();
    currentIsLight = getTheme().runtimeType == LightTheme;
    tipEnabled = false;
  }

  void switchTipOption() async {
    /// --> Forcing the await for the Fade Out tooltip animation duration.
    /// This prevents when the user taps fastly the tooltip enable/disable button
    /// doesn't got messed up showing a half-faded grey tooltip.
    await Future<void>.delayed(150.miliseconds);
    if (mounted) setState(() => tipEnabled = !tipEnabled);
  }

  String onTip(String tip) {
    if (tipEnabled) return tip;
    return '';
  }

  @override
  Widget build(BuildContext context) {
    return FittedBox(
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: SeparatorRow(
          spacing: 12,
          children: <Widget>[
            // --> Tooltips toogle
            TwsThemeToogler(
              tooltip: tipEnabled,
            ),
            // --> Theming toogle.
            ToogleRoundedButton(
              icon: Icons.disabled_visible,
              onFire: switchTipOption,
              toolTip: onTip('Tap to disable the tooltips'),
            ),
          ],
        ),
      ),
    );
  }
}
