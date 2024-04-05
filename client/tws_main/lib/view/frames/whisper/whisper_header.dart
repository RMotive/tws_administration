part of 'whisper_frame.dart';

class _WhisperHeader extends StatelessWidget {
  final String title;
  final CSMColorThemeOptions pageTheme;
  const _WhisperHeader({
    required this.title,
    required this.pageTheme,
  });

  @override
  Widget build(BuildContext context) {
    return ColoredBox(
      color: pageTheme.highlight,
      child: SizedBox(
        width: double.maxFinite,
        child: Padding(
          padding: const EdgeInsets.symmetric(
            vertical: 12,
            horizontal: 18,
          ),
          child: Text(
            title,
            textAlign: TextAlign.start,
            style: TextStyle(
              fontSize: 18,
              color: pageTheme.fore,
              fontWeight: FontWeight.w600,
              fontStyle: FontStyle.italic,
            ),
          ),
        ),
      ),
    );
  }
}
