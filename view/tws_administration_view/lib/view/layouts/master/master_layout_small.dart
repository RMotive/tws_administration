part of 'master_layout.dart';

class _MasterLayoutSmall extends StatelessWidget {
  final String currentRoute;
  final List<_MasterLayoutMenuButtonOptions> buttons;
  final Widget page;

  const _MasterLayoutSmall({
    required this.page,
    required this.currentRoute,
    required this.buttons,
  });

  @override
  Widget build(BuildContext context) {
    final MasterLayoutMenuState state = masterLayoutMenuState;
    state.restore();

    return ListenableBuilder(
      listenable: state,
      builder: (_, __) {
        return LayoutBuilder(
          builder: (_, BoxConstraints constrains) {
            return Stack(
              fit: StackFit.loose,
              children: <Widget>[
                Column(
                  children: <Widget>[
                    const _MasterLayoutHeader(),
                    Expanded(
                      child: page,
                    ),
                  ],
                ),
                AnimatedPositioned(
                  duration: !state.drawerClosed ? 300.miliseconds : 1.seconds,
                  right: state.drawerClosed ? constrains.maxWidth : 0,
                  height: constrains.maxHeight,
                  width: constrains.maxWidth,
                  child: ClipRRect(
                    child: BackdropFilter(
                      filter: ImageFilter.blur(
                        sigmaX: state.drawerClosed ? 0 : 2,
                        sigmaY: state.drawerClosed ? 0 : 2,
                      ),
                      child: Row(
                        children: <Widget>[
                          _MasterLayoutMenu(
                            currentRoute: currentRoute,
                            buttonsoptions: buttons,
                          ),
                          // --> Clickable zone to hide menu drawer
                          Expanded(
                            child: GestureDetector(
                              behavior: HitTestBehavior.opaque,
                              onTap: () {
                                state.closeMenu();
                              },
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                )
              ],
            );
          },
        );
      },
    );
  }
}
