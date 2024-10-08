part of 'login_page.dart';

class _BusinessDecorator extends StatelessWidget {
  const _BusinessDecorator();

  @override
  Widget build(BuildContext context) {
    final TWSAThemeBase theme = getTheme<TWSAThemeBase>();

    return Center(
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          minWidth: 200,
          maxWidth: 350,
        ),
        child: AspectRatio(
          aspectRatio: 1 / .75,
          child: Image(
            isAntiAlias: true,
            filterQuality: FilterQuality.high,
            image: AssetImage(
              theme.loginLogo,
            ),
          ),
        ),
      ),
    );
  }
}
