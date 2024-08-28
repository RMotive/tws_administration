part of '../master_layout.dart';

class _MasterLayoutHeader extends StatelessWidget {
  const _MasterLayoutHeader();

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions themeStruct = getTheme<TWSAThemeBase>().masterLayout;
    final MasterLayoutMenuState masterLayoutMenu = masterLayoutMenuState;

    return ColoredBox(
      color: themeStruct.main,
      child: SizedBox(
        height: 50,
        child: Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: 12,
            ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Visibility(
                visible: masterLayoutMenu.drawerActive,
                child: MouseRegion(
                  cursor: SystemMouseCursors.click,
                  child: GestureDetector(
                    onTap: () {
                      masterLayoutMenu.openMenu();
                    },
                    child: Icon(
                      Icons.menu,
                      color: themeStruct.fore,
                    ),
                  ),
                ),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(vertical: 6),
                child: MasterUserButton(
                  themeOptions: themeStruct
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
