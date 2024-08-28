part of '../tws_article_table.dart';

class _TWSArticleTableDetailsAction extends StatelessWidget {
  final String? hint;
  final VoidCallback action;
  final IconData icon;
  final Color? fore;

  const _TWSArticleTableDetailsAction({
    this.hint,
    this.fore,
    required this.icon,
    required this.action,
  });

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase themeBase = getTheme<TWSAThemeBase>();

    final CSMColorThemeOptions tPage = themeBase.page;
    final CSMColorThemeOptions tPrimary = themeBase.primaryControlColor;
    return Tooltip(
      message: hint,
      child: CSMPointerHandler(
        cursor: SystemMouseCursors.click,
        onClick: action,
        child: DecoratedBox(
          decoration: BoxDecoration(
            shape: BoxShape.circle,
            color: tPage.fore,
          ),
          child: Padding(
            padding: const EdgeInsets.all(3),
            child: Icon(
              icon,
              color: fore ?? tPrimary.main,
              size: 18,
            ),
          ),
        ),
      ),
    );
  }
}
