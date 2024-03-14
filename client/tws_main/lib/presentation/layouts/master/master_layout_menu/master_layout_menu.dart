part of '../master_layout.dart';

class _MasterLayoutMenu extends StatelessWidget {
  final String currentRoute;
  final List<_MasterLayoutMenuButtonOptions> buttonsoptions;

  const _MasterLayoutMenu({
    required this.currentRoute,
    required this.buttonsoptions,
  });

  @override
  Widget build(BuildContext context) {
    const ClampRatioConstraints ratioCts = ClampRatioConstraints(1024, 1400, _minMenuWidth, _maxMenuWidth);

    final ThemeBase theme = getTheme();
    final Size screen = MediaQuery.sizeOf(context);
    final ThemeColorStruct themeStruct = theme.masterLayoutStruct;

    final double currentMenuWidth = Responsive.clampRatio(screen.width, ratioCts);

    return ConstrainedBox(
      constraints: const BoxConstraints(
        minWidth: _minMenuWidth,
        maxWidth: _maxMenuWidth,
      ),
      child: ColoredBox(
        color: themeStruct.mainColor,
        child: SizedBox(
          width: currentMenuWidth,
          child: Column(
            children: <Widget>[
              LayoutBuilder(
                builder: (_, BoxConstraints constrains) {
                  return Padding(
                    padding: const EdgeInsets.all(20.0),
                    child: Center(
                      child: Image(
                        width: constrains.maxWidth * .4,
                        image: AssetImage(
                          theme.masterLayoutMenuLogo,
                        ),
                      ),
                    ),
                  );
                },
              ),
              Padding(
                padding: const EdgeInsets.symmetric(
                  vertical: 8,
                  horizontal: 4,
                ),
                child: SpacingColumn(
                  spacing: 8,
                  children: <Widget>[
                    for (_MasterLayoutMenuButtonOptions options in buttonsoptions)
                      _MasterLayoutMenuButton(
                        options: options,
                        isCurrent: currentRoute.contains(options.route.path),
                      ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
