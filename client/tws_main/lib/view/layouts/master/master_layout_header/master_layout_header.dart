part of '../master_layout.dart';

class _MasterLayoutHeader extends StatelessWidget {
  const _MasterLayoutHeader();

  @override
  Widget build(BuildContext context) {
    final CSMColorThemeOptions themeStruct = getTheme<TWSAThemeBase>().masterLayoutStruct;
    final MasterLayoutMenuState masterLayoutMenu = masterLayoutMenuState;

    return ColoredBox(
      color: themeStruct.main,
      child: SizedBox(
        height: 50,
        child: Row(
          children: <Widget>[
            Visibility(
              visible: masterLayoutMenu.drawerActive,
              child: Padding(
                padding: const EdgeInsets.symmetric(
                  horizontal: 12,
                ),
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
            ),
          ],
        ),
      ),
    );
  }
}
