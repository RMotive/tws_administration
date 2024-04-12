part of 'whisper_frame.dart';

class _WhisperFooter extends StatelessWidget {
  static const double _actionsWidth = 125;

  final CSMColorThemeOptions pageTheme;
  final VoidCallback? trigger;
  final bool closer;
  final VoidCallback? onClose;
  const _WhisperFooter({
    this.onClose,
    this.trigger,
    required this.closer,
    required this.pageTheme,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: double.maxFinite,
      child: Padding(
        padding: const EdgeInsets.symmetric(
          vertical: 12,
          horizontal: 18,
        ),
        child: CSMSpacingRow(
          mainAlignment: MainAxisAlignment.end,
          spacing: 16,
          children: <Widget>[
            // --> Close whisper action button.
            if (closer)
              TWSButtonFlat(
                label: 'Close',
                width: _actionsWidth,
                themeOptions: CSMColorThemeOptions(
                  Colors.transparent,
                  Colors.black,
                  Colors.red.shade900,
                  foreAlt: Colors.white,
                  hightlightAlt: Colors.red.shade900,
                ),
                onTap: () {
                  Navigator.of(context).pop();
                  onClose?.call();
                },
              ),
            // --> Trigger action button
            if (trigger != null)
              TWSButtonFlat(
                label: 'Finish',
                width: _actionsWidth,
                onTap: () => trigger?.call(),
              ),
          ],
        ),
      ),
    );
  }
}
