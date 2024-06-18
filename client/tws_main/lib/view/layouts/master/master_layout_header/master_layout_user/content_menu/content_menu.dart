part of '../../../master_layout.dart';

class _ContentMenu extends StatelessWidget {
  final double width;
  final double height;
  final CSMColorThemeOptions themeOptions;
  final void Function(CSMRouteOptions) onSelectOption;
  const _ContentMenu({
    required this.themeOptions, 
    required this.width, 
    required this.height, 
    required this.onSelectOption
  });

  @override
  Widget build(BuildContext context) {
    const double headerHeight = 50;
    return SizedBox(
      width: width,
      child: ClipRRect(
        borderRadius: BorderRadius.circular(5),
        child: ColoredBox(
          color: themeOptions.fore,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: <Widget>[
              Container(
                padding: const EdgeInsets.symmetric( vertical: 5.0),
                constraints: const BoxConstraints(
                  maxHeight:  headerHeight
                ),
                decoration: BoxDecoration(
                  color: themeOptions.main,
                  boxShadow: const <BoxShadow>[
                    BoxShadow(
                      blurRadius: 5
                    )
                  ]
                ),
                child: Row(
                  children: <Widget>[
                              
                    // Header
                    Expanded(
                      child: SizedBox(
                        height: 50,
                        width: 50,
                        child: DecoratedBox(
                          decoration: BoxDecoration(
                            color: themeOptions.fore,
                            shape: BoxShape.circle,
                          ),
                          child: Padding(
                            padding: const EdgeInsets.all(5),
                            child: FittedBox(
                              child: Icon(
                                color: themeOptions.main,
                                Icons.person_outline
                              )
                            )
                          ),
                        ),
                      ),
                    ),
                              
                    //User data
                    Expanded(
                      flex: 2,
                      child: SizedBox(
                        height: height,
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: <Widget>[
                            // Name
                            Text(
                              'Erick',
                              style: _headerMenuStyle.copyWith(
                                color: themeOptions.hightlightAlt ?? Colors.white,
                                fontSize: 14
                                ),
                            ),
                            // Email
                            Text(
                              'c.d@hotmail.es',
                              style: _headerMenuStyle.copyWith(color:  themeOptions.hightlightAlt ?? Colors.white),
                            )
                          ],
                        ),
                      ),
                    )
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(vertical: 10.0),
                child: Column(
                  children: <_MenuOption>[
                    for(_Options opt in _options)
                    _MenuOption(
                      options: opt, 
                      color: themeOptions.main,
                      onTap: () {
                        onSelectOption(opt.route);
                      },
                    ),

                    _MenuOption(
                      options: const _Options(
                        title: 'Log Out',
                        icon: Icons.logout, 
                        route: TWSARoutes.loginPage,
                        suffix: false
                      ),
                      color: themeOptions.main, 
                      onTap: (){
                        onSelectOption(TWSARoutes.loginPage);
                        _sessionStorage.clearSession();
                      })
                  ],
                ),
              )
            ],
          ),
        ),
      )
    );
  }
}