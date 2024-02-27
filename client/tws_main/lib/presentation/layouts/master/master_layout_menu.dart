part of 'master_layout.dart';

class _MasterLayoutMenu extends StatelessWidget {
  const _MasterLayoutMenu();

  @override
  Widget build(BuildContext context) {
    final ThemeBase theme = getTheme();
    final ThemeColorStruct themeStruct = theme.masterLayoutStruct;
    final Size screen = MediaQuery.sizeOf(context);

    return ConstrainedBox(
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
              const Padding(
                padding: EdgeInsets.symmetric(
                  vertical: 8,
                  horizontal: 4,
                ),
                child: _MasterLayoutMenuButton(
                  display: 'Landing Page',
                  isCurrent: true,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
