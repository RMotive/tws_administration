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
    final ThemeBase theme = getTheme();
    final ThemeColorStruct themeStruct = theme.masterLayoutStruct;
    final Size screen = MediaQuery.sizeOf(context);

    return Transform.translate(
      offset: const Offset(0, 0),
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          minWidth: 200,
          maxWidth: 250,
        ),
        child: ColoredBox(
          color: themeStruct.mainColor,
          child: SizedBox(
            width: screen.width * .25,
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
                  child: CosmosSeparatedColumn(
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
      ),
    );
  }
}
