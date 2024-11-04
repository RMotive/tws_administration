part of 'master_layout.dart';

class _MasterLayoutLarge extends StatelessWidget {
  final String currentRoute;
  final List<_MasterLayoutMenuButtonOptions> buttons;
  final Widget page;

  const _MasterLayoutLarge({
    required this.currentRoute,
    required this.buttons,
    required this.page,
  });

  @override
  Widget build(BuildContext context) {
    final MasterLayoutMenuState masterLayoutMenuDrawer = masterLayoutMenuState;
    masterLayoutMenuDrawer.deactivateDrawer();

    return Row(
      children: <Widget>[
        _MasterLayoutMenu(
          buttonsoptions: buttons,
          currentRoute: currentRoute,
        ),
        Expanded(
          child: Column(
            children: <Widget>[
              const _MasterLayoutHeader(),
              Expanded(
                child: page,
              ),
            ],
          ),
        ),
      ],
    );
  }
}
